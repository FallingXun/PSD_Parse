using System;

namespace PsdParse
{
    public class VersionInfoResourceFormat : IStreamHandler
    {
        /// <summary>
        /// 版本信息（4 字节）
        /// </summary>
        public uint Version
        {
            get; set;
        }

        /// <summary>
        /// 是否有真实合并数据（1 字节）
        /// </summary>
        public byte HasRealMergedData
        {
            get; set;
        }

        /// <summary>
        /// 作者名，Unicode 字符串
        /// </summary>
        public string WriterName
        {
            get; set;
        }

        /// <summary>
        /// 读者名，Unicode 字符串
        /// </summary>
        public string ReaderName
        {
            get; set;
        }

        /// <summary>
        /// 文件版本（4 字节）
        /// </summary>
        public uint FileVersion
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Version = reader.ReadUInt32();
            HasRealMergedData = reader.ReadByte();
            WriterName = reader.ReadUnicodeString();
            ReaderName = reader.ReadUnicodeString();
            FileVersion = reader.ReadUInt32();
        }

        public void Combine(Writer writer)
        {
            writer.WriteUInt32(Version);
            writer.WriteByte(HasRealMergedData);
            writer.WriteUnicodeString(WriterName);
            writer.WriteUnicodeString(ReaderName);
            writer.WriteUInt32(FileVersion);
        }

        public int CalculateLength(Calculator calculator)
        {
            var length = 0;

            length += calculator.CalculateUInt32(Version);
            length += calculator.CalculateByte(HasRealMergedData);
            length += calculator.CalculateUnicodeString(WriterName);
            length += calculator.CalculateUnicodeString(ReaderName);
            length += calculator.CalculateUInt32(FileVersion);

            return length;
        }
    }
}
