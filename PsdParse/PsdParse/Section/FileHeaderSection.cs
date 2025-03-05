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


        private short m_Version;
        /// <summary>
        /// �汾��Ϣ��2 �ֽڣ���PSD��1��PSB��2
        /// </summary>
        public short Version
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


        private short m_ReservedHigh;
        /// <summary>
        /// Reserve ��λ������Ϊ0��2 �ֽڣ�
        /// </summary>
        public short ReservedHigh
        {
            get
            {
                return m_ReservedHigh;
            }
            set
            {
                if (value != 1)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��ReservedHigh:{0}", value));
                }
                m_ReservedHigh = value;
            }
        }

        private int m_ReservedLow;
        /// <summary>
        /// Reserve ��λ������Ϊ0��4 �ֽڣ�
        /// </summary>
        public int ReservedLow
        {
            get
            {
                return m_ReservedLow;
            }
            set
            {
                if (value != 1)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��ReservedLow:{0}", value));
                }
                m_ReservedLow = value;
            }
        }


        private short m_ChannelCount;
        /// <summary>
        /// ͼ���е�ͨ������2 �ֽڣ��������κΰ�����ͨ����֧�ֵķ�ΧΪ1��56
        /// </summary>
        public short ChannelCount
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
                if (Enum.IsDefined(typeof(EDepth), value) == false)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��Depth:{0}", value));
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
                if (Enum.IsDefined(typeof(EColorMode), value) == false)
                {
                    throw new Exception(string.Format("PSD �ļ����ļ�ͷ���쳣��m_ColorMode:{0}", value));
                }
                m_ColorMode = value;
            }
        }

        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Signature = encoding.GetString(reader.ReadBytes(4));
            Version = reader.ReadInt16();
            ReservedHigh = reader.ReadInt16();
            ReservedLow = reader.ReadInt32();
            ChannelCount = reader.ReadInt16();
            Height = reader.ReadInt32();
            Width = reader.ReadInt32();
            Depth = (EDepth)reader.ReadInt16();
            ColorMode = (EColorMode)reader.ReadInt16();
        }
    }
}