
using System.IO;
using System.Text;
using System;

namespace PsdParse
{
    /// <summary>
    /// 图像资源段-图像资源块-资源数据-缩略图资源格式 <see cref="EImageResourceID.ThumbnailResource_PS4"/> 、 <see cref="EImageResourceID.ThumbnailResource_PS5"/>
    /// </summary>
    public class ThumbnailResourceFormat : IStreamParse
    {
        private EImageResourceID m_ImageResourceID;
        /// <summary>
        /// 图像资源ID
        /// </summary>
        public EImageResourceID ImageResourceID
        {
            get
            {
                return m_ImageResourceID;
            }
            set
            {
                if (Enum.IsDefined(typeof(EImageResourceID), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块-资源数据-缩略图资源格式）异常，ImageResourceID:{0}", value));
                }
                m_ImageResourceID = value;
            }
        }

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

        public ThumbnailResourceFormat(EImageResourceID imageResourceID)
        {
            ImageResourceID = imageResourceID;
        }

        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Format = (ERGBFormat)reader.ReadInt32();
            ThumbnailWidth = reader.ReadUInt32();
            ThumbnailHeight = reader.ReadUInt32();
            WidthBytesCount = reader.ReadUInt32();
            TotalSize = reader.ReadUInt32();
            CompressionSize = reader.ReadUInt32();
            BitsPerPixel = reader.ReadUInt16();
            PlanesCount = reader.ReadUInt16();
            JFIFData = reader.ReadBytes((int)TotalSize);
        }
    }
}
