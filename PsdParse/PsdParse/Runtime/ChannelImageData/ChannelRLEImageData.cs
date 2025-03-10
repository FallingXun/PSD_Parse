
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace PsdParse
{
    /// <summary>
    /// 单层单通道的 RLE 图像数据
    /// </summary>
    public class ChannelRLEImageData : IStreamParse
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
                    throw new Exception(string.Format("PSD 文件（单层所有通道的图像数据）异常，Compression:{0}", value));
                }
                m_Compression = value;
            }
        }

        /// <summary>
        /// 单通道图像每行的数据长度
        /// </summary>
        public List<ushort> LineDataLengthList
        {
            get; set;
        }

        /// <summary>
        /// 当前通道的图像数据
        /// </summary>
        public byte[] ChannelImageData
        {
            get; set;
        }

        /// <summary>
        /// 高度
        /// </summary>
        private int m_Height;


        public ChannelRLEImageData(int height)
        {
            m_Height = height;
        }

        public void Parse(Reader reader)
        {
            Compression = (ECompression)reader.ReadUInt16();
            var channelImageDataLength = 0;
            LineDataLengthList = new List<ushort>(m_Height);
            for (int i = 0; i < m_Height; i++)
            {
                var item = reader.ReadUInt16();
                LineDataLengthList.Add(item);
                channelImageDataLength += item;
            }
            ChannelImageData = reader.ReadBytes(channelImageDataLength);
        }
    }

}
