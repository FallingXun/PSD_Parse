namespace PsdParse
{
    /// <summary>
    /// 每个通道的比特数,支持的值为1、8、16、32。
    /// </summary>
    public enum EDepth
    {
        Unknown,
        Bit_1 = 1,
        Bit_8 = 8,
        Bit_16 = 16,
        Bit_32 = 32,
    }
}