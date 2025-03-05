using System;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 文件头段
    /// </summary>
    public class FileHeaderSection : IStreamParse
    {
        private string m_Signature;
        /// <summary>
        /// 标识符（4 字节），必须为"8BPS"
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，Signature:{0}", value));
                }
                m_Signature = value;
            }
        }


        private short m_Version;
        /// <summary>
        /// 版本信息（2 字节），PSD是1，PSB是2
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，Version:{0}", value));
                }
                m_Version = value;
            }
        }


        private short m_ReservedHigh;
        /// <summary>
        /// Reserve 高位，必须为0（2 字节）
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，ReservedHigh:{0}", value));
                }
                m_ReservedHigh = value;
            }
        }

        private int m_ReservedLow;
        /// <summary>
        /// Reserve 低位，必须为0（4 字节）
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，ReservedLow:{0}", value));
                }
                m_ReservedLow = value;
            }
        }


        private short m_ChannelCount;
        /// <summary>
        /// 图像中的通道数（2 字节），包括任何阿尔法通道，支持的范围为1到56
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，ChannelCount:{0}", value));
                }
                m_ChannelCount = value;
            }
        }


        private int m_Height;
        /// <summary>
        /// 图像的高度（4 字节）,支持的范围为1到30000（PSB到300000）
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，Height:{0}", value));
                }
                m_Height = value;
            }
        }

        /// <summary>
        /// 图像的宽度（4 字节）,支持的范围为1到30000（PSB到300000）
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，Width:{0}", value));
                }
                m_Width = value;
            }
        }


        private EDepth m_Depth;
        /// <summary>
        /// 每个通道的比特数（2 字节）
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，Depth:{0}", value));
                }
                m_Depth = value;
            }
        }


        private EColorMode m_ColorMode;
        /// <summary>
        /// 文件的颜色模式（2 字节）
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，m_ColorMode:{0}", value));
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