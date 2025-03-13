using System;

namespace PsdParse
{
    /// <summary>
    /// 图像资源段-图像资源块-资源数据-缩略图资源格式 <see cref="EImageResourceID.ThumbnailResource_PS4"/> 、 <see cref="EImageResourceID.ThumbnailResource_PS5"/>
    /// </summary>
    public class ThumbnailResourceFormat : IStreamHandler
    {
        private ERGBFormat m_Format;
        /// <summary>
        /// RGB格式（4 字节）
        /// </summary>
        public ERGBFormat Format
        {
            get
            {
                return m_Format;
            }
            set
            {
                if (Enum.IsDefined(typeof(ERGBFormat), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块-资源数据-缩略图资源格式）异常，Format:{0}", value));
                }
                m_Format = value;
            }
        }

        /// <summary>
        /// 缩略图像素宽度（4 字节）
        /// </summary>
        public uint ThumbnailWidth
        {
            get; set;
        }

        /// <summary>
        /// 缩略图像素高度（4 字节）
        /// </summary>
        public uint ThumbnailHeight
        {
            get; set;
        }

        /// <summary>
        /// 宽度字节数（4 字节），填充行字节 = （宽度 * 每像素位数 + 31）/ 32 * 4
        /// </summary>
        public uint WidthBytesCount
        {
            get; set;
        }

        /// <summary>
        /// 总字节数（4 字节），总字节数 = 宽度字节数 * 高 * 平面数
        /// </summary>
        public uint TotalSize
        {
            get; set;
        }

        /// <summary>
        /// 压缩后字节数（4 字节）
        /// </summary>
        public uint CompressionSize
        {
            get; set;
        }

        /// <summary>
        /// 每像素字节数（2 字节）
        /// </summary>
        public ushort BitsPerPixel
        {
            get; set;
        }

        /// <summary>
        /// 平面数量（2 字节）
        /// </summary>
        public ushort PlanesCount
        {
            get; set;
        }

        /// <summary>
        /// JFIF 图像数据，<see cref="EImageResourceID.ThumbnailResource_PS4"> 是 BGR，<see cref="EImageResourceID.ThumbnailResource_PS4"> 是 RGB
        /// </summary>
        public byte[] JFIFData
        {
            get; set;
        }

        private EImageResourceID m_ImageResourceID;


        public ThumbnailResourceFormat(EImageResourceID imageResourceID)
        {
            m_ImageResourceID = imageResourceID;
        }

        public void Parse(Reader reader)
        {
            Format = (ERGBFormat)reader.ReadUInt32();
            ThumbnailWidth = reader.ReadUInt32();
            ThumbnailHeight = reader.ReadUInt32();
            WidthBytesCount = reader.ReadUInt32();
            TotalSize = reader.ReadUInt32();
            CompressionSize = reader.ReadUInt32();
            BitsPerPixel = reader.ReadUInt16();
            PlanesCount = reader.ReadUInt16();
            JFIFData = reader.ReadBytes((int)CompressionSize);
        }

        public void Combine(Writer writer)
        {
            writer.WriteUInt32((uint)Format);
            writer.WriteUInt32(ThumbnailWidth);
            writer.WriteUInt32(ThumbnailHeight);
            writer.WriteUInt32(WidthBytesCount);
            writer.WriteUInt32(TotalSize);
            writer.WriteUInt32(CompressionSize);
            writer.WriteUInt16(BitsPerPixel);
            writer.WriteUInt16(PlanesCount);
            writer.WriteBytes(JFIFData);
        }

        public int CalculateLength(Calculator calculator)
        {
            var length = 0;

            length += calculator.CalculateUInt32((uint)Format);
            length += calculator.CalculateUInt32(ThumbnailWidth);
            length += calculator.CalculateUInt32(ThumbnailHeight);
            length += calculator.CalculateUInt32(WidthBytesCount);
            length += calculator.CalculateUInt32(TotalSize);
            length += calculator.CalculateUInt32(CompressionSize);
            length += calculator.CalculateUInt16(BitsPerPixel);
            length += calculator.CalculateUInt16(PlanesCount);
            length += calculator.CalculateBytes(JFIFData);

            return length;
        }
    }
}
