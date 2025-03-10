namespace PsdParse
{
    /// <summary>
    /// 图层蒙版的位信息
    /// </summary>
    public enum ELayerMaskFlag : byte
    {
        PositionRelativeToLayer = 1 << 0,
        LayerMaskDisabled = 1 << 1,
        /// <summary>
        /// 已废弃
        /// </summary>
        InvertLayerMaskWhenBlending = 1 << 2,
        /// <summary>
        /// 表示用户蒙版实际上来自渲染其他数据
        /// </summary>
        UserMaskCameFromRenderingOtherData = 1 << 3,
        /// <summary>
        /// 表示用户和/或矢量蒙版具有应用于它们的参数
        /// </summary>
        UserVectorMasksHaveParamsApplied = 1 << 4,
    }
}
