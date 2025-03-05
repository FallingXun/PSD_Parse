namespace PsdParse
{
    /// <summary>
    /// ͼ����ԴID���ͣ������ <a href='https://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577409_pgfId-1037450'>Adobe Photoshop File Formats Specification</a> ��  Image Resource IDs
    /// </summary>
    public enum EImageResourceID : short
    {
        /// <summary>
        /// �������2�ֽ�ֵ��ͨ��������������������Ⱥ�ģʽ���ѷ������� Photoshop 2.0��
        /// </summary>
        ID_1000 = 1000,
        /// <summary>
        /// Mac ��ӡ��������ӡ��Ϣ��¼
        /// </summary>
        MacPrintInfo = 1001,
        /// <summary>
        /// Mac ҳ���ʽ��Ϣ���ѷ���������ͨ�� Photoshop �Ķ���
        /// </summary>
        MacPageFormatInfo = 1002,
        /// <summary>
        /// ��ɫ�������ѷ������� Photoshop 2.0��
        /// </summary>
        IndexColorTable = 1003,
        /// <summary>
        /// ResolutionInfo �ṹ
        /// </summary>
        ResolutionInfo = 1005,
        /// <summary>
        /// Alpha ͨ�������ƣ�Pascal �ַ�����
        /// </summary>
        AlphaChannelNames = 1006,
        /// <summary>
        /// DisplayInfo �ṹ���ѷ������� 1077��
        /// </summary>
        DisplayInfo = 1007,
        /// <summary>
        /// ˵������
        /// </summary>
        Caption = 1008,
        /// <summary>
        /// �߽���Ϣ�������߽��ȵĹ̶����֣�2���ֽ�ʵ����2���ֽڷ������ͱ߽絥λ��2���ֽڣ�1=Ӣ�磬2=���ף�3=�㣬4=picas��5=�У�
        /// </summary>
        BorderInfo = 1009,
        /// <summary>
        /// ������ɫ
        /// </summary>
        BackgroundColor = 1010,
        /// <summary>
        /// ��ӡ��־��һϵ�е��ֽڲ���ֵ����ǩ���ü���ǡ���ɫ����ע���ǡ���������ת����ֵ�����⡢��ӡ��־
        /// </summary>
        PrintFlags = 1011,
        /// <summary>
        /// �ҶȺͶ�ͨ����ɫ����Ϣ
        /// </summary>
        GrayscaleAndMultiChannelHalftoningInfo = 1012,
        /// <summary>
        /// ��ɫ��ɫ����Ϣ
        /// </summary>
        ColorHalftoningInfo = 1013,
        /// <summary>
        /// ˫ɫ��ɫ����Ϣ
        /// </summary>
        DuotoneHalftoningInfo = 1014,
        /// <summary>
        /// �ҶȺͶ�ͨ�����ݺ���
        /// </summary>
        GrayscaleAndMultichannelTransferFunction = 1015,
        /// <summary>
        /// ��ɫ���ݺ���
        /// </summary>
        ColorTransferFunctions = 1016,
        /// <summary>
        /// ˫ɫ���ݺ���
        /// </summary>
        DuotoneTransferFunctions = 1017,
        /// <summary>
        /// ˫ɫͼ����Ϣ
        /// </summary>
        DuotoneImageInfo = 1018,
        /// <summary>
        /// �㷶Χ����Ч�ڰ�ֵ�������ֽ�
        /// </summary>
        EffectiveBlackAndWhite = 1019,
        /// <summary>
        /// �ѷ���
        /// </summary>
        ID_1020 = 1020,
        /// <summary>
        /// EPS ѡ��
        /// </summary>
        EPSOptions = 1021,
        /// <summary>
        /// ����������Ϣ��2���ֽڰ�����������ͨ��ID��1�ֽڲ���ֵ��ָʾ��������Ƿ�Ϊ��
        /// </summary>
        QuickMaskInfo = 1022,
        /// <summary>
        /// �ѷ���
        /// </summary>
        ID_1023 = 1023,
        /// <summary>
        /// ͼ��״̬��Ϣ��2���ֽڰ���Ŀ����������0=�ײ㣩
        /// </summary>
        LayerStateInfo = 1024,
        /// <summary>
        /// ����·����δ���棩
        /// </summary>
        WorkingPath = 1025,
        /// <summary>
        /// ͼ������Ϣ��ÿ��2���ֽڣ������϶������ID�����еĲ������ͬ����ID
        /// </summary>
        LayersGroupInformation = 1026,
        /// <summary>
        /// �ѷ���
        /// </summary>
        ID_1027 = 1027,
        /// <summary>
        /// IPTC-NAA��¼������ File Info... ����Ϣ
        /// </summary>
        IPTCNAARecord = 1028,
        /// <summary>
        /// ԭʼ��ʽ�ļ���ͼ��ģʽ
        /// </summary>
        ImageModeForRawFormatFiles = 1029,
        /// <summary>
        /// JPEG Ʒ��
        /// </summary>
        JPEGQuality = 1030,

        #region Photoshop 4.0 

        /// <summary>
        /// �����������Ϣ��Photoshop 4.0��
        /// </summary>
        GridAndGuidesInfo_PS4 = 1032,
        /// <summary>
        /// ����ͼ��Դ��Photoshop 4.0��
        /// </summary>
        ThumbnailResource_PS4 = 1033,
        /// <summary>
        /// ��Ȩ��־������ֵ��ָʾͼ���Ƿ��ܰ�Ȩ����������ͨ�������׼����û��� File Info... �����ã�Photoshop 4.0��
        /// </summary>
        CopyrightFlag_PS4 = 1034,
        /// <summary>
        /// URL��ʹ��ͳһ��Դ��λ�������ı��ַ���������ͨ�������׼����û��� File Info... �����ã�Photoshop 4.0��
        /// </summary>
        URL_PS4 = 1035,

        #endregion

        #region Photoshop 5.0 

        /// <summary>
        /// ����ͼ��Դ��ȡ�� 1033��Photoshop 5.0��
        /// </summary>
        ThumbnailResource_PS5 = 1036,
        /// <summary>
        /// ȫ�ֽǶȣ�4���ֽڣ�����0��359֮�������������Ч�����ȫ�������Ƕȣ���������ڣ���ٶ�Ϊ30��Photoshop 5.0��
        /// </summary>
        GlobalAngle_PS5 = 1037,
        /// <summary>
        /// ��ɫ��������Դ���ѷ������¼� 1073��Photoshop 5.0��
        /// </summary>
        ColorSamplersResource_PS5 = 1038,
        /// <summary>
        /// ICC�����ļ���ICC������ɫ�����ˣ���ʽ�����ļ���ԭʼ�ֽڣ�Photoshop 5.0��
        /// </summary>
        ICCProfile_PS5 = 1039,
        /// <summary>
        /// ˮӡ��1���ֽڣ�Photoshop 5.0��
        /// </summary>
        Watermark_PS5 = 1040,
        /// <summary>
        /// ICCδ��������ļ���1���ֽڣ������ڴ��ļ�ʱ�����κμٶ��������ļ�����1=���ⲻ�ӱ�ǩ��Photoshop 5.0��
        /// </summary>
        ICCUntaggedProfile_PS5 = 1041,
        /// <summary>
        /// Ч���ɼ���1�ֽ�ȫ�ֱ�־��������ʾ/��������Ч���㣬ֻ�е����Ǳ�����ʱ�Ŵ��ڣ�Photoshop 5.0��
        /// </summary>
        EffectsVisible_PS5 = 1042,
        /// <summary>
        /// ���ɫ����4���ֽڵİ汾�ţ�4���ֽڵ����ݳ��ȣ�ָ���������ݣ�Photoshop 5.0��
        /// </summary>
        SpotHalftone_PS5 = 1043,
        /// <summary>
        /// �ĵ��ض�ID���ӱ�ţ�4�ֽڣ���ֵ�������ɲ�ID��ʼ���������ID�Ѿ�������ֵ����Ϊ�����ֵ��������Ŀ���Ǳ���������Ӳ㡢չ�������桢�򿪣�Ȼ����Ӹ����������һ��ID��ͬ�Ĳ㣨Photoshop 5.0��
        /// </summary>
        DocumentSpecificIDsSeedNumber_PS5 = 1044,
        /// <summary>
        /// Unicode ��ĸ���ƣ�Unicode�ַ�����Photoshop 5.0��
        /// </summary>
        UnicodeAlphaNames_PS5 = 1045,

        #endregion

        #region Photoshop 6.0

        /// <summary>
        /// ������ɫ�����������ʵ�ʶ������ɫ����Ϊ2���ֽڣ�Photoshop 6.0��
        /// </summary>
        IndexedColorTableCount_PS6 = 1046,
        /// <summary>
        /// ͸����ָ����͸����ɫ����Ϊ2���ֽڣ�����У���Photoshop 6.0��
        /// </summary>
        TransparencyIndex_PS6 = 1047,
        /// <summary>
        /// ȫ�ָ߶ȣ�4�ֽ����루Photoshop 6.0��
        /// </summary>
        GlobalAltitude_PS6 = 1049,
        /// <summary>
        /// ��Ƭ��Photoshop 6.0��
        /// </summary>
        Slices_PS6 = 1050,
        /// <summary>
        /// ������ URL��Unicode�ַ�����Photoshop 6.0��
        /// </summary>
        WorkflowURL_PS6 = 1051,
        /// <summary>
        /// ��ת��XPEP��2�ֽ����汾��2�ֽڴΰ汾��4�ֽڼ������ظ����¼�����4�ֽڿ��С��4�ֽ���Կ�����key='jtDd'��������������־�Ĳ���ֵ����������һ��4�ֽڵ�mod������Ŀ��Photoshop 6.0��
        /// </summary>
        JumpToXPEP_PS6 = 1052,
        /// <summary>
        /// Alpha ��ʶ��������Ϊ4���ֽڣ�ÿ����ĸ��ʶ�������4���ֽڣ�Photoshop 6.0��
        /// </summary>
        AlphaIdentifiers_PS6 = 1053,
        /// <summary>
        /// URL �б�4�ֽڵ�URL������������4�ֽڳ����͡�4�ֽ�ID��ÿ��������Unicode�ַ�����Photoshop 6.0��
        /// </summary>
        URLList_PS6 = 1054,
        /// <summary>
        /// �汾��Ϣ��4�ֽڰ汾��1�ֽ���RealMergedData��Unicode�ַ�����д�������ƣ�Unicode�ַ���-��ȡ�����ƣ�4�ֽ��ļ��汾��Photoshop 6.0��
        /// </summary>
        VersionInfo_PS6 = 1057,

        #endregion

        #region Photoshop 7.0

        /// <summary>
        /// EXIF ���� 1��Photoshop 7.0��
        /// </summary>
        EXIFData1_PS7 = 1058,
        /// <summary>
        /// EXIF ���� 3��Photoshop 7.0��
        /// </summary>
        EXIFData3_PS7 = 1059,
        /// <summary>
        /// XMP Ԫ���ݣ��ļ���ϢΪXML������Photoshop 7.0��
        /// </summary>
        XMPMetadata_PS7 = 1060,
        /// <summary>
        /// ����ժҪ��16�ֽڣ�RSA���ݰ�ȫ��MD5��ϢժҪ�㷨��Photoshop 7.0��
        /// </summary>
        CaptionDigest_PS7 = 1061,
        /// <summary>
        /// ��ӡ������2�ֽ���ʽ��0=���У�1=�ʺϵĴ�С��2=�û����壩��4�ֽ�xλ�ã����㣩��4�ֽ�yλ�ã����㣩��4�ֽڿ̶ȣ����㣩��Photoshop 7.0��
        /// </summary>
        PrintScale_PS7 = 1062,

        #endregion

        #region Photoshop CS 

        /// <summary>
        /// �����ݺ�ȡ�4���ֽڣ��汾=1��2����8���ֽ�˫����x/y���أ�Photoshop CS��
        /// </summary>
        PixelAspectRatio_PSCS = 1064,
        /// <summary>
        /// �������4���ֽڣ��������汾=16����Photoshop CS��
        /// </summary>
        LayerComps_PSCS = 1065,
        /// <summary>
        /// �����˫ɫ����ɫ��2���ֽڣ��汾=1����2���ֽڼ�����ÿ�μ������ظ��������ݣ�[��ɫ��2���ֽ����ڿռ䣬������4*2���ֽڵ���ɫ����]������������һ��2�ֽڼ�����ͨ��Ϊ256��������Lab��ɫ��L��a��b��һ���ֽڡ�Photoshop�����ȡ��ʹ�ô���Դ��Photoshop CS��
        /// </summary>
        AlternateDuotoneColors_PSCS = 1066,
        /// <summary>
        /// ���רɫ��2�ֽڣ��汾=1����2�ֽ�ͨ��������ÿ�μ����ظ��������ݣ�4�ֽ�ͨ��ID����ɫ��2�ֽ����ڿռ䣬������4*2�ֽڵ���ɫ������Photoshop�����ȡ��ʹ�ô���Դ��Photoshop CS��
        /// </summary>
        AlternateSpotColors_PSCS = 1067,

        #endregion

        #region Photoshop CS2 
        /// <summary>
        /// ͼ��ѡ��ID��2�ֽڼ�����ÿ�μ����ظ��������ݣ�4�ֽڲ�ID��Photoshop CS2��
        /// </summary>
        LayerSelectionID_PSCS2 = 1069,
        /// <summary>
        /// HDR��ɫ��Ϣ��Photoshop CS2��
        /// </summary>
        HDRToningInfo_PSCS2 = 1070,
        /// <summary>
        /// ��ӡ��Ϣ��Photoshop CS2��
        /// </summary>
        PrintInfo_PSCS2 = 1071,
        /// <summary>
        /// �����ò���ID���ĵ���ÿ��1���ֽڣ�����Դ�����ظ���ע�⣺ͼ�����п�ʼ�ͽ�����ǣ�Photoshop CS2��
        /// </summary>
        LayerGroupEnabledID_PSCS2 = 1072,

        #endregion

        #region Photoshop CS3

        /// <summary>
        /// ��ɫ��������Դ��Photoshop CS3��
        /// </summary>
        ColorSamplersResource_PSCS3 = 1073,
        /// <summary>
        /// �����̶ȡ�4���ֽڣ��������汾=16����Photoshop CS3��
        /// </summary>
        MeasurementScale_PSCS3 = 1074,
        /// <summary>
        /// ʱ������Ϣ��4���ֽڣ��������汾=16����Photoshop CS3��
        /// </summary>
        TimelineInfo_PSCS3 = 1075,
        /// <summary>
        /// �����¶��4���ֽڣ��������汾=16����Photoshop CS3��
        /// </summary>
        SheetDisclosure_PSCS3 = 1076,
        /// <summary>
        /// DisplayInfo �ṹ��֧�ָ�����ɫ��Photoshop CS3��
        /// </summary>
        DisplayInfo_PSCS3 = 1077,
        /// <summary>
        /// Onion Ƥ����4���ֽڣ��������汾=16����Photoshop CS3��
        /// </summary>
        OnionSkins_PSCS3 = 1078,
        /// <summary>
        /// Lightroom��������������ڣ����ĵ�����Lightroom�������̵��м䡣
        /// </summary>
        LightroomWorkflows_PSCS3 = 8000,

        #endregion

        #region Photoshop CS4
        /// <summary>
        /// ������Ϣ��4���ֽڣ��������汾=16����Photoshop CS4��
        /// </summary>
        CountInfo_PSCS4 = 1080,

        #endregion

        #region Photoshop CS5

        /// <summary>
        /// ��ӡ��Ϣ��4���ֽڣ��������汾=16����Photoshop CS5��
        /// </summary>
        PrintInfo_PSCS5 = 1082,
        /// <summary>
        /// ��ӡ��ʽ��4���ֽڣ��������汾=16����Photoshop CS5��
        /// </summary>
        PrintStyle_PSCS5 = 1083,
        /// <summary>
        /// MacNSPrint��Ϣ��Photoshop CS5��
        /// </summary>
        MacNSPrintInfo_PSCS5 = 1084,
        /// <summary>
        /// Windows����ģʽ��Photoshop CS5��
        /// </summary>
        WindowsDEVMODE_PSCS5 = 1085,

        #endregion

        #region Photoshop CS6
        /// <summary>
        /// �Զ������ļ�·�������鲻Ҫʹ�ã�Photoshop CS6��
        /// </summary>
        AutoSaveFilePath_PSCS6 = 1086,
        /// <summary>
        /// �Զ������ʽ�����鲻Ҫʹ�ã�Photoshop CS6��
        /// </summary>
        AutoSaveFormat_PSCS6 = 1087,

        #endregion

        #region Photoshop CC
        /// <summary>
        /// ·��ѡ��״̬��4���ֽڣ��������汾=16����Photoshop CC��
        /// </summary>
        PathSelectionState_PSCC = 1088,
        /// <summary>
        /// ԭ��·����Ϣ��4���ֽڣ��������汾=16����Photoshop CC��
        /// </summary>
        OriginPathInfo_PSCC = 3000,

        #endregion

        /// <summary>
        /// ·����Ϣ start�������·����
        /// </summary>
        PathInfoStart = 2000,
        /// <summary>
        /// ·����Ϣ end�������·����
        /// </summary>
        PathInfoEnd = 2997,
        /// <summary>
        /// ����·����
        /// </summary>
        ClippingPathName = 2999,
        /// <summary>
        /// �����Դ start
        /// </summary>
        PluginResourceStart = 4000,
        /// <summary>
        /// �����Դ end
        /// </summary>
        PluginResourceEnd = 4999,
        /// <summary>
        /// Image Ready ���������������XML��ʾ
        /// </summary>
        ImageReadyVariables = 7000,
        /// <summary>
        /// Image Ready ���ݼ�
        /// </summary>
        ImageReadyDataSets = 7001,
        /// <summary>
        /// Image Ready Ĭ��ѡ��״̬
        /// </summary>
        ImageReadyDefaultSelectedState = 7002,
        /// <summary>
        /// Image Ready 7������չ״̬
        /// </summary>
        ImageReady7RolloverExpandedState = 7003,
        /// <summary>
        /// Image Ready ������չ״̬
        /// </summary>
        ImageReadyRolloverExpandedState = 7004,
        /// <summary>
        /// Image Ready ���������
        /// </summary>
        ImageReadySaveLayerSettings = 7005,
        /// <summary>
        /// Image Ready �汾
        /// </summary>
        ImageReadyVersion = 7006,

        /// <summary>
        /// ��ӡ��־��Ϣ��2�ֽڰ汾��=1����1�ֽ����Ĳü���ǣ�1�ֽڣ�=0����4�ֽڳ�Ѫ���ֵ��2�ֽڳ�Ѫ��ȿ̶�
        /// </summary>
        PrintFlagsInfo = 10000,
    }

}