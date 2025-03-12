using System;

namespace PsdParse
{
    /// <summary>
    /// 图像资源段-图像资源块-资源数据-网格和引导信息格式 <see cref="EImageResourceID.GridAndGuidesInfo_PS4"/>
    /// </summary>
    public class GridAndGuidesResourceFormat : IStreamHandler
    {
        #region 头文件

        private int m_Version;
        /// <summary>
        /// 版本号（4 字节），必须为 1
        /// </summary>
        public int Version
        {
            get
            {
                return m_Version;
            }
            set
            {
                if (value != 1)
                {
                    throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块-资源数据-网格和引导信息格式）异常，Version:{0}", value));
                }
                m_Version = value;
            }
        }

        /// <summary>
        /// 文档特定网格-水平方向（4 字节）
        /// </summary>
        public uint DocumentSpecificGridsHorizontal
        {
            get; set;
        }

        /// <summary>
        /// 文档特定网格-垂直方向（4 字节）
        /// </summary>
        public uint DocumentSpecificGridsVertical
        {
            get; set;
        }

        /// <summary>
        /// 引导资源块的数量（4 字节），可为0
        /// </summary>
        public uint GuideCount
        {
            get; set;
        }

        #endregion

        #region 数据块

        /// <summary>
        /// 指南在文档坐标中的位置（4 字节），由于导向是垂直的或水平的，因此这只需要是坐标的一个组成部分
        /// </summary>
        public int GuideLocation
        {
            get; set;
        }

        private EDirection m_GuideDirection;
        public EDirection GuideDirection
        {
            get
            {
                return m_GuideDirection;
            }
            set
            {
                if (Enum.IsDefined(typeof(EDirection), value) == false)
                {
                    throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块-资源数据-网格和引导信息格式）异常，GuideDirection:{0}", value));
                }
                m_GuideDirection = value;
            }
        }
        #endregion

        public void Parse(Reader reader)
        {
            #region 头文件
            Version = reader.ReadInt32();
            DocumentSpecificGridsHorizontal = reader.ReadUInt32();
            DocumentSpecificGridsVertical = reader.ReadUInt32();
            GuideCount = reader.ReadUInt32();
            #endregion


            #region 数据块
            if (GuideCount > 0)
            {
                GuideLocation = reader.ReadInt32();
                GuideDirection = (EDirection)reader.ReadByte();
            }
            #endregion
        }

        public void Combine(Writer writer)
        {
            #region 头文件
            writer.WriteInt32(Version);
            writer.WriteUInt32(DocumentSpecificGridsHorizontal);
            writer.WriteUInt32(DocumentSpecificGridsVertical);
            writer.WriteUInt32(GuideCount);
            #endregion


            #region 数据块
            if (GuideCount > 0)
            {
                writer.WriteInt32(GuideLocation);
                writer.WriteByte((byte)GuideDirection);
            }
            #endregion
        }
    }
}
