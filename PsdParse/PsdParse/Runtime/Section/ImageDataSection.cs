
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// ͼ�����ݶ�
    /// </summary>
    public class ImageDataSection : IStreamParse
    {
        /// <summary>
        /// ����ͨ����ͼ�������б�
        ///     RLEѹ��ʱ��ÿ��ͨ�����ֽ����飬ǰ�沿��Ϊÿһ�е����ݳ��ȣ�����Ϊ LayerBottom - LayerTop ��ÿ�����ݳ���Ϊ 2 �ֽڣ�PSB Ϊ 4 �ֽڣ��������еĳ��Ⱥ����ͼ������
        /// </summary>
        [ByteSize()]
        public List<IStreamParse> ChannelImageDataList
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
            ChannelImageDataList = new List<IStreamParse>(m_ChannelCount);
            for (int i = 0; i < m_ChannelCount; i++)
            {
                var compression = (ECompression)reader.ReadUInt16();
                // �˴�ֻ��Ԥ��ȡ������ȷ����ʽ����ʵ��ȡ�ŵ���Ӧ��ʽ��ͳһ����
                reader.BaseStream.Position -= 2;
                IStreamParse item = null;
                switch (compression)
                {
                    case ECompression.RawData:
                        {
                            var channelImageDataLength = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            item = new ChannelRawImageData(channelImageDataLength);
                        }
                        break;
                    case ECompression.RLECompression:
                        {
                            item = new ChannelRLEImageData(m_Height);
                        }
                        break;
                    default:
                        {
                            var channelImageDataLength = ((int)m_Depth * m_Width + 7) / 8 * m_Height;
                            item = new ChannelRawImageData(channelImageDataLength);
                        }
                        break;
                }
                item.Parse(reader);
                ChannelImageDataList.Add(item);
            }
        }
    }

}