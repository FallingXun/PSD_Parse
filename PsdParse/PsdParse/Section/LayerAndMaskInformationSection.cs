using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// ͼ����ɰ���Ϣ��
    /// </summary>
    public class LayerAndMaskInformationSection : IStreamParse
    {
        /// <summary>
        /// ͼ����ɰ���Ϣ�γ��ȣ�4 �ֽڣ�PSB �� 8 �ֽڣ�
        /// </summary>
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// ͼ����Ϣ
        /// </summary>
        public LayerInfo LayerInfo
        {
            get; set;
        }

        /// <summary>
        /// ȫ��ͼ���ɰ���Ϣ
        /// </summary>
        public GlobalLayerMaskInfo GlobalLayerMaskInfo
        {
            get; set;
        }

        /// <summary>
        /// ����ͼ����Ϣ�����������������ݵ�һϵ�б�ǿ飬Photoshop 4.0 ����
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
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ�Σ��쳣�����ݳ���:{0}��Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }
    }

    #region ͼ����Ϣ LayerInfo
    /// <summary>
    /// ͼ����ɰ���Ϣ��-ͼ����Ϣ
    /// </summary>
    public class LayerInfo : IStreamParse
    {
        /// <summary>
        /// ͼ����Ϣ���ȣ�4 �ֽڣ�PSB Ϊ 8 �ֽڣ�������ȡ 2 �ı���
        /// </summary>
        [ByteSize(4)]
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// ͼ������2 �ֽڣ�������Ǹ������������ֵ�ǲ�������һ�� alpha ͨ�������ϲ������͸��������
        /// </summary>
        [ByteSize(2)]
        public short LayerCount
        {
            get; set;
        }

        /// <summary>
        /// ͼ���¼�б�
        /// </summary>
        [ByteSize()]
        public List<LayerRecords> LayerRecordsList
        {
            get; set;
        }

        /// <summary>
        /// ͼ�����ݼ�¼�б�
        /// </summary>
        [ByteSize()]
        public List<ImageDataRecords> ImageDataRecordsList
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Length = Utils.RoundUp(reader.ReadUInt32(), 2u);
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
                var item = new ImageDataRecords(width, height, channelInfoList);
                item.Parse(reader);
                ImageDataRecordsList.Add(item);
            }
        }
    }

    #region ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼ LayerRecords
    /// <summary>
    /// ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼
    /// </summary>
    public class LayerRecords : IStreamParse
    {
        /// <summary>
        /// ����ͼ�����ݵľ������꣨4 * 4�ֽڣ���ָ���ϡ����¡�������
        /// </summary>
        public Rectangle LayerContentsRectangle
        {
            get; set;
        }

        /// <summary>
        /// ͨ������2 �ֽڣ�
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
        /// ���ģʽ��ʶ����4 �ֽڣ�������Ϊ"8BIM"
        /// </summary>
        public string BlendModeSignature
        {
            get
            {
                return m_BlendModeSignature;
            }
            set
            {
                if (value != "8BIM")
                {
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼���쳣��BlendModeSignature:{0}", value));
                }
                m_BlendModeSignature = value;
            }
        }

        private string m_BlendModeKey;
        /// <summary>
        /// ���ģʽ key��4 �ֽڣ�<see cref="BlendModeKeyConst">
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
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼���쳣��BlendModeKey:{0}", value));
                }
                m_BlendModeKey = value;
            }
        }

        /// <summary>
        /// ��͸���ȣ�1 �ֽڣ���0 = ͸����255 = ��͸��
        /// </summary>
        public byte Opacity
        {
            get; set;
        }

        private EClipping m_Clipping;
        /// <summary>
        /// �ü���1 �ֽڣ���0 = ͸����255 = ��͸��
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
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼���쳣��Clipping:{0}", value));
                }
                m_Clipping = value;
            }
        }

        /// <summary>
        /// �����Ϣ��1 �ֽڣ���<see cref="ELayerRecordFlag"/>
        /// </summary>
        public byte Flags
        {
            get; set;
        }

        /// <summary>
        /// �������1 �ֽڣ����� 0 ���
        /// </summary>
        public byte Filler
        {
            get; set;
        }

        /// <summary>
        /// ���������ֶεĳ��ȣ�4 �ֽڣ������ڽ���������ֶε��ܳ���
        /// </summary>
        public uint ExtraDataFieldLength
        {
            get; set;
        }

        /// <summary>
        /// ���ɰ���Ϣ
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
        /// ͼ������Pascal �ַ���������Ϊ 4 �ı���
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
            LayerMask = new LayerMask();
            LayerMask.Parse(reader);
            LayerBlendingRanges = new LayerBlendingRanges();
            LayerBlendingRanges.Parse(reader);

            var factor = 4u;
            LayerName = reader.ReadPascalString(factor);
        }
    }

    /// <summary>
    /// ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼-ͨ����Ϣ��6 �ֽڣ�
    /// </summary>
    public class ChannelInfo : IStreamParse
    {
        /// <summary>
        /// ͨ�� id��2 �ֽڣ���
        ///     -1 �� ͸�� mask
        ///     -2 �� �û��ṩ�Ĳ� mask �� ʸ�� mask
        ///     -3 �� �û��ṩ��ʵ�ʲ� mask�����û� mask ��ʸ�� mask ͬʱ���ڣ�
        /// </summary>
        [ByteSize(2)]
        public short ID
        {
            get; set;
        }

        /// <summary>
        /// ͼ�����ݼ�¼ <see cref="ImageDataRecords"/> �ĳ��ȣ�4 �ֽڣ���<see cref="ImageDataRecords.ImageData"/> �ĳ�����Ҫ��ȥ <see cref="ImageDataRecords.Compression"/> �ĳ��ȣ��� 2 �ֽڣ�
        /// </summary>
        [ByteSize(4)]
        public uint Length
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            ID = reader.ReadInt16();
            Length = reader.ReadUInt32();
        }
    }

    /// <summary>
    /// ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼-ͼ���ɰ�
    /// </summary>
    public class LayerMask : IStreamParse
    {
        /// <summary>
        /// �ɰ����ݴ�С��4 �ֽڣ������ڼ���С�ͱ�־����ȷ���Ƿ���ڣ����Ϊ�㣬������ֶβ�����
        /// </summary>
        public uint Size
        {
            get; set;
        }

        /// <summary>
        /// ��ղ�������꣨4 * 4 �ֽڣ�
        /// </summary>
        public Rectangle EnclosingLayerMaskRectangle
        {
            get; set;
        }

        private EDefaultColor m_DefaultColor;
        /// <summary>
        /// Ĭ����ɫ��1 �ֽڣ�
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
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼-ͼ���ɰ棩�쳣��DefaultColor:{0}", value));
                }
                m_DefaultColor = value;
            }
        }

        /// <summary>
        /// �����Ϣ��1 �ֽڣ���<see cref="ELayerMaskFlag"/>
        /// </summary>
        public byte Flags
        {
            get; set;
        }

        /// <summary>
        /// �ɰ������1 �ֽڣ����� Flags �� bit 4 ����ʱ�Ŵ��ڣ�<see cref="EMaskParamFlags"/>
        /// </summary>
        public byte MaskParams
        {
            get; set;
        }

        /// <summary>
        /// �û��ɰ��ܶȣ�1 �ֽڣ�
        /// </summary>
        public byte UserMaskDensity
        {
            get; set;
        }

        /// <summary>
        /// �û��ɰ��𻯣�8 �ֽڣ�
        /// </summary>
        public double UserMaskFeather
        {
            get; set;
        }


        /// <summary>
        /// ʸ���ɰ��ܶȣ�1 �ֽڣ�
        /// </summary>
        public byte VectorMaskDensity
        {
            get; set;
        }

        /// <summary>
        /// ʸ���ɰ��𻯣�8 �ֽڣ�
        /// </summary>
        public double VectorMaskFeather
        {
            get; set;
        }

        /// <summary>
        /// ƫ�ƣ�2 �ֽڣ������� <see cref="Size"/> = 20 ʱ���ڣ���������ֶδ���
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
        /// ��ʵ�û��ɰ汳����1 �ֽڣ�
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
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼-ͼ���ɰ棩�쳣��RealUserMaskBackground:{0}", value));
                }
                m_RealUserMaskBackground = value;
            }
        }


        /// <summary>
        /// ��ʵ��ղ�������꣨4 * 4 �ֽڣ�
        /// </summary>
        public Rectangle RealEnclosingLayerMaskRectangle
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Size = reader.ReadUInt32();
            if (Size <= 0)
            {
                return;
            }
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

    }

    /// <summary>
    /// ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼-ͼ���Ϸ�Χ
    /// </summary>
    public class LayerBlendingRanges : IStreamParse
    {
        /// <summary>
        /// ͼ���Ϸ�Χ���ݳ��ȣ�4 �ֽڣ�
        /// </summary>
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// ���ϻ�ɫ���Դ��4 �ֽڣ�������2����ɫֵ��2����ɫֵ
        /// </summary>
        public uint CompositeGrayBlendSource
        {
            get; set;
        }

        /// <summary>
        /// ���ϻ�ɫ���Ŀ�귶Χ��4 �ֽڣ�
        /// </summary>
        public uint CompositeGrayBlendDestinationRange
        {
            get; set;
        }

        /// <summary>
        /// ͨ����Χ��4 �ֽڣ�,��һԴ����һĿ�ꡢ�ڶ�Դ���ڶ�Ŀ�ꡢ...
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
                throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ���¼-ͼ���Ϸ�Χ���쳣�����ݳ���:{0}��Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }
    }
    #endregion

    /// <summary>
    /// ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ�����ݼ�¼���ٷ��ĵ�Ϊ Layer info -> Channel image data
    /// </summary>
    public class ImageDataRecords : IStreamParse
    {
        private ECompression m_Compression;
        /// <summary>
        /// ѹ����ʽ��2 �ֽڣ�
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
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ͼ����Ϣ-ͼ�����ݼ�¼���쳣��Compression:{0}", value));
                }
                m_Compression = value;
            }
        }

        /// <summary>
        /// ����ͨ����ͼ������
        ///     RLEѹ��ʱ��ÿ��ͨ�����ֽ����飬ǰ�沿��Ϊÿһ�е����ݳ��ȣ�����Ϊ LayerBottom - LayerTop ��ÿ�����ݳ���Ϊ 2 �ֽڣ�PSB Ϊ 4 �ֽڣ��������еĳ��Ⱥ����ͼ������
        /// </summary>
        [ByteSize()]
        public IStreamParse ImageData
        {
            get; set;
        }

        /// <summary>
        /// ���
        /// </summary>
        private int m_Width;
        /// <summary>
        /// �߶�
        /// </summary>
        private int m_Height;
        /// <summary>
        /// ��ǰ�������ͨ����Ϣ
        /// </summary>
        private List<ChannelInfo> m_ChannelInfoList;

        /// <summary>
        /// ͼ�����ݼ�¼
        /// </summary>
        /// <param name="channelInfoList">����ͨ����Ϣ</param>
        public ImageDataRecords(int width, int height, List<ChannelInfo> channelInfoList)
        {
            m_Width = width;
            m_Height = height;
            m_ChannelInfoList = channelInfoList;
        }


        public void Parse(Reader reader)
        {
            Compression = (ECompression)reader.ReadUInt16();
            var channelCount = m_ChannelInfoList.Count;
            switch (Compression)
            {
                case ECompression.RawData:
                    {
                        ImageData = new RawImageData(channelCount, m_Width, m_Height, m_ChannelInfoList);
                    }
                    break;
                case ECompression.RLECompression:
                    {
                        ImageData = new RLEImageData(channelCount, m_Height);
                    }
                    break;
                default:
                    {
                        ImageData = new RawImageData(channelCount, m_Width, m_Height, m_ChannelInfoList);
                    }
                    break;
            }
            ImageData.Parse(reader);
        }
    }

    #endregion

    /// <summary>
    /// ͼ����ɰ���Ϣ��-ȫ��ͼ���ɰ���Ϣ
    /// </summary>
    public class GlobalLayerMaskInfo : IStreamParse
    {
        /// <summary>
        /// ȫ��ͼ���ɰ���Ϣ���ȣ�4 �ֽڣ�
        /// </summary>
        [ByteSize(4)]
        public uint Length
        {
            get; set;
        }

        /// <summary>
        /// ������ɫ�ռ䣨2 �ֽڣ�
        /// </summary>
        [ByteSize(2)]
        public ushort OverlayColorSpace
        {
            get; set;
        }

        /// <summary>
        /// ��ɫ������飨8 �ֽڣ���4 * 2 byte
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
                switch (value)
                {
                    case Const.GlobalLayerMaskInfo_Opacity_Transparent:
                    case Const.GlobalLayerMaskInfo_Opacity_Opaque:
                        break;
                    default:
                        {
                            throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-ȫ��ͼ���ɰ���Ϣ���쳣��Opacity:{0}", value));
                        }
                }
                m_Opacity = value;
            }
        }

        /// <summary>
        /// ���ͣ�1 �ֽڣ���0=��ѡ��ɫ������ת��1=��ɫ������128=ÿ��洢��ʹ��ֵ
        /// </summary>
        public byte Kind
        {
            get; set;
        }

        /// <summary>
        /// ��������� 0 ���
        /// </summary>
        public byte[] Filler
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Length = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Length;
            OverlayColorSpace = reader.ReadUInt16();
            ColorComponents = reader.ReadUInt64();
            Opacity = reader.ReadUInt16();
            Kind = reader.ReadByte();
            Filler = reader.ReadBytes((int)(endPosition - reader.BaseStream.Position));
        }
    }

    /// <summary>
    /// ͼ����ɰ���Ϣ��-��������Ϣ
    /// </summary>
    public class AdditionalLayerInfo : IStreamParse
    {
        private string m_Signature;
        /// <summary>
        /// �����Ϣ��4 �ֽڣ�
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
                    case "8BIM":
                    case "8B64":
                        break;
                    default:
                        {
                            throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-��������Ϣ���쳣��Signature:{0}", value));
                        }
                }
                m_Signature = value;
            }
        }

        private string m_Key;
        /// <summary>
        /// 4 �ַ������ʾ��4 �ֽڣ�
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
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-��������Ϣ���쳣��Key:{0}", value));
                }
                m_Key = value;
            }
        }

        private uint m_DataLength;
        /// <summary>
        /// ���ݳ��ȣ�4 �ֽڣ���ż��ֵ��PSB �� LMsk, Lr16, Lr32, Layr, Mt16, Mt32, Mtrn, Alph, FMsk, lnk2, FEid, FXid, PxSD Ϊ 8 �ֽ�
        /// </summary>
        public uint DataLength
        {
            get
            {
                return m_DataLength;
            }
            set
            {
                if (value % 2 > 0)
                {
                    throw new Exception(string.Format("PSD �ļ���ͼ����ɰ���Ϣ��-��������Ϣ���쳣��DataLength:{0}", value));
                }
                m_DataLength = value;
            }
        }

        /// <summary>
        /// ������Ϣ��todo��δ��������ͬ key �в�ͬ��ʽ��key �� <see cref="LayerKeyConst"/>
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
            if (DataLength > 0)
            {
                Data = reader.ReadBytes((int)DataLength);
            }
        }
    }

}