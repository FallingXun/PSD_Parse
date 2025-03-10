
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace PsdParse
{
    /// <summary>
    /// 单层单通道的图像数据
    /// </summary>
    public class ChannelRawImageData : IStreamParse
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
        /// 当前通道的图像数据
        /// </summary>
        public byte[] ChannelImageData
        {
            get; set;
        }

        private int m_ChannelImageDataLength;


        public ChannelRawImageData(int channelImageDataLength)
        {
            m_ChannelImageDataLength = channelImageDataLength;
        }


        public void Parse(Reader reader)
        {
            Compression = (ECompression)reader.ReadUInt16();
            ChannelImageData = reader.ReadBytes((int)m_ChannelImageDataLength);
        }
    }
}
