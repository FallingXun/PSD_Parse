namespace PsdParse
{
    /// <summary>
    /// 单层单通道默认图像数据（未解析）
    /// </summary>
    public class ChannelDefaultImageData : IStreamHandler
    {
        /// <summary>
        /// 当前通道的图像数据
        /// </summary>
        public byte[] ChannelImageBytes
        {
            get; set;
        }

        private int m_ChannelImageBytesLength;
        private ECompression m_Compression;


        public ChannelDefaultImageData(ECompression compression, int channelImageBytesLength)
        {
            m_Compression = compression;
            m_ChannelImageBytesLength = channelImageBytesLength;
        }


        public void Parse(Reader reader)
        {
            ChannelImageBytes = reader.ReadBytes((int)m_ChannelImageBytesLength);
        }

        public void Combine(Writer writer)
        {
            writer.WriteBytes(ChannelImageBytes);
        }
    }
}
