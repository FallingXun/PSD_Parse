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
        /// ͼ����Դ������
        /// </summary>
        public ImageResourceBlock m_ImageResourceBlock;


        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Length = reader.ReadInt32();
            if (Length > 0)
            {
                m_ImageResourceBlock = new ImageResourceBlock();
                m_ImageResourceBlock.Parse(reader, encoding);
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
        public EImageResourceID ImageResourceIID
        {
            get
            {
                return m_ImageResourceID;
            }
            set
            {
                if(value == EImageResourceID.Unknowm)
                {
                    throw new Exception(string.Format("PSD �ļ���ͼ����Դ��-ͼ����Դ�飩�쳣��ImageResourceI:{0}", value));
                }
                m_ImageResourceID = value;
            }
        }

        private string m_Name;
        /// <summary>
        /// ���֣�Pascal �ַ������������������ֽڵ�0��ɣ�
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        private uint m_ResourceDataSize;
        /// <summary>
        /// ��Դ���ݴ�С��4 �ֽڣ�����Ҫ�ڴ����
        /// </summary>
        public uint ResourceDataSize
        {
            get
            {
                return m_ResourceDataSize;
            }
            set
            {
                m_ResourceDataSize = value;
            }
        }


        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Signature = Encoding.ASCII.GetString(reader.ReadBytes(4));
            ImageResourceIID = (EImageResourceID)reader.ReadInt16();

            // �ڴ�����ֽڴ�С
            var multiple = 2;

            // Pascal �ַ���Ҫ���趨ֵ�ı����洢�������� 2 �ֽڣ���ȡ�����Ҫ����ƫ���ֽ�
            var startPosition = reader.BaseStream.Position;
            var count = (int)reader.ReadByte();
            var bytes = reader.ReadBytes(count);
            var offset = (int)(reader.BaseStream.Position - startPosition);
            var mod = offset % multiple;
            if (mod > 0)
            {
                var padding = multiple - mod;
                reader.BaseStream.Position += padding;
            }
            Name = encoding.GetString(bytes);

            ResourceDataSize = reader.ReadUInt32();

            var endPosition = reader.BaseStream.Position + ResourceDataSize;
            mod = (int)(ResourceDataSize % (uint)multiple);
            if (mod > 0)
            {
                var padding = multiple - mod;
                endPosition += padding;
            }
        }
    }
}