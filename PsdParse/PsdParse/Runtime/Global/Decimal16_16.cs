using System;


namespace PsdParse
{
    /// <summary>
    /// Decimal 结构，16 位整数 + 16 位小数
    /// </summary>
    public struct Decimal16_16
    {
        public ushort Integer { get; set; }
        public ushort Fraction { get; set; }

        public Decimal16_16(ushort integer, ushort fraction)
        {
            Integer = integer;
            Fraction = fraction;
        }

        public Decimal16_16(uint value)
        {
            Integer = (ushort)(value >> 16);
            Fraction = (ushort)(value & 0x0000FFFF);
        }

        public Decimal16_16(double value)
        {
            if (value >= 65536.0d || value < 0d)
            {
                throw new Exception(string.Format("Decimal16_16 数据超出范围，value : {0}", value));
            }
            Integer = (ushort)value;
            Fraction = (ushort)((value - Integer) * 65536 + 0.5);
        }

        public static implicit operator uint(Decimal16_16 value)
        {
            return (uint)value.Integer << 16 + value.Fraction;
        }

        public static implicit operator double(Decimal16_16 value)
        {
            return value.Integer + value.Fraction / 65536.0d;
        }
    }
}
