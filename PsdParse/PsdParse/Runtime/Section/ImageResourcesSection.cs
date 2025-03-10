using System;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// ͼ����Դ��
    /// </summary>
    public class ImageResourcesSection : IStreamParse
    {
        /// <summary>
        /// ͼ����Դ�γ��ȣ�4 �ֽڣ�
        /// </summary>
        public int Length
        {
            get; set;
        }

        /// <summary>
        /// ͼ����Դ��
        /// </summary>
        public ImageResourceBlock ImageResourceBlock
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Length = reader.ReadInt32();
            if (Length > 0)
            {
                ImageResourceBlock = new ImageResourceBlock();
                ImageResourceBlock.Parse(reader);
            }
        }
    }


    /// <summary>
    /// ͼ����Դ��-ͼ����Դ��
    /// </summary>
    public class ImageResourceBlock : IStreamParse
    {
        private string m_Signature;
        /// <summary>
        /// ��ʶ����4 �ֽڣ�������Ϊ"8BIM"
        /// </summary>
        public string Signature
        {
            get
            {
                return m_Signature;
            }
            set
            {
                if (value != "8BIM")
                {
                    throw new Exception(string.Format("PSD �ļ���ͼ����Դ��-ͼ����Դ�飩�쳣��Signature:{0}", value));
                }
                m_Signature = value;
            }
        }

        private EImageResourceID m_ImageResourceID;
        /// <summary>
        /// ͼ����ԴID��2 �ֽڣ�
        /// </summary>
        public EImageResourceID ImageResourceID
        {
            get
            {
                return m_ImageResourceID;
            }
            set
            {
                if (Enum.IsDefined(typeof(EImageResourceID), value) == false)
                {
                    throw new Exception(string.Format("PSD �ļ���ͼ����Դ��-ͼ����Դ�飩�쳣��ImageResource:{0}", value));
                }
                m_ImageResourceID = value;
            }
        }

        /// <summary>
        /// ���֣�Pascal �ַ���������Ϊ 2 �ı������������������ֽڵ�0��ɣ�
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// ��Դ���ݴ�С��4 �ֽڣ�����Ҫ�ڴ����
        /// </summary>
        public uint ResourceDataSize
        {
            get; set;
        }


        /// <summary>
        /// ��Դ����
        /// </summary>
        public ResourceData ResourceData
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Signature = reader.ReadASCIIString(4);
            ImageResourceID = (EImageResourceID)reader.ReadInt16();

            // �ڴ�����ֽڴ�С
            var factor = 2u;

            // Pascal �ַ���Ҫ���趨ֵ�ı����洢�������� 2 �ֽڣ���ȡ�����Ҫ����ƫ���ֽ�
            Name = reader.ReadPascalString(factor);

            ResourceDataSize = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Utils.RoundUp(ResourceDataSize, factor);
            ResourceData = new ResourceData(ImageResourceID);
            if (ResourceData.ResourceFormat != null)
            {
                ResourceData.ResourceFormat.Parse(reader);
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD �ļ���ͼ����Դ��-ͼ����Դ�飩�쳣�����ݳ���:{0}��ResourceDataSize:{1}", reader.BaseStream.Position - startPosition, ResourceDataSize));
            }
        }
    }


    /// <summary>
    /// ͼ����Դ��-ͼ����Դ��-��Դ����
    /// </summary>
    public class ResourceData
    {
        private EImageResourceID m_ImageResourceID;
        /// <summary>
        /// ͼ����ԴID
        /// </summary>
        public EImageResourceID ImageResourceID
        {
            get
            {
                return m_ImageResourceID;
            }
            set
            {
                if (Enum.IsDefined(typeof(EImageResourceID), value) == false)
                {
                    throw new Exception(string.Format("PSD �ļ���ͼ����Դ��-ͼ����Դ��-��Դ���ݣ��쳣��ImageResourceID:{0}", value));
                }
                m_ImageResourceID = value;
            }
        }

        /// <summary>
        /// ��ʽ����
        /// </summary>
        public IStreamParse ResourceFormat
        {
            get; set;
        }

        public ResourceData(EImageResourceID imageResourceID)
        {
            ImageResourceID = imageResourceID;
            switch (imageResourceID)
            {
                case EImageResourceID.GridAndGuidesInfo_PS4:
                    {
                        ResourceFormat = new GridAndGuidesResourceFormat();
                    }
                    break;
                case EImageResourceID.ThumbnailResource_PS4:
                case EImageResourceID.ThumbnailResource_PS5:
                    {
                        ResourceFormat = new ThumbnailResourceFormat(imageResourceID);
                    }
                    break;
                default:
                    {
                        // todo���ο����� ResourceFormat ʵ��
                    }
                    break;
            }
        }
    }
}