using System.Collections.Generic;

namespace PsdParse
{
    /// <summary>
    /// 单层单通道的 RLE 图像数据
    /// </summary>
    public class ChannelRLEImageData : IStreamHandler
    {
        /// <summary>
        /// 单通道图像每行的数据长度
        /// </summary>
        public ushort[] ChannelLineDataLength
        {
            get; set;
        }

        /// <summary>
        /// 当前通道的图像数据
        /// </summary>
        public byte[] ChannelImageBytes
        {
            get; set;
        }

        /// <summary>
        /// 高度
        /// </summary>
        private int m_Height;


        public ChannelRLEImageData(int height)
        {
            m_Height = height;
        }

        public void Parse(Reader reader)
        {
            var length = 0;
            ChannelLineDataLength = new ushort[m_Height];
            for (int i = 0; i < m_Height; i++)
            {
                ChannelLineDataLength[i] = reader.ReadUInt16();
                length += ChannelLineDataLength[i];
            }
            ChannelImageBytes = reader.ReadBytes(length);
        }

        public void Combine(Writer writer)
        {
            for (int i = 0; i < m_Height; i++)
            {
                writer.WriteUInt16(ChannelLineDataLength[i]);
            }
            writer.WriteBytes(ChannelImageBytes);
        }
    }

}
