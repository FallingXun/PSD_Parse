
namespace PsdParse
{
    /// <summary>
    /// 图层记录的位信息
    /// </summary>
    public enum ELayerRecordFlag : byte
    {
        TransparencyProtected = 1 << 0,
        Visible = 1 << 1,
        Obsolete = 1 << 2,
        /// <summary>
        /// 标记 bit 4 是否有效，PhotoShip 5.0 后为 1
        /// </summary>
        Bit4Sign = 1 << 3,
        /// <summary>
        /// 与文档外观无关的像素数据
        /// </summary>
        PixelDataIrrelevantToAppearanceOfDocument = 1 << 4,
    }
}
