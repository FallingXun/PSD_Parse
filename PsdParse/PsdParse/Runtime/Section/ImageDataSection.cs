
using System.Collections.Generic;
using System;

namespace PsdParse
{
    /// <summary>
    /// 图像数据段
    /// </summary>
    public class ImageDataSection : IStreamHandler
    {
        private ECompression m_Compression;
        /// <summary>
        /// 压缩格式（2 字节）
        /// </summary>
        [ByteSize(2)]
        public ECompression Compression
        {
            get
            {
                return m_Compression;
            }
            set
            {
                if (Enum.IsDefined(typeof(ECompression), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图像数据段）异常，Compression:{0}", value));
                }
                m_Compression = value;
            }
        }

        /// <summary>
        /// 所有通道图像的每行数据长度的列表
        /// </summary>
        [ByteSize()]
        public List<ushort[]> ChannelLineLengthList
        {
            get; set;
        }

        /// <summary>
        /// 所有通道图像的实际图像数据的列表
        ///     RLE压缩时，每个通道的字节数组，前面部分为每一行的数据长度，行数为 LayerBottom - LayerTop ，每个数据长度为 2 字节（PSB 为 4 字节），所有行的长度后才是图像数据
        /// </summary>
        public List<byte[]> ChannelImageBytesList
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
            ChannelLineLengthList = new List<ushort[]>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                switch (Compression)
                {
                    case ECompression.RawData:
                        {
                            ChannelLineLengthList.Add(null);
                        }
                        break;
                    case ECompression.RLECompression:
                        {
                            var lineLength = new ushort[m_Height];
                            for (int j = 0; j < m_Height; j++)
                            {
                                lineLength[j] = reader.ReadUInt16();
                            }
                            ChannelLineLengthList.Add(lineLength);
                        }
                        break;
                    default:
                        {
                            ChannelLineLengthList.Add(null);
                        }
                        break;
                }
            }

            ChannelImageBytesList = new List<byte[]>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                switch (Compression)
                {
                    case ECompression.RawData:
                        {
                            var length = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            var data = reader.ReadBytes(length);
                            ChannelImageBytesList.Add(data);
                        }
                        break;
                    case ECompression.RLECompression:
                        {
                            var length = 0;
                            for (int j = 0; j < ChannelLineLengthList[i].Length; j++)
                            {
                                length += ChannelLineLengthList[i][j];
                            }
                            var data = reader.ReadBytes(length);
                            ChannelImageBytesList.Add(data);
                        }
                        break;
                    default:
                        {
                            var length = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            var data = reader.ReadBytes(length);
                            ChannelImageBytesList.Add(data);
                        }
                        break;
                }
            }
        }

        public void Combine(Writer writer)
        {
            writer.WriteUInt16((ushort)Compression);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                switch (Compression)
                {
                    case ECompression.RLECompression:
                        {
                            var lineLength = ChannelLineLengthList[i];
                            for (int j = 0; j < m_Height; j++)
                            {
                                writer.WriteUInt16(lineLength[j]);
                            }
                        }
                        break;
                }
            }

            ChannelImageBytesList = new List<byte[]>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                writer.WriteBytes(ChannelImageBytesList[i]);
            }
        }
    }

}