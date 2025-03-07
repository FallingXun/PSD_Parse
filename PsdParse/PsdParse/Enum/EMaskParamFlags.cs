namespace PsdParse
{
    /// <summary>
    /// 层蒙版的蒙版参数位信息
    /// </summary>
    public enum EMaskParamFlags : byte
    {
        /// <summary>
        /// 用户蒙版密度（1 字节）
        /// </summary>
        UserMaskDensity = 1 << 0,
        /// <summary>
        /// 用户蒙版羽化（8 字节），double 类型
        /// </summary>
        UserMaskFeather = 1 << 1,
        /// <summary>
        /// 矢量蒙版密度（1 字节）
        /// </summary>
        VectorMaskDensity = 1 << 2,
        /// <summary>
        /// 矢量蒙版羽化（8 字节） double 类型
        /// </summary>
        VectorMaskFeather = 1 << 3,
    }
}
