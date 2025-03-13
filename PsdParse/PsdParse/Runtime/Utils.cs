namespace PsdParse
{
    public class Utils
    {
        /// <summary>
        /// 向上取因数的倍数
        /// </summary>
        /// <param name="value">初始值</param>
        /// <param name="factor">因数</param>
        /// <returns></returns>
        public static uint RoundUp(uint value, uint factor)
        {
            return (value / factor + (value % factor > 0 ? 1u : 0)) * factor;
        }

        /// <summary>
        /// 获取偏移字节数
        /// </summary>
        /// <param name="length">原长度</param>
        /// <param name="factor">因数(即 <see cref="RoundUp"/> 的 factor)</param>
        /// <returns></returns>
        public static uint GetPadding(uint length, uint factor)
        {
            var paddingLength = RoundUp(length, factor);
            var padding = paddingLength - length;
            return padding;
        }
    }
}
