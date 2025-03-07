namespace PsdParse
{
    /// <summary>
    /// 压缩格式
    /// </summary>
    public enum ECompression : ushort
    {
        /// <summary>
        /// 无压缩
        /// </summary>
        RawData = 0,
        /// <summary>
        /// RLE压缩
        /// </summary>
        RLECompression = 1,
        /// <summary>
        /// 无预测 ZIP 压缩
        /// </summary>
        ZipWithoutPrediction = 2,
        /// <summary>
        /// 有预测 ZIP 压缩
        /// </summary>
        ZipWithPrediction = 3,
    }
}
