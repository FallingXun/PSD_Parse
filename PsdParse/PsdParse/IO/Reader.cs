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

        }

        public Reader(Stream stream, Encoding encoding) : base(stream, encoding)
        {

        }

        public Reader(Stream stream, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen)
        {

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
        /// 读取 Pascal 字符串，Pascal 字符串 = 1 个字节长度信息 + 字符串内容 + 对齐偏移
        /// </summary>
        /// <param name="factor">对齐因数（字符串长度需要为此数的倍数）</param>
        /// <returns></returns>
        public string ReadPascalString(uint factor)
        {
            var startPosition = BaseStream.Position;
            var count = (int)ReadByte();
            var bytes = ReadBytes(count);
            BaseStream.Position += Utils.RoundUp((uint)(BaseStream.Position - startPosition), factor);
            var value = m_Encoding.GetString(bytes);
            return value;
        }
    }
}
