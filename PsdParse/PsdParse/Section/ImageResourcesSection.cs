using System;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 图像资源段
    /// </summary>
    public class ImageResourcesSection : IStreamParse
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
        public ImageResourceBlock ImageResourceBlock
        {
            get; set;
        }


        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Length = reader.ReadInt32();
            if (Length > 0)
            {
                ImageResourceBlock = new ImageResourceBlock();
                ImageResourceBlock.Parse(reader, encoding);
            }
        }
    }


    /// <summary>
    /// 图像资源段-图像资源块
    /// </summary>
    public class ImageResourceBlock : IStreamParse
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
                if (value != "8BIM")
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
                    throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块）异常，ImageResource:{0}", value));
                }
                m_ImageResourceID = value;
            }
        }

        /// <summary>
        /// 名字，Pascal 字符串（空名称由两个字节的0组成）
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


        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Signature = Encoding.ASCII.GetString(reader.ReadBytes(4));
            ImageResourceID = (EImageResourceID)reader.ReadInt16();

            // 内存对齐字节大小
            var multiple = 2;

            // Pascal 字符串要以设定值的倍数存储，这里是 2 字节，读取完后还需要跳过偏移字节
            var startPosition = reader.BaseStream.Position;
            var count = (int)reader.ReadByte();
            var bytes = reader.ReadBytes(count);
            var offset = (int)(reader.BaseStream.Position - startPosition);
            var mod = offset % multiple;
            if (mod > 0)
            {
                var padding = multiple - mod;
                reader.BaseStream.Position += padding;
            }
            Name = encoding.GetString(bytes);

            ResourceDataSize = reader.ReadUInt32();

            var endPosition = reader.BaseStream.Position + ResourceDataSize;
            mod = (int)(ResourceDataSize % (uint)multiple);
            if (mod > 0)
            {
                var padding = multiple - mod;
                endPosition += padding;
            }
            ResourceData = new ResourceData(ImageResourceID);
            if (ResourceData != null)
            {
                ResourceData.ResourceFormat.Parse(reader, encoding);
            }
        }
    }


    /// <summary>
    /// 图像资源段-图像资源块-资源数据
    /// </summary>
    public class ResourceData
    {
        private EImageResourceID m_ImageResourceID;
        /// <summary>
        /// 图像资源ID
        /// </summary>
        public EImageResourceID ImageResourceID
        {
            get
            {
                return m_ImageResourceID;
            }
            set
            {
                if (Enum.IsDefined(typeof(EImageResourceID), value)== false)
                {
                    throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块-资源数据）异常，ImageResourceID:{0}", value));
                }
                m_ImageResourceID = value;
            }
        }

        /// <summary>
        /// 格式数据
        /// </summary>
        public IStreamParse ResourceFormat
        {
            get; set;
        }

        public ResourceData(EImageResourceID imageResourceID)
        {
            ImageResourceID = imageResourceID;
            switch (imageResourceID)
            {
                case EImageResourceID.GridAndGuidesInfo_PS4:
                    {
                        ResourceFormat = new GridAndGuidesResourceFormat();
                    }
                    break;
                case EImageResourceID.ThumbnailResource_PS4:
                case EImageResourceID.ThumbnailResource_PS5:
                    {
                        ResourceFormat = new ThumbnailResourceFormat(imageResourceID);
                    }
                    break;
                default:
                    {
                        // todo：参考其他 ResourceFormat 实现
                    }
                    break;
            }
        }
    }
}