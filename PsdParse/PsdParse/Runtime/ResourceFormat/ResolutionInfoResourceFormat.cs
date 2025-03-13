using System;


namespace PsdParse
{
    public class ResolutionInfoResourceFormat : IStreamHandler
    {
        /// <summary>
        /// 高精度分辨率（4 字节），是用于描述屏幕显示精度的单位，表示每单位（<see cref="EResolutionUnit"/>）长度上所能显示的像素点的数量。
        /// </summary>
        public Decimal16_16 HDPI
        {
            get; set;
        }

        /// <summary>
        /// HDPI 的分辨率单位（2 字节）
        /// </summary>
        public EResolutionUnit HResolutionDisplayUnit
        {
            get; set;
        }

        /// <summary>
        /// 宽度单位（2 字节）
        /// </summary>
        public EUnit WidthDisplayUnit
        {
            get; set;
        }

        /// <summary>
        /// 虚拟像素密度（4 字节），安卓系统会根据设备的屏幕大小、分辨率以及用户的设置等因素，计算出一个合适的 VDPI 值
        /// </summary>
        public Decimal16_16 VDPI
        {
            get; set;
        }

        /// <summary>
        /// VDPI 的分辨率单位（2 字节）
        /// </summary>
        public EResolutionUnit VResolutionDisplayUnit
        {
            get; set;
        }

        /// <summary>
        /// 高度单位（2 字节）
        /// </summary>
        public EUnit HeightDisplayUnit
        {
            get; set;
        }
        
        public void Parse(Reader reader)
        {
            HDPI = reader.ReadDecimal16_16();
            HResolutionDisplayUnit = (EResolutionUnit)reader.ReadUInt16();
            WidthDisplayUnit = (EUnit)reader.ReadUInt16();
            VDPI = reader.ReadDecimal16_16();
            VResolutionDisplayUnit = (EResolutionUnit)reader.ReadUInt16();
            HeightDisplayUnit = (EUnit)reader.ReadUInt16();
        }

        public void Combine(Writer writer)
        {
            writer.WriteDecimal16_16(HDPI);
            writer.WriteUInt16((ushort)HResolutionDisplayUnit);
            writer.WriteUInt16((ushort)WidthDisplayUnit);
            writer.WriteDecimal16_16(VDPI);
            writer.WriteUInt16((ushort)VResolutionDisplayUnit);
            writer.WriteUInt16((ushort)HeightDisplayUnit);
        }


        public int CalculateLength(Calculator calculator)
        {
            var length = 0;

            length += calculator.CalculateDecimal16_16(HDPI);
            length += calculator.CalculateUInt16((ushort)HResolutionDisplayUnit);
            length += calculator.CalculateUInt16((ushort)WidthDisplayUnit);
            length += calculator.CalculateDecimal16_16(VDPI);
            length += calculator.CalculateUInt16((ushort)VResolutionDisplayUnit);
            length += calculator.CalculateUInt16((ushort)HeightDisplayUnit);

            return length;
        }
    }
}
