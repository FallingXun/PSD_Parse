using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 图像资源段
    /// </summary>
    public class ImageResourcesSection : IStreamHandler
    {
        /// <summary>
        /// 图像资源段长度（4 字节）
        /// </summary>
        public int Length
        {
            get; set;
        }

        /// <summary>
        /// 图像资源块
        /// </summary>
        public List<ImageResourceBlock> ImageResourceBlockList
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Length = reader.ReadInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Length;
            if (Length > 0)
            {
                ImageResourceBlockList = new List<ImageResourceBlock>();
                while (reader.BaseStream.Position < endPosition)
                {
                    var item = new ImageResourceBlock();
                    item.Parse(reader);
                    ImageResourceBlockList.Add(item);

                    // 检查是否还有下一个 ImageResourceBlock
                    var signature = reader.ReadASCIIString(4);
                    reader.BaseStream.Position -= 4;
                    if (signature != Const.Signature_8BIM)
                    {
                        break;
                    }
                }
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.ReadPadding((uint)(endPosition - reader.BaseStream.Position));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图像资源段）异常，数据超长:{0}，Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }

        public void Combine(Writer writer)
        {
            writer.WriteInt32(Length);
            var startPosition = writer.BaseStream.Position;
            var endPosition = startPosition + Length;
            if (Length > 0)
            {
                for (int i = 0; i < ImageResourceBlockList.Count; i++)
                {
                    var item = ImageResourceBlockList[i];
                    item.Combine(writer);
                }
            }
            if (writer.BaseStream.Position <= endPosition)
            {
                writer.WritePadding((uint)(endPosition - writer.BaseStream.Position));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图像资源段）异常，数据超长:{0}，Length:{1}", writer.BaseStream.Position - startPosition, Length));
            }
        }

        public int CalculateLength(Calculator calculator)
        {
            var length = 0;

            length = calculator.CalculateInt32(Length);
            var startLength = length;
            var endLength = startLength + Length;
            if (Length > 0)
            {
                for (int i = 0; i < ImageResourceBlockList.Count; i++)
                {
                    var item = ImageResourceBlockList[i];
                    length += item.CalculateLength(calculator);
                }
            }
            if (length <= endLength)
            {
                length += calculator.CalculatePadding((uint)(endLength - length));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图像资源段）异常，数据超长:{0}，Length:{1}", length - startLength, Length));
            }

            return length;
        }
    }

    #region 图像资源段（Image Resources Section）- 图像资源块（Image Resource Blocks）
    /// <summary>
    /// 图像资源段-图像资源块
    /// </summary>
    public class ImageResourceBlock : IStreamHandler
    {
        private string m_Signature;
        /// <summary>
        /// 标识符（4 字节），必须为"8BIM"
        /// </summary>
        public string Signature
        {
            get
            {
                return m_Signature;
            }
            set
            {
                if (value != Const.Signature_8BIM)
                {
                    throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块）异常，Signature:{0}", value));
                }
                m_Signature = value;
            }
        }

        private EImageResourceID m_ImageResourceID;
        /// <summary>
        /// 图像资源ID（2 字节）
        /// </summary>
        public EImageResourceID ImageResourceID
        {
            get
            {
                return m_ImageResourceID;
            }
            set
            {
                if (Enum.IsDefined(typeof(EImageResourceID), value) == false)
                {
                    if ((value > EImageResourceID.PathInfoStart && value < EImageResourceID.PathInfoEnd) == false && (value > EImageResourceID.PluginResourceStart && value < EImageResourceID.PluginResourceEnd) == false)
                    {
                        Console.Write(string.Format("PSD 文件（图像资源段-图像资源块）异常，ImageResource:{0}", value));
                    }
                }
                m_ImageResourceID = value;
            }
        }

        /// <summary>
        /// 名字，Pascal 字符串，长度为 2 的倍数（空名称由两个字节的0组成）
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 资源数据大小（4 字节），需要内存对齐
        /// </summary>
        public uint ResourceDataSize
        {
            get; set;
        }


        /// <summary>
        /// 资源数据
        /// </summary>
        public ResourceData ResourceData
        {
            get; set;
        }


        public void Parse(Reader reader)
        {
            Signature = reader.ReadASCIIString(4);
            ImageResourceID = (EImageResourceID)reader.ReadInt16();

            // 内存对齐字节大小
            var factor = 2u;

            // Pascal 字符串要以设定值的倍数存储，这里是 2 字节，读取完后还需要跳过偏移字节
            Name = reader.ReadPascalString(factor);

            ResourceDataSize = reader.ReadUInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Utils.RoundUp(ResourceDataSize, factor);
            ResourceData = new ResourceData(ImageResourceID, ResourceDataSize);
            ResourceData.Parse(reader);
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.ReadPadding((uint)(endPosition - reader.BaseStream.Position));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块）异常，数据超长:{0}，ResourceDataSize:{1}", reader.BaseStream.Position - startPosition, ResourceDataSize));
            }
        }

        public void Combine(Writer writer)
        {
            writer.WriteASCIIString(Signature, 4);
            writer.WriteInt16((short)ImageResourceID);

            // 内存对齐字节大小
            var factor = 2u;

            // Pascal 字符串要以设定值的倍数存储，这里是 2 字节，读取完后还需要跳过偏移字节
            writer.WritePascalString(Name, factor);

            writer.WriteUInt32(ResourceDataSize);
            var startPosition = writer.BaseStream.Position;
            var endPosition = startPosition + Utils.RoundUp(ResourceDataSize, factor);
            ResourceData.Combine(writer);
            if (writer.BaseStream.Position <= endPosition)
            {
                writer.WritePadding((uint)(endPosition - writer.BaseStream.Position));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块）异常，数据超长:{0}，ResourceDataSize:{1}", writer.BaseStream.Position - startPosition, ResourceDataSize));
            }
        }

        public int CalculateLength(Calculator calculator)
        {
            var length = 0;
            length += calculator.CalculateASCIIString(Signature, 4);
            length += calculator.CalculateInt16((short)ImageResourceID);

            // 内存对齐字节大小
            var factor = 2u;

            // Pascal 字符串要以设定值的倍数存储，这里是 2 字节，读取完后还需要跳过偏移字节
            length += calculator.CalculatePascalString(Name, factor);

            length += calculator.CalculateUInt32(ResourceDataSize);
            var startLength = length;
            var endLength = startLength + Utils.RoundUp(ResourceDataSize, factor);
            length += ResourceData.CalculateLength(calculator);
            if (length <= endLength)
            {
                length += calculator.CalculatePadding((uint)(endLength - length));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块）异常，数据超长:{0}，ResourceDataSize:{1}", length - startLength, ResourceDataSize));
            }

            return length;
        }
    }


    /// <summary>
    /// 图像资源段-图像资源块-资源数据
    /// </summary>
    public class ResourceData : IStreamHandler
    {
        /// <summary>
        /// 格式数据
        /// </summary>
        public IStreamHandler ResourceFormat
        {
            get; set;
        }

        /// <summary>
        /// 图像资源ID
        /// </summary>
        private EImageResourceID m_ImageResourceID;
        /// <summary>
        /// 资源数据大小
        /// </summary>
        private uint m_DataSize;


        public ResourceData(EImageResourceID imageResourceID, uint dataSize)
        {
            m_ImageResourceID = imageResourceID;
            m_DataSize = dataSize;

        }

        public void Parse(Reader reader)
        {
            switch (m_ImageResourceID)
            {
                case EImageResourceID.ResolutionInfo:
                    {
                        ResourceFormat = new ResolutionInfoResourceFormat();
                    }
                    break;
                case EImageResourceID.GridAndGuidesInfo_PS4:
                    {
                        ResourceFormat = new GridAndGuidesResourceFormat();
                    }
                    break;
                case EImageResourceID.ThumbnailResource_PS4:
                case EImageResourceID.ThumbnailResource_PS5:
                    {
                        ResourceFormat = new ThumbnailResourceFormat(m_ImageResourceID);
                    }
                    break;
                case EImageResourceID.VersionInfo_PS6:
                    {
                        ResourceFormat = new VersionInfoResourceFormat();
                    }
                    break;
                default:
                    {
                        // todo：参考其他 ResourceFormat 实现
                        ResourceFormat = new DefaultResourceFormat(m_ImageResourceID, m_DataSize);
                    }
                    break;
            }
            ResourceFormat.Parse(reader);
        }

        public void Combine(Writer writer)
        {
            ResourceFormat.Combine(writer);
        }

        public int CalculateLength(Calculator calculator)
        {
            var length = 0;

            length += ResourceFormat.CalculateLength(calculator);

            return length;
        }
    }
    #endregion
}