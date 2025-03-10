
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 图像数据段
    /// </summary>
    public class ImageDataSection : IStreamParse
    {
        /// <summary>
        /// 压缩方法（2 字节）
        /// </summary>
        [ByteSize(2)]
        public ECompression Compression
        {
            get; set;
        }

        /// <summary>
        /// 所有通道的图像
        ///     RLE压缩时，每个通道的字节数组，前面部分为每一行的数据长度，行数为 LayerBottom - LayerTop ，每个数据长度为 2 字节（PSB 为 4 字节），所有行的长度后才是图像数据
        /// </summary>
        [ByteSize()]
        public IStreamParse ImageData
        {
            get; set;
        }

        /// <summary>
        /// 通道数
        /// </summary>
        private int m_ChannelCount;
        /// <summary>
        /// 宽度
        /// </summary>
        private int m_Width;
        /// <summary>
        /// 高度
        /// </summary>
        private int m_Height;
        /// <summary>
        /// 通道比特数
        /// </summary>
        private EDepth m_Depth;
        /// <summary>
        /// 颜色模式
        /// </summary>
        private EColorMode m_ColorMode;

        public ImageDataSection(int channelCount, int width, int height, EDepth depth, EColorMode colorMode)
        {
            m_ChannelCount = channelCount;
            m_Width = width;
            m_Height = height;
            m_Depth = depth;
            m_ColorMode = colorMode;
        }

        public void Parse(Reader reader)
        {
            Compression = (ECompression)reader.ReadUInt16();

            switch (Compression)
            {
                case ECompression.RawData:
                    {
                        ImageData = new RawImageData(m_ChannelCount, m_Width, m_Height, m_Depth);
                    }
                    break;
                case ECompression.RLECompression:
                    {
                        ImageData = new RLEImageData(m_ChannelCount, m_Height);
                    }
                    break;
                default:
                    {
                        ImageData = new RawImageData(m_ChannelCount, m_Width, m_Height, m_Depth);
                    }
                    break;
            }
            ImageData.Parse(reader);
        }
    }

}