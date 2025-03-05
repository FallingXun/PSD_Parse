using System;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// �ļ�ͷ��
    /// </summary>
    public class FileHeaderSection : IStreamParse
    {
        private string m_Signature;
        /// <summary>
        /// ��ʶ����4 �ֽڣ�������Ϊ"8BPS"
        /// </summary>
        public string Signature
        {
            get
            {
                return m_Signature;
            }
            set
            {
                if (value != "8BPS")
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��Signature:{0}", value));
                }
                m_Signature = value;
            }
        }


        private int m_Version;
        /// <summary>
        /// �汾��Ϣ��2 �ֽڣ���PSD��1��PSB��2
        /// </summary>
        public int Version
        {
            get
            {
                return m_Version;
            }
            set
            {
                if (value != 1)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��Version:{0}", value));
                }
                m_Version = value;
            }
        }


        private int m_Reserved;
        /// <summary>
        /// ����Ϊ0��6 �ֽڣ�
        /// </summary>
        public int Reserved
        {
            get
            {
                return m_Reserved;
            }
            set
            {
                if (value != 1)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��Reserved:{0}", value));
                }
                m_Reserved = value;
            }
        }


        private int m_ChannelCount;
        /// <summary>
        /// ͼ���е�ͨ������2 �ֽڣ��������κΰ�����ͨ����֧�ֵķ�ΧΪ1��56
        /// </summary>
        public int ChannelCount
        {
            get
            {
                return m_ChannelCount;
            }
            set
            {
                if (value < 1 || value > 56)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��ChannelCount:{0}", value));
                }
                m_ChannelCount = value;
            }
        }


        private int m_Height;
        /// <summary>
        /// ͼ��ĸ߶ȣ�4 �ֽڣ�,֧�ֵķ�ΧΪ1��30000��PSB��300000��
        /// </summary>
        public int Height
        {
            get
            {
                return m_Height;
            }
            set
            {
                if (value < 1 || value > 30000)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��Height:{0}", value));
                }
                m_Height = value;
            }
        }

        /// <summary>
        /// ͼ��Ŀ�ȣ�4 �ֽڣ�,֧�ֵķ�ΧΪ1��30000��PSB��300000��
        /// </summary>
        private int m_Width;
        public int Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                if (value < 1 || value > 30000)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��Width:{0}", value));
                }
                m_Width = value;
            }
        }


        private EDepth m_Depth;
        /// <summary>
        /// ÿ��ͨ���ı�������2 �ֽڣ�
        /// </summary>
        public EDepth Depth
        {
            get
            {
                return m_Depth;
            }
            set
            {
                switch (value)
                {
                    case EDepth.Bit_1:
                    case EDepth.Bit_8:
                    case EDepth.Bit_16:
                    case EDepth.Bit_32:
                        break;
                    default:
                        {
                            throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��Depth:{0}", value));
                        }
                }
                m_Depth = value;
            }
        }


        private EColorMode m_ColorMode;
        /// <summary>
        /// �ļ�����ɫģʽ��2 �ֽڣ�
        /// </summary>
        public EColorMode ColorMode
        {
            get
            {
                return m_ColorMode;
            }
            set
            {
                switch (value)
                {
                    case EColorMode.Bitmap:
                    case EColorMode.Grayscale:
                    case EColorMode.Indexed:
                    case EColorMode.RGB:
                    case EColorMode.CMYK:
                    case EColorMode.Multichannel:
                    case EColorMode.Duotone:
                    case EColorMode.Lab:
                        break;
                    default:
                        {
                            throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��m_ColorMode:{0}", value));
                        }
                }
                m_ColorMode = value;
            }
        }

        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Signature = encoding.GetString(reader.ReadBytes(4));
            Version = reader.ReadInt16();
            Reserved = (reader.ReadInt16() << 16) + reader.ReadInt32();
            ChannelCount = reader.ReadInt16();
            Height = reader.ReadInt32();
            Width = reader.ReadInt32();
            Depth = (EDepth)reader.ReadInt16();
            ColorMode = (EColorMode)reader.ReadInt16();
        }
    }
}