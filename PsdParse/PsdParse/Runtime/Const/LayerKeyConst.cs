
namespace PsdParse
{
    public class LayerKeyConst
    {
        #region  Adjustment layer (Photoshop 4.0)

        public const string SolidColor = "SoCo";
        public const string Gradient = "GdFl";
        public const string Pattern = "PtFl";
        public const string BrightnessOrContrast = "brit";
        public const string Levels = "levl";
        public const string Curves = "curv";
        public const string Exposure = "expA";
        public const string Vibrance = "vibA";
        public const string OldHueOrSaturation = "hue ";
        public const string NewHueOrSaturation = "hue2";    // (Photoshop 5.0)
        public const string ColorBalance = "blnc";
        public const string BlackAndWhite = "blwh";
        public const string PhotoFilter = "phfl";
        public const string ChannelMixer = "mixr";
        public const string ColorLookup = "clrL";
        public const string Invert = "nvrt";
        public const string Posterize = "post";
        public const string Threshold = "thrs";
        public const string GradientMap = "grdm";
        public const string Selectivecolor = "selc";

        #endregion


        #region Photoshop 5.0
        public const string EffectsLayer = "lrFX";
        public const string TypeToolInfo = "tySh";      // (Photoshop 5.0 and 5.5 only)
        public const string UnicodeLayerName = "luni";

        #endregion


        #region Photoshop 6.0
        public const string LayerID = "lyid";
        public const string ObjectBasedEffectsLayerInfo = "lfx2";
        public const string Patterns = "Patt";      //  (Photoshop 6.0 and CS (8.0))
        public const string Patterns2 = "Pat2";     //  (Photoshop 6.0 and CS (8.0))
        public const string Patterns3 = "Pat3";     //  (Photoshop 6.0 and CS (8.0))
        public const string Annotations = "Anno";
        public const string BlendClippingElements = "clbl";
        public const string BlendInteriorElements = "infx";
        public const string KnockoutSetting = "knko";
        public const string ProtectedSetting = "lspf";
        public const string SheetcolorSetting = "lclr";
        public const string ReferencePoint  = "fxrp";
        public const string GradientSetting = "grdm";
        public const string SectionDividerSetting = "lsct";
        public const string ChannelBlendingRestrictionsSetting = "brst";
        public const string SolidColorSheetSetting = "SoCo";
        public const string PatternFillSetting = "PtFl";
        public const string GradientFillSetting = "GdFl";
        public const string VectorMaskSetting = "vmsk";
        public const string VectorMaskSetting_PSCS6 = "vsms";
        public const string TypeToolObjectSetting = "TySh";
        public const string ForeignEffectID = "ffxi";
        public const string LayerNameSourceSetting = "lnsr";
        public const string PatternData  = "shpa";
        public const string MetadataSetting = "shmd";


        #endregion

        #region Photoshop 7.0
        public const string LayerVersion = "lyvr";
        public const string TransparencyShapesLayer = "tsly";
        public const string LayerMaskAsGlobaMask = "lmgm";
        public const string VectorMaskAsGlobaMask = "vmgm";

        #endregion


        #region Photoshop CS3
        public const string BlackWhite = "blwh";
        public const string TextEngineData = "Txt2";
        //public const string Vibrance = "vibA";
        public const string FilterMask = "FMsk";
        public const string PlacedayerData = "SoLd";        // See also 'PlLd' key

        #endregion

        #region Photoshop CS5
        public const string ContentGeneratorExtraData = "CgEd";

        #endregion

        #region Photoshop CS6
        //public const string ColorLookup = "clrL";
        public const string UnicodePathName = "pths";
        public const string AnimationEffects = "anFX";
        public const string VectorStrokeData = "vstk";
        public const string VectorStrokeContentData = "vscg";
        public const string UsingAlignedRendering = "sn2P";

        #endregion

        #region Photoshop CC
        public const string VectorOriginationData = "vogk";
        public const string PixelSourceData = "PxSc";

        #endregion

        #region Photoshop CC 2015
        public const string PixelSourceData_PSCC2015 = "PxSD";
        public const string ArtboardData = "artb";
        public const string ArtboardData2 = "artd";
        public const string ArtboardData3 = "abdd";
        public const string SmartObjectLayerData = "SoLE";

        #endregion

        #region Photoshop 2020
        public const string CompositorUsed= "cinf";

        #endregion

        public const string BrightnessAndContrast = "brit";
        //public const string ChannelMixer = "mixr";
        public const string PlacedLayer = "plLd";   //(replaced by SoLd in Photoshop CS3)
        public const string LinkedLayer = "lnkD";
        public const string LinkedLayer2 = "lnk2";
        public const string LinkedLayer3 = "lnk3";
        //public const string PhotoFilter = "phfl";
        public const string SavingMergedTransparency = "Mtrn";
        public const string SavingMergedTransparency2 = "Mt16";
        public const string SavingMergedTransparency3 = "Mt32";
        public const string UserMask = "LMsk";
        //public const string Exposure = "expA";
        public const string FilterEffects = "FXid";
        public const string FilterEffects2 = "FEid";

        public static bool IsDefined(string key)
        {
            return false;
        }
    }
}
