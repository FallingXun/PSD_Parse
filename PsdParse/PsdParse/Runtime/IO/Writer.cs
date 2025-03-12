using System.IO;
using System.Text;
using System;

namespace PsdParse
{
    public class Writer : BinaryWriter
    {
        private Encoding m_Encoding;

        public Writer(Stream stream) : base(stream)
        {
            m_Encoding = Encoding.Default;
        }

        public Writer(Stream stream, Encoding encoding) : base(stream, encoding)
        {
            m_Encoding = encoding;
        }

        public Writer(Stream stream, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen)
        {
            m_Encoding = encoding;
        }

        public void WriteByte(byte value)
        {
            base.Write(value);
        }

        public void WriteBytes(byte[] value)
        {
            base.Write(value);
        }

        #region BinaryWriter 使用小端字节序，需要反转字节数据

        public void WriteInt16(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        public void WriteUInt16(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        public void WriteInt32(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        public void WriteUInt32(uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        public void WriteInt64(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        public void WriteUInt64(ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        public void WriteSingle(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        public void WriteDouble(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            WriteBytes(bytes);
        }

        #endregion

        public void WriteASCIIString(string value, int length)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            if (bytes.Length != length)
            {
                throw new Exception(string.Format("WriteASCIIString 错误，数据长度：{0}，length：{1}", bytes.Length, length));
            }
            WriteBytes(bytes);
        }

        /// <summary>
        /// 写入 Pascal 字符串，Pascal 字符串 = 1 个字节长度信息 + 字符串内容 + 对齐偏移
        /// </summary>
        /// <param name="factor">对齐因数（字符串长度需要为此数的倍数）</param>
        /// <returns></returns>
        public void WritePascalString(string value, uint factor)
        {
            var startPosition = BaseStream.Position;
            var bytes = m_Encoding.GetBytes(value);
            byte count = (byte)bytes.Length;
            WriteByte(count);
            WriteBytes(bytes);
            var padding = Utils.GetPadding((uint)(BaseStream.Position - startPosition), factor);
            WritePadding(padding);
        }


        /// <summary>
        /// 写入对齐偏移数据（用 0 填充）
        /// </summary>
        /// <param name="padding">偏移长度</param>
        /// <returns></returns>
        public void WritePadding(uint padding)
        {
            for (int i = 0; i < padding; i++)
            {
                WriteByte(0);
            }
        }

        public void WriteRectangle(Rectangle value)
        {
            WriteInt32(value.Top);
            WriteInt32(value.Left);
            WriteInt32(value.Bottom);
            WriteInt32(value.Right);
        }
    }
}
