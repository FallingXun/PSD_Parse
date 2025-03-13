

namespace PsdParse
{
    /// <summary>
    /// 图像资源段-图像资源块-资源数据-默认格式（未解析）
    /// </summary>
    public class DefaultResourceFormat : IStreamHandler
    {
        public byte[] Data
        {
            get;set;
        }

        private EImageResourceID m_ImageResourceID;

        private uint m_DataSize;


        public DefaultResourceFormat(EImageResourceID imageResourceID, uint dataSize)
        {
            m_ImageResourceID = imageResourceID;
            m_DataSize = dataSize;
        }

        public void Parse(Reader reader)
        {
            Data = reader.ReadBytes((int)m_DataSize);
        }

        public void Combine(Writer writer)
        {
            writer.WriteBytes(Data);
        }

        public int CalculateLength(Calculator calculator)
        {
            var length = 0;

            length += calculator.CalculateBytes(Data);

            return length;
        }
    }
}
