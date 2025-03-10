
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsdParse
{
    public class RLEImageData : IStreamParse
    {
        /// <summary>
        /// 通道数
        /// </summary>
        private int m_ChannelCount;
        /// <summary>
        /// 高度
        /// </summary>
        private int m_Height;

        /// <summary>
        /// 所有通道的图像数据列表
        /// </summary>
        public List<RLEChannelImageData> RLEChannelImageDataList
        {
            get; set;
        }



        public RLEImageData(int channelCount, int height)
        {
            m_ChannelCount = channelCount;
            m_Height = height;
        }

        public void Parse(Reader reader)
        {
            RLEChannelImageDataList = new List<RLEChannelImageData>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                var item = new RLEChannelImageData(m_Height);
                for (int j = 0; j < m_Height; j++)
                {
                    item.Parse(reader);
                }
                RLEChannelImageDataList.Add(item);
            }

        }
    }

    /// <summary>
    /// RLE 单通道的图像数据
    /// </summary>
    public class RLEChannelImageData : IStreamParse
    {
        private int m_Height;

        /// <summary>
        /// 单通道图像每行的数据长度
        /// </summary>
        public List<ushort> LineDataLengthList
        {
            get; set;
        }

        /// <summary>
        /// 单通道图像的数据
        /// </summary>
        public byte[] ChannelImageData
        {
            get;set;
        }

        public RLEChannelImageData(int height)
        {
            m_Height = height;
        }

        public void Parse(Reader reader)
        {
            int channelImageDataLength = 0;
            LineDataLengthList = new List<ushort>(m_Height);
            for (int i = 0; i < m_Height; i++)
            {
                var item = reader.ReadUInt16();
                LineDataLengthList.Add(item);
                channelImageDataLength += item;
            }
            ChannelImageData = reader.ReadBytes(channelImageDataLength);
        }
    }

}
