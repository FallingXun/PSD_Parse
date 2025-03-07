
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 单层所有通道的图像数据
    /// </summary>
    public class RawImageData : IStreamParse
    {
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
        /// 每个通道的图像数据长度
        /// </summary>
        private List<int> m_ChannelImageDataLengthList;

        /// <summary>
        /// 所有通道的图像数据列表
        /// </summary>
        public List<byte[]> ChannelImageDataList
        {
            get; set;
        }

        public RawImageData(int channelCount, int width, int height, EDepth depth)
        {
            m_ChannelCount = channelCount;
            m_Width = width;
            m_Height = height;
            // 单像素数据不为字节的倍数，最终需要补足 1 个字节
            var channelImageDataLength = ((int)depth * width + 7) / 8 * height;
            m_ChannelImageDataLengthList = new List<int>(channelCount);
            for (int i = 0; i < channelCount; i++)
            {
                m_ChannelImageDataLengthList.Add(channelImageDataLength);
            }

        }

        public RawImageData(int channelCount, int width, int height, List<ChannelInfo> channelInfoList)
        {
            m_ChannelCount = channelCount;
            m_Width = width;
            m_Height = height;
            m_ChannelImageDataLengthList = new List<int>(channelCount);
            for (int i = 0; i < channelCount; i++)
            {
                // ChannelInfo.Length 为 ChannelImageData 的总长度，数据长度需要减去 ChannelImageData.Compress 的长度
                var channelImageDataLength = (int)channelInfoList[i].Length - 2;
                m_ChannelImageDataLengthList.Add(channelImageDataLength);
            }
        }


        public void Parse(BinaryReader reader, Encoding encoding)
        {
            ChannelImageDataList = new List<byte[]>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                var item = reader.ReadBytes(m_ChannelImageDataLengthList[i]);
                ChannelImageDataList.Add(item);
            }
        }
    }
}
