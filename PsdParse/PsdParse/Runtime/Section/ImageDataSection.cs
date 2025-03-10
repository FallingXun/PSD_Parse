
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
        /// ѹ��������2 �ֽڣ�
        /// </summary>
        [ByteSize(2)]
        public ECompression Compression
        {
            get; set;
        }

        /// <summary>
        /// ����ͨ����ͼ��
        ///     RLEѹ��ʱ��ÿ��ͨ�����ֽ����飬ǰ�沿��Ϊÿһ�е����ݳ��ȣ�����Ϊ LayerBottom - LayerTop ��ÿ�����ݳ���Ϊ 2 �ֽڣ�PSB Ϊ 4 �ֽڣ��������еĳ��Ⱥ����ͼ������
        /// </summary>
        [ByteSize()]
        public IStreamParse ImageData
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

            switch (Compression)
            {
                case ECompression.RawData:
                    {
                        ImageData = new RawImageData(m_ChannelCount, m_Width, m_Height, m_Depth);
                    }
                    break;
                case ECompression.RLECompression:
                    {
                        ImageData = new RLEImageData(m_ChannelCount, m_Height);
                    }
                    break;
                default:
                    {
                        ImageData = new RawImageData(m_ChannelCount, m_Width, m_Height, m_Depth);
                    }
                    break;
            }
            ImageData.Parse(reader);
        }
    }

}