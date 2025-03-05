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


        private int m_Version;
        /// <summary>
        /// 版本信息（2 字节），PSD是1，PSB是2
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，Version:{0}", value));
                }
                m_Version = value;
            }
        }


        private int m_Reserved;
        /// <summary>
        /// 必须为0（6 字节）
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
                    throw new Exception(string.Format("PSD 文件（文件头）异常，Reserved:{0}", value));
                }
                m_Reserved = value;
            }
        }


        private int m_ChannelCount;
        /// <summary>
        /// 图像中的通道数（2 字节），包括任何阿尔法通道，支持的范围为1到56
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
                switch (value)
                {
                    case EDepth.Bit_1:
                    case EDepth.Bit_8:
                    case EDepth.Bit_16:
                    case EDepth.Bit_32:
                        break;
                    default:
                        {
                            throw new Exception(string.Format("PSD 文件（文件头）异常，Depth:{0}", value));
                        }
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
                            throw new Exception(string.Format("PSD 文件（文件头）异常，m_ColorMode:{0}", value));
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