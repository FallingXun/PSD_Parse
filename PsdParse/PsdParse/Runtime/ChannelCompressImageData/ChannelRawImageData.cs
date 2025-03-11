namespace PsdParse
{
    /// <summary>
    /// 单层单通道的无压缩图像数据
    /// </summary>
    public class ChannelRawImageData : IStreamParse
    {
        /// <summary>
        /// 当前通道的图像数据
        /// </summary>
        public byte[] ChannelImageBytes
        {
            get; set;
        }

        private int m_ChannelImageBytesLength;


        public ChannelRawImageData(int channelImageBytesLength)
        {
            m_ChannelImageBytesLength = channelImageBytesLength;
        }


        public void Parse(Reader reader)
        {
            ChannelImageBytes = reader.ReadBytes((int)m_ChannelImageBytesLength);
        }
    }
}
