
using System.Collections.Generic;
using System;

namespace PsdParse
{
    /// <summary>
    /// ͼ�����ݶ�
    /// </summary>
    public class ImageDataSection : IStreamHandler
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
                    throw new Exception(string.Format("PSD �ļ���ͼ�����ݶΣ��쳣��Compression:{0}", value));
                }
                m_Compression = value;
            }
        }

        /// <summary>
        /// ����ͨ��ͼ���ÿ�����ݳ��ȵ��б�
        /// </summary>
        [ByteSize()]
        public List<ushort[]> ChannelLineLengthList
        {
            get; set;
        }

        /// <summary>
        /// ����ͨ��ͼ���ʵ��ͼ�����ݵ��б�
        ///     RLEѹ��ʱ��ÿ��ͨ�����ֽ����飬ǰ�沿��Ϊÿһ�е����ݳ��ȣ�����Ϊ LayerBottom - LayerTop ��ÿ�����ݳ���Ϊ 2 �ֽڣ�PSB Ϊ 4 �ֽڣ��������еĳ��Ⱥ����ͼ������
        /// </summary>
        public List<byte[]> ChannelImageBytesList
        {
            get; set;
        }


        /// <summary>
        /// ͨ����
        /// </summary>
        private int m_ChannelCount;
        /// <summary>
        /// ���
        /// </summary>
        private int m_Width;
        /// <summary>
        /// �߶�
        /// </summary>
        private int m_Height;
        /// <summary>
        /// ͨ��������
        /// </summary>
        private EDepth m_Depth;
        /// <summary>
        /// ��ɫģʽ
        /// </summary>
        private EColorMode m_ColorMode;

        public ImageDataSection(int channelCount, int width, int height, EDepth depth, EColorMode colorMode)
        {
            m_ChannelCount = channelCount;
            m_Width = width;
            m_Height = height;
            m_Depth = depth;
            m_ColorMode = colorMode;
        }

        public void Parse(Reader reader)
        {
            Compression = (ECompression)reader.ReadUInt16();
            ChannelLineLengthList = new List<ushort[]>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                switch (Compression)
                {
                    case ECompression.RawData:
                        {
                            ChannelLineLengthList.Add(null);
                        }
                        break;
                    case ECompression.RLECompression:
                        {
                            var lineLength = new ushort[m_Height];
                            for (int j = 0; j < m_Height; j++)
                            {
                                lineLength[j] = reader.ReadUInt16();
                            }
                            ChannelLineLengthList.Add(lineLength);
                        }
                        break;
                    default:
                        {
                            ChannelLineLengthList.Add(null);
                        }
                        break;
                }
            }

            ChannelImageBytesList = new List<byte[]>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                switch (Compression)
                {
                    case ECompression.RawData:
                        {
                            var length = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            var data = reader.ReadBytes(length);
                            ChannelImageBytesList.Add(data);
                        }
                        break;
                    case ECompression.RLECompression:
                        {
                            var length = 0;
                            for (int j = 0; j < ChannelLineLengthList[i].Length; j++)
                            {
                                length += ChannelLineLengthList[i][j];
                            }
                            var data = reader.ReadBytes(length);
                            ChannelImageBytesList.Add(data);
                        }
                        break;
                    default:
                        {
                            var length = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            var data = reader.ReadBytes(length);
                            ChannelImageBytesList.Add(data);
                        }
                        break;
                }
            }
        }

        public void Combine(Writer writer)
        {
            writer.WriteUInt16((ushort)Compression);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                switch (Compression)
                {
                    case ECompression.RLECompression:
                        {
                            var lineLength = ChannelLineLengthList[i];
                            for (int j = 0; j < m_Height; j++)
                            {
                                writer.WriteUInt16(lineLength[j]);
                            }
                        }
                        break;
                }
            }

            ChannelImageBytesList = new List<byte[]>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                writer.WriteBytes(ChannelImageBytesList[i]);
            }
        }
    }

}