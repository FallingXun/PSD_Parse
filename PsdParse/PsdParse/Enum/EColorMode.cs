namespace PsdParse
{/// <summary>
 /// 文件的颜色模式
 /// </summary>
    public enum EColorMode : short
    {
        /// <summary>
        /// 位图
        /// </summary>
        Bitmap = 0,
        /// <summary>
        /// 灰度
        /// </summary>
        Grayscale = 1,
        /// <summary>
        /// 索引
        /// </summary>
        Indexed = 2,
        /// <summary>
        /// RGB
        /// </summary>
        RGB = 3,
        /// <summary>
        /// CMYK
        /// </summary>
        CMYK = 4,
        /// <summary>
        /// 多通道
        /// </summary>
        Multichannel = 7,
        /// <summary>
        /// 双音
        /// </summary>
        Duotone = 8,
        /// <summary>
        /// 实验室
        /// </summary>
        Lab = 9,
    }
}