using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 图层和蒙版信息段
    /// </summary>
    public class LayerAndMaskInformationSection : IStreamParse
    {
        /// <summary>
        /// 图层和蒙版信息段长度（4 字节，PSB 是 8 字节）
        /// </summary>
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// 图层信息
        /// </summary>
        public LayerInfo LayerInfo
        {
            get; set;
        }

        /// <summary>
        /// 全局图层蒙版信息
        /// </summary>
        public GlobalLayerMaskInfo GlobalLayerMaskInfo
        {
            get; set;
        }

        /// <summary>
        /// 其他图层信息，包含各种类型数据的一系列标记块，Photoshop 4.0 以上
        /// </summary>
        public List<AdditionalLayerInfo> AdditionalLayerInfoList
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Length = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Length;
            if (Length > 0)
            {
                LayerInfo = new LayerInfo();
                LayerInfo.Parse(reader);
                GlobalLayerMaskInfo = new GlobalLayerMaskInfo();
                GlobalLayerMaskInfo.Parse(reader);
                AdditionalLayerInfoList = new List<AdditionalLayerInfo>();
                while (reader.BaseStream.Position < endPosition)
                {
                    var item = new AdditionalLayerInfo();
                    item.Parse(reader);
                    AdditionalLayerInfoList.Add(item);
                }
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图层和蒙版信息段）异常，数据超长:{0}，Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }
    }

    #region 图层和蒙版信息段（Layer and Mask Information Section）- 图层信息（Layer info）
    /// <summary>
    /// 图层和蒙版信息段-图层信息
    /// </summary>
    public class LayerInfo : IStreamParse
    {
        /// <summary>
        /// 图层信息长度（4 字节，PSB 为 8 字节），向上取 2 的倍数
        /// </summary>
        [ByteSize(4)]
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// 图层数（2 字节），如果是负数，则其绝对值是层数，第一个 alpha 通道包含合并结果的透明度数据
        /// </summary>
        [ByteSize(2)]
        public short LayerCount
        {
            get; set;
        }

        /// <summary>
        /// 图层记录列表
        /// </summary>
        [ByteSize()]
        public List<LayerRecords> LayerRecordsList
        {
            get; set;
        }

        /// <summary>
        /// 图像数据记录列表
        /// </summary>
        [ByteSize()]
        public List<ImageDataRecords> ImageDataRecordsList
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Length = Utils.RoundUp(reader.ReadUInt32(), 2u);
            if (Length > 0)
            {
                LayerCount = reader.ReadInt16();

                LayerRecordsList = new List<LayerRecords>(LayerCount);
                for (int i = 0; i < LayerCount; i++)
                {
                    var item = new LayerRecords();
                    item.Parse(reader);
                    LayerRecordsList.Add(item);
                }


                ImageDataRecordsList = new List<ImageDataRecords>(LayerCount);
                for (int i = 0; i < LayerCount; i++)
                {
                    var width = LayerRecordsList[i].LayerContentsRectangle.Width;
                    var height = LayerRecordsList[i].LayerContentsRectangle.Height;
                    var channelInfoList = LayerRecordsList[i].ChannelInfoList;
                    var item = new ImageDataRecords(LayerRecordsList[i]);
                    item.Parse(reader);
                    ImageDataRecordsList.Add(item);
                }
            }
        }
    }

    #region 图层和蒙版信息段（Layer and Mask Information Section）- 图层信息（Layer info）- 图层记录（Layer records）
    /// <summary>
    /// 图层和蒙版信息段-图层信息-图层记录
    /// </summary>
    public class LayerRecords : IStreamParse
    {
        /// <summary>
        /// 包含图层内容的矩形坐标（4 * 4字节），指定上、左、下、右坐标。
        ///     <see cref="ChannelInfo.ID"/> 为 -2 时，使用 <see cref="LayerMask.EnclosingLayerMaskRectangle"/>。
        ///     <see cref="ChannelInfo.ID"/> 为 -3 时，使用 <see cref="LayerMask.RealEnclosingLayerMaskRectangle"/>。
        /// </summary>
        public Rectangle LayerContentsRectangle
        {
            get; set;
        }

        /// <summary>
        /// 通道数（2 字节）
        /// </summary>
        public ushort ChannelCount
        {
            get; set;
        }

        public List<ChannelInfo> ChannelInfoList
        {
            get; set;
        }

        private string m_BlendModeSignature;
        /// <summary>
        /// 混合模式标识符（4 字节），必须为"8BIM"
        /// </summary>
        public string BlendModeSignature
        {
            get
            {
                return m_BlendModeSignature;
            }
            set
            {
                if (value != Const.Signature_8BIM)
                {
                    throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录）异常，BlendModeSignature:{0}", value));
                }
                m_BlendModeSignature = value;
            }
        }

        private string m_BlendModeKey;
        /// <summary>
        /// 混合模式 key（4 字节）<see cref="BlendModeKeyConst">
        /// </summary>
        public string BlendModeKey
        {
            get
            {
                return m_BlendModeKey;
            }
            set
            {
                if (BlendModeKeyConst.IsDefined(value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录）异常，BlendModeKey:{0}", value));
                }
                m_BlendModeKey = value;
            }
        }

        /// <summary>
        /// 不透明度（1 字节），0 = 透明，255 = 不透明
        /// </summary>
        public byte Opacity
        {
            get; set;
        }

        private EClipping m_Clipping;
        /// <summary>
        /// 裁剪（1 字节），0 = 透明，255 = 不透明
        /// </summary>
        public EClipping Clipping
        {
            get
            {
                return m_Clipping;
            }
            set
            {
                if (Enum.IsDefined(typeof(EClipping), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录）异常，Clipping:{0}", value));
                }
                m_Clipping = value;
            }
        }

        /// <summary>
        /// 标记信息（1 字节），<see cref="ELayerRecordFlag"/>
        /// </summary>
        public byte Flags
        {
            get; set;
        }

        /// <summary>
        /// 填充器（1 字节），用 0 填充
        /// </summary>
        public byte Filler
        {
            get; set;
        }

        /// <summary>
        /// 额外数据字段的长度（4 字节），等于接下来五个字段的总长度
        /// </summary>
        public uint ExtraDataFieldLength
        {
            get; set;
        }

        /// <summary>
        /// 层蒙版信息
        /// </summary>
        public LayerMask LayerMask
        {
            get; set;
        }

        public LayerBlendingRanges LayerBlendingRanges
        {
            get; set;
        }

        /// <summary>
        /// 图层名，Pascal 字符串，长度为 4 的倍数
        /// </summary>
        public string LayerName
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            LayerContentsRectangle = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
            ChannelCount = reader.ReadUInt16();
            ChannelInfoList = new List<ChannelInfo>(ChannelCount);
            for (int i = 0; i < ChannelCount; i++)
            {
                var item = new ChannelInfo();
                item.Parse(reader);
                ChannelInfoList.Add(item);
            }
            BlendModeSignature = reader.ReadASCIIString(4);
            BlendModeKey = reader.ReadASCIIString(4);
            Opacity = reader.ReadByte();
            Clipping = (EClipping)reader.ReadByte();
            Flags = reader.ReadByte();
            Filler = reader.ReadByte();
            ExtraDataFieldLength = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + ExtraDataFieldLength;
            if (ExtraDataFieldLength > 0)
            {
                LayerMask = new LayerMask();
                LayerMask.Parse(reader);
                LayerBlendingRanges = new LayerBlendingRanges();
                LayerBlendingRanges.Parse(reader);

                var factor = 4u;
                LayerName = reader.ReadPascalString(factor);
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录），数据超长:{0}，ExtraDataFieldLength:{1}", reader.BaseStream.Position - startPosition, ExtraDataFieldLength));
            }
        }
    }

    /// <summary>
    /// 图层和蒙版信息段-图层信息-图层记录-通道信息（6 字节）
    /// </summary>
    public class ChannelInfo : IStreamParse
    {
        private EChannelID m_ID;
        /// <summary>
        /// 通道 id（2 字节）
        /// </summary>
        [ByteSize(2)]
        public EChannelID ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                if (Enum.IsDefined((typeof(EChannelID)), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（ 图层和蒙版信息段-图层信息-图层记录-通道信息），ID:{0}", value));
                }
                m_ID = value;
            }
        }

        /// <summary>
        /// 图像数据记录 <see cref="ImageDataRecords"/> 的长度（4 字节），<see cref="ImageDataRecords.ImageData"/> 的长度需要减去 <see cref="ImageDataRecords.Compression"/> 的长度（即 2 字节）
        /// </summary>
        [ByteSize(4)]
        public uint Length
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            ID = (EChannelID)reader.ReadInt16();
            Length = reader.ReadUInt32();
        }
    }

    /// <summary>
    /// 图层和蒙版信息段-图层信息-图层记录-图层蒙版
    /// </summary>
    public class LayerMask : IStreamParse
    {
        /// <summary>
        /// 蒙版数据大小（4 字节），用于检查大小和标志，以确定是否存在，如果为零，则后续字段不存在
        /// </summary>
        public uint Size
        {
            get; set;
        }

        /// <summary>
        /// 封闭层矩形坐标（4 * 4 字节）
        /// </summary>
        public Rectangle EnclosingLayerMaskRectangle
        {
            get; set;
        }

        private EDefaultColor m_DefaultColor;
        /// <summary>
        /// 默认颜色（1 字节）
        /// </summary>
        public EDefaultColor DefaultColor
        {
            get
            {
                return m_DefaultColor;
            }
            set
            {
                if (Enum.IsDefined(typeof(EDefaultColor), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录-图层蒙版）异常，DefaultColor:{0}", value));
                }
                m_DefaultColor = value;
            }
        }

        /// <summary>
        /// 标记信息（1 字节），<see cref="ELayerMaskFlag"/>
        /// </summary>
        public byte Flags
        {
            get; set;
        }

        /// <summary>
        /// 蒙版参数（1 字节），当 Flags 的 bit 4 设置时才存在，<see cref="EMaskParamFlags"/>
        /// </summary>
        public byte MaskParams
        {
            get; set;
        }

        /// <summary>
        /// 用户蒙版密度（1 字节）
        /// </summary>
        public byte UserMaskDensity
        {
            get; set;
        }

        /// <summary>
        /// 用户蒙版羽化（8 字节）
        /// </summary>
        public double UserMaskFeather
        {
            get; set;
        }


        /// <summary>
        /// 矢量蒙版密度（1 字节）
        /// </summary>
        public byte VectorMaskDensity
        {
            get; set;
        }

        /// <summary>
        /// 矢量蒙版羽化（8 字节）
        /// </summary>
        public double VectorMaskFeather
        {
            get; set;
        }

        /// <summary>
        /// 偏移（2 字节），仅当 <see cref="Size"/> = 20 时存在，否则后续字段存在
        /// </summary>
        public ushort Padding
        {
            get; set;
        }

        public byte RealFlags
        {
            get; set;
        }

        private EDefaultColor m_RealUserMaskBackground;
        /// <summary>
        /// 真实用户蒙版背景（1 字节）
        /// </summary>
        public EDefaultColor RealUserMaskBackground
        {
            get
            {
                return m_RealUserMaskBackground;
            }
            set
            {
                if (Enum.IsDefined(typeof(EDefaultColor), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录-图层蒙版）异常，RealUserMaskBackground:{0}", value));
                }
                m_RealUserMaskBackground = value;
            }
        }


        /// <summary>
        /// 真实封闭层矩形坐标（4 * 4 字节）
        /// </summary>
        public Rectangle RealEnclosingLayerMaskRectangle
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Size = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Size;
            if (Size > 0)
            {
                EnclosingLayerMaskRectangle = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                DefaultColor = (EDefaultColor)reader.ReadByte();
                Flags = reader.ReadByte();
                if ((Flags & (byte)ELayerMaskFlag.UserMaskCameFromRenderingOtherData) > 0)
                {
                    MaskParams = reader.ReadByte();
                    if ((MaskParams & (byte)EMaskParamFlags.UserMaskDensity) > 0)
                    {
                        UserMaskDensity = reader.ReadByte();
                    }
                    if ((MaskParams & (byte)EMaskParamFlags.UserMaskFeather) > 0)
                    {
                        UserMaskFeather = reader.ReadDouble();
                    }
                    if ((MaskParams & (byte)EMaskParamFlags.VectorMaskDensity) > 0)
                    {
                        VectorMaskDensity = reader.ReadByte();
                    }
                    if ((MaskParams & (byte)EMaskParamFlags.VectorMaskFeather) > 0)
                    {
                        VectorMaskFeather = reader.ReadDouble();
                    }
                }
                if (Size == 20)
                {
                    Padding = reader.ReadUInt16();
                }
                else
                {
                    RealFlags = reader.ReadByte();
                    RealUserMaskBackground = (EDefaultColor)reader.ReadByte();
                    RealEnclosingLayerMaskRectangle = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                }
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录-图层蒙版）异常，数据超长:{0}，Size:{1}", reader.BaseStream.Position - startPosition, Size));
            }
        }

    }

    /// <summary>
    /// 图层和蒙版信息段-图层信息-图层记录-图层混合范围
    /// </summary>
    public class LayerBlendingRanges : IStreamParse
    {
        /// <summary>
        /// 图层混合范围数据长度（4 字节）
        /// </summary>
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// 复合灰色混合源（4 字节），包含2个白色值、2个黑色值
        /// </summary>
        public uint CompositeGrayBlendSource
        {
            get; set;
        }

        /// <summary>
        /// 复合灰色混合目标范围（4 字节）
        /// </summary>
        public uint CompositeGrayBlendDestinationRange
        {
            get; set;
        }

        /// <summary>
        /// 通道范围（4 字节）,第一源、第一目标、第二源、第二目标、...
        /// </summary>
        public List<uint> ChannelRange
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Length = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Length;
            if (Length > 0)
            {
                CompositeGrayBlendSource = reader.ReadUInt32();
                CompositeGrayBlendDestinationRange = reader.ReadUInt32();
                ChannelRange = new List<uint>();
                while (reader.BaseStream.Position < endPosition)
                {
                    ChannelRange.Add(reader.ReadUInt32());
                }
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录-图层混合范围）异常，数据超长:{0}，Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }
    }
    #endregion

    #region 图层和蒙版信息段（Layer and Mask Information Section）- 图层信息（Layer info）- 图像数据记录（Channel image data，代码称为 ImageDataRecords）

    /// <summary>
    /// 图层和蒙版信息段-图层信息-图像数据记录（单层所有通道的图像数据记录信息，官方文档为 Layer info -> Channel image data）
    /// </summary>
    public class ImageDataRecords : IStreamParse
    {

        /// <summary>
        /// 所有通道的图像数据列表
        ///     RLE压缩时，每个通道的字节数组，前面部分为每一行的数据长度，行数为 LayerBottom - LayerTop ，每个数据长度为 2 字节（PSB 为 4 字节），所有行的长度后才是图像数据
        /// </summary>
        [ByteSize()]
        public List<ChannelImageData> ChannelImageDataList
        {
            get; set;
        }

        /// <summary>
        /// 当前层信息
        /// </summary>
        private LayerRecords m_LayerRecords;

        /// <summary>
        /// 图像数据记录
        /// </summary>
        /// <param name="layerRecords">当前层信息</param>
        public ImageDataRecords(LayerRecords layerRecords)
        {
            m_LayerRecords = layerRecords;
        }


        public void Parse(Reader reader)
        {
            var channelCount = m_LayerRecords.ChannelCount;
            ChannelImageDataList = new List<ChannelImageData>(channelCount);
            for (int i = 0; i < channelCount; i++)
            {
                var item = new ChannelImageData(m_LayerRecords, m_LayerRecords.ChannelInfoList[i]);
                item.Parse(reader);
                ChannelImageDataList.Add(item);
            }
        }
    }

    /// <summary>
    /// 图层和蒙版信息段-图层信息-图像数据记录-通道图像数据
    /// </summary>
    public class ChannelImageData : IStreamParse
    {
        private ECompression m_Compression;
        /// <summary>
        /// 压缩格式（2 字节）
        /// </summary>
        [ByteSize(2)]
        public ECompression Compression
        {
            get
            {
                return m_Compression;
            }
            set
            {
                if (Enum.IsDefined(typeof(ECompression), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图像数据记录-通道图像数据）异常，Compression:{0}", value));
                }
                m_Compression = value;
            }
        }

        public IStreamParse Data
        {
            get; set;
        }


        /// <summary>
        /// 当前层信息
        /// </summary>
        private LayerRecords m_LayerRecords;
        /// <summary>
        /// 当前通道信息
        /// </summary>
        private ChannelInfo m_ChannelInfo;

        public ChannelImageData(LayerRecords layerRecords, ChannelInfo channelInfo)
        {
            m_LayerRecords = layerRecords;
            m_ChannelInfo = channelInfo;
        }

        public void Parse(Reader reader)
        {
            Compression = (ECompression)reader.ReadUInt16();

            var height = 0;
            switch (m_ChannelInfo.ID)
            {
                case EChannelID.UserMaskAndVectorMask:
                    {
                        height = m_LayerRecords.LayerMask.RealEnclosingLayerMaskRectangle.Height;
                    }
                    break;
                case EChannelID.UserMaskOrVectorMask:
                    {
                        height = m_LayerRecords.LayerMask.EnclosingLayerMaskRectangle.Height;
                    }
                    break;
                default:
                    {
                        height = m_LayerRecords.LayerContentsRectangle.Height;
                    }
                    break;
            }
            switch (Compression)
            {
                case ECompression.RawData:
                    {
                        var channelImageDataLength = (int)m_ChannelInfo.Length - 2;
                        Data = new ChannelRawImageData(channelImageDataLength);
                    }
                    break;
                case ECompression.RLECompression:
                    {
                        Data = new ChannelRLEImageData(height);
                    }
                    break;
                default:
                    {
                        var channelImageDataLength = (int)m_ChannelInfo.Length - 2;
                        Data = new ChannelDefaultImageData(Compression, channelImageDataLength);
                    }
                    break;
            }
            Data.Parse(reader);
        }
    }
    #endregion

    #endregion

    #region 图层和蒙版信息段（Layer and Mask Information Section）-> 全局图层蒙版信息（Global layer mask info）

    /// <summary>
    /// 图层和蒙版信息段-全局图层蒙版信息
    /// </summary>
    public class GlobalLayerMaskInfo : IStreamParse
    {
        /// <summary>
        /// 全局图层蒙版信息长度（4 字节）
        /// </summary>
        [ByteSize(4)]
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// 叠加颜色空间（2 字节）
        /// </summary>
        [ByteSize(2)]
        public ushort OverlayColorSpace
        {
            get; set;
        }

        /// <summary>
        /// 颜色组件数组（8 字节），4 * 2 byte
        /// </summary>
        [ByteSize(8)]
        public ulong ColorComponents
        {
            get; set;
        }

        private ushort m_Opacity;
        public ushort Opacity
        {
            get
            {
                return m_Opacity;
            }
            set
            {
                if (value < Const.GlobalLayerMaskInfo_Opacity_Transparent || value > Const.GlobalLayerMaskInfo_Opacity_Opaque)
                {
                    throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-全局图层蒙版信息）异常，Opacity:{0}", value));
                }
                m_Opacity = value;
            }
        }

        /// <summary>
        /// 类型（1 字节），0=所选颜色，即反转；1=颜色保护；128=每层存储的使用值
        /// </summary>
        public byte Kind
        {
            get; set;
        }

        /// <summary>
        /// 填充器，用 0 填充
        /// </summary>
        public byte[] Filler
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Length = reader.ReadUInt32();
            if (Length > 0)
            {
                var startPosition = reader.BaseStream.Position;
                var endPosition = startPosition + Length;
                OverlayColorSpace = reader.ReadUInt16();
                ColorComponents = reader.ReadUInt64();
                Opacity = reader.ReadUInt16();
                Kind = reader.ReadByte();
                Filler = reader.ReadBytes((int)(endPosition - reader.BaseStream.Position));
            }
        }
    }

    #endregion

    #region 图层和蒙版信息段（Layer and Mask Information Section）-> 其他层信息（Additional Layer Information）
    /// <summary>
    /// 图层和蒙版信息段-其他层信息
    /// </summary>
    public class AdditionalLayerInfo : IStreamParse
    {
        private string m_Signature;
        /// <summary>
        /// 标记信息（4 字节）
        /// </summary>
        public string Signature
        {
            get
            {
                return m_Signature;
            }
            set
            {
                switch (value)
                {
                    case Const.Signature_8BIM:
                    case Const.Signature_8B64:
                        break;
                    default:
                        {
                            throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-其他层信息）异常，Signature:{0}", value));
                        }
                }
                m_Signature = value;
            }
        }

        private string m_Key;
        /// <summary>
        /// 4 字符代码表示（4 字节）
        /// </summary>
        public string Key
        {
            get
            {
                return m_Key;
            }
            set
            {
                if (LayerKeyConst.IsDefined(value) == false)
                {
                    Console.Write(string.Format("PSD 文件（图层和蒙版信息段-其他层信息）异常，Key:{0}", value));
                }
                m_Key = value;
            }
        }

        /// <summary>
        /// 数据长度（4 字节），偶数值，PSB 中 LMsk, Lr16, Lr32, Layr, Mt16, Mt32, Mtrn, Alph, FMsk, lnk2, FEid, FXid, PxSD 为 8 字节
        /// </summary>
        public uint DataLength
        {
            get;set;
        }

        /// <summary>
        /// 数据信息，todo：未解析，不同 key 有不同格式，key 见 <see cref="LayerKeyConst"/>
        /// </summary>
        public byte[] Data
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Signature = reader.ReadASCIIString(4);
            Key = reader.ReadASCIIString(4);
            DataLength = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            // 文档中说明长度值 Length 为偶数字节数，实际上大部分 key 的长度值为 4 的倍数的偏移长度，部分 key（LMsk）为偶数偏移长度，其他 key（Txt2, Lr16, Lr32）为无偏移长度。而无论长度值为多少，数据块长度最终都会对齐到 4 的倍数。
            var endPosition = startPosition + Utils.RoundUp(DataLength, 4u);
            Data = reader.ReadBytes((int)DataLength);
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图层和蒙版信息段-图层信息-图层记录-图层混合范围）异常，数据超长:{0}，DataLength:{1}", reader.BaseStream.Position - startPosition, DataLength));
            }
        }
    }
    #endregion

}