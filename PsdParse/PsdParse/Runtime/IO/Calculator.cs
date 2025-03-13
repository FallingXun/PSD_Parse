using System;
using System.Text;

namespace PsdParse
{
    public class Calculator
    {
        private Encoding m_Encoding;

        public Calculator()
        {
            m_Encoding = Encoding.Default;
        }

        public Calculator(Encoding encoding)
        {
            m_Encoding = encoding;
        }

        public int CalculateByte(byte value)
        {
            return 1;
        }

        public int CalculateBytes(byte[] value)
        {
            if (value == null)
            {
                return 0;
            }
            return value.Length;
        }


        public int CalculateInt16(short value)
        {
            return 2;
        }

        public int CalculateUInt16(ushort value)
        {
            return 2;
        }

        public int CalculateInt32(int value)
        {
            return 4;
        }

        public int CalculateUInt32(uint value)
        {
            return 4;
        }

        public int CalculateInt64(long value)
        {
            return 8;
        }

        public int CalculateUInt64(ulong value)
        {
            return 8;
        }

        public int CalculateSingle(float value)
        {
            return 4;
        }

        public int CalculateDouble(double value)
        {
            return 8;
        }

        public int CalculateASCIIString(string value, int length)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            if (bytes.Length != length)
            {
                throw new Exception(string.Format("CalculateASCIIString 错误，数据长度：{0}，length：{1}", bytes.Length, length));
            }
            return length;
        }

        /// <summary>
        /// 计算 Unicode 字符串，Unicode 字符串 = 4 字节字符数 + 字符串内容（长度为字符数的 2 倍）
        /// </summary>
        /// <returns></returns>
        public int CalculateUnicodeString(string value)
        {
            var bytes = Encoding.BigEndianUnicode.GetBytes(value);
            return 4 + bytes.Length;
        }


        /// <summary>
        /// 计算 Pascal 字符串，Pascal 字符串 = 1 个字节长度信息 + 字符串内容 + 对齐偏移
        /// </summary>
        /// <param name="factor">对齐因数（字符串长度需要为此数的倍数）</param>
        /// <returns></returns>
        public int CalculatePascalString(string value, uint factor)
        {
            var bytes = m_Encoding.GetBytes(value);
            var padding = Utils.GetPadding((uint)(bytes.Length + 1), factor);
            return 1 + bytes.Length + (int)padding;
        }


        /// <summary>
        /// 计算对齐偏移数据
        /// </summary>
        /// <param name="padding">偏移长度</param>
        /// <returns></returns>
        public int CalculatePadding(uint padding)
        {
            return (int)padding;
        }

        public int CalculateRectangle(Rectangle value)
        {
            return 4 * 4;
        }

        public int CalculateDecimal16_16(Decimal16_16 value)
        {
            return 4;
        }
    }
}
