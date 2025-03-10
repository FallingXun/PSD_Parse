
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
        /// 所有通道的图像数据列表
        ///     RLE压缩时，每个通道的字节数组，前面部分为每一行的数据长度，行数为 LayerBottom - LayerTop ，每个数据长度为 2 字节（PSB 为 4 字节），所有行的长度后才是图像数据
        /// </summary>
        [ByteSize()]
        public List<IStreamParse> ChannelImageDataList
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
            ChannelImageDataList = new List<IStreamParse>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                var compression = (ECompression)reader.ReadUInt16();
                // 此次只做预读取，方便确定格式，真实读取放到对应格式中统一管理
                reader.BaseStream.Position -= 2;
                IStreamParse item = null;
                switch (compression)
                {
                    case ECompression.RawData:
                        {
                            var channelImageDataLength = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            item = new ChannelRawImageData(channelImageDataLength);
                        }
                        break;
                    case ECompression.RLECompression:
                        {
                            item = new ChannelRLEImageData(m_Height);
                        }
                        break;
                    default:
                        {
                            var channelImageDataLength = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            item = new ChannelRawImageData(channelImageDataLength);
                        }
                        break;
                }
                item.Parse(reader);
                ChannelImageDataList.Add(item);
            }
        }
    }

}