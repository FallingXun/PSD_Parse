using System;
using System.Collections.Generic;
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
        public List<ImageResourceBlock> ImageResourceBlockList
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Length = reader.ReadInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Length;
            if (Length > 0)
            {
                ImageResourceBlockList = new List<ImageResourceBlock>();
                while (reader.BaseStream.Position < endPosition)
                {
                    var item = new ImageResourceBlock();
                    item.Parse(reader);
                    ImageResourceBlockList.Add(item);

                    // ����Ƿ�����һ�� ImageResourceBlock
                    var signature = reader.ReadASCIIString(4);
                    reader.BaseStream.Position -= 4;
                    if (signature != Const.Signature_8BIM)
                    {
                        break;
                    }
                }
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD �ļ���ͼ����Դ�Σ��쳣�����ݳ���:{0}��Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }
    }

    #region ͼ����Դ�Σ�Image Resources Section��- ͼ����Դ�飨Image Resource Blocks��
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
                if (value != Const.Signature_8BIM)
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
                    if ((value > EImageResourceID.PathInfoStart && value < EImageResourceID.PathInfoEnd) == false && (value > EImageResourceID.PluginResourceStart && value < EImageResourceID.PluginResourceEnd) == false)
                    {
                        Console.Write(string.Format("PSD �ļ���ͼ����Դ��-ͼ����Դ�飩�쳣��ImageResource:{0}", value));
                    }
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
            ResourceData = new ResourceData(ImageResourceID, ResourceDataSize);
            ResourceData.Parse(reader);
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
    public class ResourceData : IStreamParse
    {
        /// <summary>
        /// ͼ����ԴID
        /// </summary>
        private EImageResourceID m_ImageResourceID;
        /// <summary>
        /// ��Դ���ݴ�С
        /// </summary>
        private uint m_DataSize;

        /// <summary>
        /// ��ʽ����
        /// </summary>
        [ByteSize()]
        public IStreamParse ResourceFormat
        {
            get; set;
        }

        public ResourceData(EImageResourceID imageResourceID, uint dataSize)
        {
            m_ImageResourceID = imageResourceID;
            m_DataSize = dataSize;

        }

        public void Parse(Reader reader)
        {
            switch (m_ImageResourceID)
            {
                case EImageResourceID.GridAndGuidesInfo_PS4:
                    {
                        ResourceFormat = new GridAndGuidesResourceFormat();
                    }
                    break;
                case EImageResourceID.ThumbnailResource_PS4:
                case EImageResourceID.ThumbnailResource_PS5:
                    {
                        ResourceFormat = new ThumbnailResourceFormat(m_ImageResourceID);
                    }
                    break;
                default:
                    {
                        // todo���ο����� ResourceFormat ʵ��
                        ResourceFormat = new DefaultResourceFormat(m_ImageResourceID, m_DataSize);
                    }
                    break;
            }
            ResourceFormat.Parse(reader);
        }
    }
    #endregion
}