using System.IO;
using System.Text;
using System;

namespace PsdParse
{
    public class Reader : BinaryReader
    {
        private Encoding m_Encoding;

        public Reader(Stream stream) : base(stream)
        {
            m_Encoding = Encoding.Default;
        }

        public Reader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
            m_Encoding = encoding;
        }

        public Reader(Stream stream, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen)
        {
            m_Encoding = encoding;
        }

        #region BinaryReader 使用小端字节序，需要反转字节数据

        public override short ReadInt16()
        {
            var bytes = base.ReadBytes(2);
            Array.Reverse(bytes);
            var value = BitConverter.ToInt16(bytes, 0);
            return value;
        }

        public override ushort ReadUInt16()
        {
            var bytes = base.ReadBytes(2);
            Array.Reverse(bytes);
            var value = BitConverter.ToUInt16(bytes, 0);
            return value;
        }

        public override int ReadInt32()
        {
            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            var value = BitConverter.ToInt32(bytes, 0);
            return value;
        }

        public override uint ReadUInt32()
        {
            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            var value = BitConverter.ToUInt32(bytes, 0);
            return value;
        }

        public override long ReadInt64()
        {
            var bytes = base.ReadBytes(8);
            Array.Reverse(bytes);
            var value = BitConverter.ToInt64(bytes, 0);
            return value;
        }

        public override ulong ReadUInt64()
        {
            var bytes = base.ReadBytes(8);
            Array.Reverse(bytes);
            var value = BitConverter.ToUInt64(bytes, 0);
            return value;
        }

        public override float ReadSingle()
        {
            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            var value = BitConverter.ToSingle(bytes, 0);
            return value;
        }

        public override double ReadDouble()
        {
            var bytes = base.ReadBytes(8);
            Array.Reverse(bytes);
            var value = BitConverter.ToDouble(bytes, 0);
            return value;
        }

        #endregion


        public string ReadASCIIString(int count)
        {
            var bytes = base.ReadBytes(count);
            var value = Encoding.ASCII.GetString(bytes);
            return value;
        }

        /// <summary>
        /// 读取 Unicode 字符串，Unicode 字符串 = 4 字节字符数 + 字符串内容（长度为字符数的 2 倍）
        /// </summary>
        /// <returns></returns>
        public string ReadUnicodeString()
        {
            // 字符数量
            var count = ReadUInt32();
            var length = count * 2;
            var bytes = ReadBytes((int)length);
            var value = Encoding.BigEndianUnicode.GetString(bytes);
            return value;
        }

        /// <summary>
        /// 读取 Pascal 字符串，Pascal 字符串 = 1 个字节长度信息 + 字符串内容 + 对齐偏移
        /// </summary>
        /// <param name="factor">对齐因数（字符串长度需要为此数的倍数）</param>
        /// <returns></returns>
        public string ReadPascalString(uint factor)
        {
            var startPosition = BaseStream.Position;
            var count = (int)ReadByte();
            var bytes = ReadBytes(count);
            var padding = Utils.GetPadding((uint)(BaseStream.Position - startPosition), factor);
            ReadPadding(padding);
            var value = m_Encoding.GetString(bytes);
            return value;
        }

        /// <summary>
        /// 读取对齐偏移数据
        /// </summary>
        /// <param name="padding">偏移长度</param>
        /// <returns></returns>
        public byte[] ReadPadding(uint padding)
        {
            var value = ReadBytes((int)padding);
            return value;
        }


        public Rectangle ReadRectangle()
        {
            var top = ReadInt32();
            var left = ReadInt32();
            var bottom = ReadInt32();
            var right = ReadInt32();
            var value = new Rectangle(top, left, bottom, right);
            return value;
        }

        public Decimal16_16 ReadDecimal16_16()
        {
            var data = ReadUInt32();
            var value = new Decimal16_16(data);
            return value;
        }
    }
}
