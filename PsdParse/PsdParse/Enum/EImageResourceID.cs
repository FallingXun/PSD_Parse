namespace PsdParse
{
    /// <summary>
    /// 图像资源ID类型，详情见 <a href='https://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577409_pgfId-1037450'>Adobe Photoshop File Formats Specification</a> 的  Image Resource IDs
    /// </summary>
    public enum EImageResourceID : short
    {
        /// <summary>
        /// 包含五个2字节值：通道数、行数、列数、深度和模式（已废弃，仅 Photoshop 2.0）
        /// </summary>
        ID_1000 = 1000,
        /// <summary>
        /// Mac 打印管理器打印信息记录
        /// </summary>
        MacPrintInfo = 1001,
        /// <summary>
        /// Mac 页面格式信息（已废弃，不再通过 Photoshop 阅读）
        /// </summary>
        MacPageFormatInfo = 1002,
        /// <summary>
        /// 颜色索引表（已废弃，仅 Photoshop 2.0）
        /// </summary>
        IndexColorTable = 1003,
        /// <summary>
        /// ResolutionInfo 结构
        /// </summary>
        ResolutionInfo = 1005,
        /// <summary>
        /// Alpha 通道的名称（Pascal 字符串）
        /// </summary>
        AlphaChannelNames = 1006,
        /// <summary>
        /// DisplayInfo 结构（已废弃，见 1077）
        /// </summary>
        DisplayInfo = 1007,
        /// <summary>
        /// 说明文字
        /// </summary>
        Caption = 1008,
        /// <summary>
        /// 边界信息，包含边界宽度的固定数字（2个字节实数，2个字节分数）和边界单位的2个字节（1=英寸，2=厘米，3=点，4=picas，5=列）
        /// </summary>
        BorderInfo = 1009,
        /// <summary>
        /// 背景颜色
        /// </summary>
        BackgroundColor = 1010,
        /// <summary>
        /// 打印标志，一系列单字节布尔值：标签、裁剪标记、颜色条、注册标记、负数、翻转、插值、标题、打印标志
        /// </summary>
        PrintFlags = 1011,
        /// <summary>
        /// 灰度和多通道半色调信息
        /// </summary>
        GrayscaleAndMultiChannelHalftoningInfo = 1012,
        /// <summary>
        /// 颜色半色调信息
        /// </summary>
        ColorHalftoningInfo = 1013,
        /// <summary>
        /// 双色半色调信息
        /// </summary>
        DuotoneHalftoningInfo = 1014,
        /// <summary>
        /// 灰度和多通道传递函数
        /// </summary>
        GrayscaleAndMultichannelTransferFunction = 1015,
        /// <summary>
        /// 颜色传递函数
        /// </summary>
        ColorTransferFunctions = 1016,
        /// <summary>
        /// 双色传递函数
        /// </summary>
        DuotoneTransferFunctions = 1017,
        /// <summary>
        /// 双色图像信息
        /// </summary>
        DuotoneImageInfo = 1018,
        /// <summary>
        /// 点范围的有效黑白值，两个字节
        /// </summary>
        EffectiveBlackAndWhite = 1019,
        /// <summary>
        /// 已废弃
        /// </summary>
        ID_1020 = 1020,
        /// <summary>
        /// EPS 选项
        /// </summary>
        EPSOptions = 1021,
        /// <summary>
        /// 快速掩码信息，2个字节包含快速掩码通道ID，1字节布尔值，指示掩码最初是否为空
        /// </summary>
        QuickMaskInfo = 1022,
        /// <summary>
        /// 已废弃
        /// </summary>
        ID_1023 = 1023,
        /// <summary>
        /// 图层状态信息，2个字节包含目标层的索引（0=底层）
        /// </summary>
        LayerStateInfo = 1024,
        /// <summary>
        /// 工作路径（未保存）
        /// </summary>
        WorkingPath = 1025,
        /// <summary>
        /// 图层组信息，每层2个字节，包含拖动组的组ID。组中的层具有相同的组ID
        /// </summary>
        LayersGroupInformation = 1026,
        /// <summary>
        /// 已废弃
        /// </summary>
        ID_1027 = 1027,
        /// <summary>
        /// IPTC-NAA记录，包含 File Info... 的信息
        /// </summary>
        IPTCNAARecord = 1028,
        /// <summary>
        /// 原始格式文件的图像模式
        /// </summary>
        ImageModeForRawFormatFiles = 1029,
        /// <summary>
        /// JPEG 品质
        /// </summary>
        JPEGQuality = 1030,

        #region Photoshop 4.0 

        /// <summary>
        /// 网格和引导信息（Photoshop 4.0）
        /// </summary>
        GridAndGuidesInfo_PS4 = 1032,
        /// <summary>
        /// 缩略图资源（Photoshop 4.0）
        /// </summary>
        ThumbnailResource_PS4 = 1033,
        /// <summary>
        /// 版权标志，布尔值，指示图像是否受版权保护，可以通过属性套件或用户在 File Info... 中设置（Photoshop 4.0）
        /// </summary>
        CopyrightFlag_PS4 = 1034,
        /// <summary>
        /// URL，使用统一资源定位符处理文本字符串，可以通过属性套件或用户在 File Info... 中设置（Photoshop 4.0）
        /// </summary>
        URL_PS4 = 1035,

        #endregion

        #region Photoshop 5.0 

        /// <summary>
        /// 缩略图资源，取代 1033（Photoshop 5.0）
        /// </summary>
        ThumbnailResource_PS5 = 1036,
        /// <summary>
        /// 全局角度，4个字节，包含0到359之间的整数，这是效果层的全局照明角度，如果不存在，则假定为30（Photoshop 5.0）
        /// </summary>
        GlobalAngle_PS5 = 1037,
        /// <summary>
        /// 颜色采样器资源，已废弃，新见 1073（Photoshop 5.0）
        /// </summary>
        ColorSamplersResource_PS5 = 1038,
        /// <summary>
        /// ICC配置文件。ICC（国际色彩联盟）格式配置文件的原始字节（Photoshop 5.0）
        /// </summary>
        ICCProfile_PS5 = 1039,
        /// <summary>
        /// 水印，1个字节（Photoshop 5.0）
        /// </summary>
        Watermark_PS5 = 1040,
        /// <summary>
        /// ICC未标记配置文件，1个字节，用于在打开文件时禁用任何假定的配置文件处理，1=故意不加标签（Photoshop 5.0）
        /// </summary>
        ICCUntaggedProfile_PS5 = 1041,
        /// <summary>
        /// 效果可见，1字节全局标志，用于显示/隐藏所有效果层，只有当它们被隐藏时才存在（Photoshop 5.0）
        /// </summary>
        EffectsVisible_PS5 = 1042,
        /// <summary>
        /// 点半色调，4个字节的版本号，4个字节的数据长度，指定长度数据（Photoshop 5.0）
        /// </summary>
        SpotHalftone_PS5 = 1043,
        /// <summary>
        /// 文档特定ID种子编号，4字节：基值，从生成层ID开始（如果现有ID已经超过该值，则为更大的值）。它的目的是避免我们添加层、展开、保存、打开，然后添加更多最终与第一组ID相同的层（Photoshop 5.0）
        /// </summary>
        DocumentSpecificIDsSeedNumber_PS5 = 1044,
        /// <summary>
        /// Unicode 字母名称，Unicode字符串（Photoshop 5.0）
        /// </summary>
        UnicodeAlphaNames_PS5 = 1045,

        #endregion

        #region Photoshop 6.0

        /// <summary>
        /// 索引颜色表计数，表中实际定义的颜色数量为2个字节（Photoshop 6.0）
        /// </summary>
        IndexedColorTableCount_PS6 = 1046,
        /// <summary>
        /// 透明度指数，透明颜色索引为2个字节（如果有）（Photoshop 6.0）
        /// </summary>
        TransparencyIndex_PS6 = 1047,
        /// <summary>
        /// 全局高度，4字节输入（Photoshop 6.0）
        /// </summary>
        GlobalAltitude_PS6 = 1049,
        /// <summary>
        /// 切片（Photoshop 6.0）
        /// </summary>
        Slices_PS6 = 1050,
        /// <summary>
        /// 工作流 URL，Unicode字符串（Photoshop 6.0）
        /// </summary>
        WorkflowURL_PS6 = 1051,
        /// <summary>
        /// 跳转到XPEP，2字节主版本，2字节次版本，4字节计数。重复以下计数：4字节块大小，4字节密钥，如果key='jtDd'，则接下来是脏标志的布尔值；否则，它是一个4字节的mod日期条目（Photoshop 6.0）
        /// </summary>
        JumpToXPEP_PS6 = 1052,
        /// <summary>
        /// Alpha 标识符，长度为4个字节，每个字母标识符后面各4个字节（Photoshop 6.0）
        /// </summary>
        AlphaIdentifiers_PS6 = 1053,
        /// <summary>
        /// URL 列表，4字节的URL数量，后面是4字节长整型、4字节ID和每个计数的Unicode字符串（Photoshop 6.0）
        /// </summary>
        URLList_PS6 = 1054,
        /// <summary>
        /// 版本信息，4字节版本，1字节有RealMergedData，Unicode字符串：写入者名称，Unicode字符串-读取器名称，4字节文件版本（Photoshop 6.0）
        /// </summary>
        VersionInfo_PS6 = 1057,

        #endregion

        #region Photoshop 7.0

        /// <summary>
        /// EXIF 数据 1（Photoshop 7.0）
        /// </summary>
        EXIFData1_PS7 = 1058,
        /// <summary>
        /// EXIF 数据 3（Photoshop 7.0）
        /// </summary>
        EXIFData3_PS7 = 1059,
        /// <summary>
        /// XMP 元数据，文件信息为XML描述（Photoshop 7.0）
        /// </summary>
        XMPMetadata_PS7 = 1060,
        /// <summary>
        /// 标题摘要，16字节：RSA数据安全，MD5消息摘要算法（Photoshop 7.0）
        /// </summary>
        CaptionDigest_PS7 = 1061,
        /// <summary>
        /// 打印比例，2字节样式（0=居中，1=适合的大小，2=用户定义）。4字节x位置（浮点）。4字节y位置（浮点）。4字节刻度（浮点）（Photoshop 7.0）
        /// </summary>
        PrintScale_PS7 = 1062,

        #endregion

        #region Photoshop CS 

        /// <summary>
        /// 像素纵横比。4个字节（版本=1或2），8个字节双倍，x/y像素（Photoshop CS）
        /// </summary>
        PixelAspectRatio_PSCS = 1064,
        /// <summary>
        /// 层组件。4个字节（描述符版本=16）（Photoshop CS）
        /// </summary>
        LayerComps_PSCS = 1065,
        /// <summary>
        /// 交替的双色调颜色。2个字节（版本=1），2个字节计数，每次计数都重复以下内容：[颜色：2个字节用于空间，后面是4*2个字节的颜色分量]，接下来是另一个2字节计数，通常为256，后面是Lab颜色，L、a、b各一个字节。Photoshop不会读取或使用此资源（Photoshop CS）
        /// </summary>
        AlternateDuotoneColors_PSCS = 1066,
        /// <summary>
        /// 替代专色。2字节（版本=1），2字节通道计数，每次计数重复以下内容：4字节通道ID，颜色：2字节用于空间，后面是4*2字节的颜色分量。Photoshop不会读取或使用此资源（Photoshop CS）
        /// </summary>
        AlternateSpotColors_PSCS = 1067,

        #endregion

        #region Photoshop CS2 
        /// <summary>
        /// 图层选择ID。2字节计数，每次计数重复以下内容：4字节层ID（Photoshop CS2）
        /// </summary>
        LayerSelectionID_PSCS2 = 1069,
        /// <summary>
        /// HDR调色信息（Photoshop CS2）
        /// </summary>
        HDRToningInfo_PSCS2 = 1070,
        /// <summary>
        /// 打印信息（Photoshop CS2）
        /// </summary>
        PrintInfo_PSCS2 = 1071,
        /// <summary>
        /// 已启用层组ID。文档中每层1个字节，按资源长度重复。注意：图层组有开始和结束标记（Photoshop CS2）
        /// </summary>
        LayerGroupEnabledID_PSCS2 = 1072,

        #endregion

        #region Photoshop CS3

        /// <summary>
        /// 颜色采样器资源（Photoshop CS3）
        /// </summary>
        ColorSamplersResource_PSCS3 = 1073,
        /// <summary>
        /// 测量刻度。4个字节（描述符版本=16）（Photoshop CS3）
        /// </summary>
        MeasurementScale_PSCS3 = 1074,
        /// <summary>
        /// 时间线信息。4个字节（描述符版本=16）（Photoshop CS3）
        /// </summary>
        TimelineInfo_PSCS3 = 1075,
        /// <summary>
        /// 表格披露。4个字节（描述符版本=16）（Photoshop CS3）
        /// </summary>
        SheetDisclosure_PSCS3 = 1076,
        /// <summary>
        /// DisplayInfo 结构，支持浮点颜色（Photoshop CS3）
        /// </summary>
        DisplayInfo_PSCS3 = 1077,
        /// <summary>
        /// Onion 皮肤，4个字节（描述符版本=16）（Photoshop CS3）
        /// </summary>
        OnionSkins_PSCS3 = 1078,
        /// <summary>
        /// Lightroom工作流，如果存在，则文档处于Lightroom工作流程的中间。
        /// </summary>
        LightroomWorkflows_PSCS3 = 8000,

        #endregion

        #region Photoshop CS4
        /// <summary>
        /// 计数信息。4个字节（描述符版本=16）（Photoshop CS4）
        /// </summary>
        CountInfo_PSCS4 = 1080,

        #endregion

        #region Photoshop CS5

        /// <summary>
        /// 打印信息。4个字节（描述符版本=16）（Photoshop CS5）
        /// </summary>
        PrintInfo_PSCS5 = 1082,
        /// <summary>
        /// 打印样式。4个字节（描述符版本=16）（Photoshop CS5）
        /// </summary>
        PrintStyle_PSCS5 = 1083,
        /// <summary>
        /// MacNSPrint信息（Photoshop CS5）
        /// </summary>
        MacNSPrintInfo_PSCS5 = 1084,
        /// <summary>
        /// Windows开发模式（Photoshop CS5）
        /// </summary>
        WindowsDEVMODE_PSCS5 = 1085,

        #endregion

        #region Photoshop CS6
        /// <summary>
        /// 自动保存文件路径，建议不要使用（Photoshop CS6）
        /// </summary>
        AutoSaveFilePath_PSCS6 = 1086,
        /// <summary>
        /// 自动保存格式，建议不要使用（Photoshop CS6）
        /// </summary>
        AutoSaveFormat_PSCS6 = 1087,

        #endregion

        #region Photoshop CC
        /// <summary>
        /// 路径选择状态。4个字节（描述符版本=16）（Photoshop CC）
        /// </summary>
        PathSelectionState_PSCC = 1088,
        /// <summary>
        /// 原点路径信息。4个字节（描述符版本=16）（Photoshop CC）
        /// </summary>
        OriginPathInfo_PSCC = 3000,

        #endregion

        /// <summary>
        /// 路径信息 start（保存的路径）
        /// </summary>
        PathInfoStart = 2000,
        /// <summary>
        /// 路径信息 end（保存的路径）
        /// </summary>
        PathInfoEnd = 2997,
        /// <summary>
        /// 剪切路径名
        /// </summary>
        ClippingPathName = 2999,
        /// <summary>
        /// 插件资源 start
        /// </summary>
        PluginResourceStart = 4000,
        /// <summary>
        /// 插件资源 end
        /// </summary>
        PluginResourceEnd = 4999,
        /// <summary>
        /// Image Ready 变量，变量定义的XML表示
        /// </summary>
        ImageReadyVariables = 7000,
        /// <summary>
        /// Image Ready 数据集
        /// </summary>
        ImageReadyDataSets = 7001,
        /// <summary>
        /// Image Ready 默认选择状态
        /// </summary>
        ImageReadyDefaultSelectedState = 7002,
        /// <summary>
        /// Image Ready 7滚动扩展状态
        /// </summary>
        ImageReady7RolloverExpandedState = 7003,
        /// <summary>
        /// Image Ready 滚动扩展状态
        /// </summary>
        ImageReadyRolloverExpandedState = 7004,
        /// <summary>
        /// Image Ready 保存层设置
        /// </summary>
        ImageReadySaveLayerSettings = 7005,
        /// <summary>
        /// Image Ready 版本
        /// </summary>
        ImageReadyVersion = 7006,

        /// <summary>
        /// 打印标志信息。2字节版本（=1），1字节中心裁剪标记，1字节（=0），4字节出血宽度值，2字节出血宽度刻度
        /// </summary>
        PrintFlagsInfo = 10000,
    }

}