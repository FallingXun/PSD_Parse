
namespace PsdParse
{
    /// <summary>
    /// 通道 ID
    /// </summary>
    public enum EChannelID : short
    {
        /// <summary>
        /// 用户蒙版和矢量蒙版同时存在
        /// </summary>
        UserMaskAndVectorMask = -3,
        /// <summary>
        /// 用户蒙版或矢量蒙版
        /// </summary>
        UserMaskOrVectorMask = -2,
        /// <summary>
        /// 透明蒙版
        /// </summary>
        TransparentMask = -1,
        R = 0,
        G = 1,
        B = 2,
    }
}
