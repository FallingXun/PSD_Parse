using System.IO;
using System.Text;


namespace PsdParse
{
    public class PsdFile
    {
        private FileHeaderSection m_FileHeader;
        private ColorModeDataSection m_ColorModeData;
        private ImageResourcesSection m_ImageResources;
        private LayerAndMaskInformationSection m_LayerAndMaskInformation;
        private ImageDataSection m_ImageData;

        public PsdFile(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                Read(stream, Encoding.GetEncoding("GB2312"));
            }
        }

        public PsdFile(string path, Encoding encoding)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                Read(stream, encoding);
            }
        }

        public PsdFile(string path, Encoding encoding, FileHeaderSection fileHeader, ColorModeDataSection colorModeData, ImageResourcesSection imageResources, LayerAndMaskInformationSection layerAndMaskInformation, ImageDataSection imageData)
        {
            m_FileHeader = fileHeader;
            m_ColorModeData = colorModeData;
            m_ImageResources = imageResources;
            m_LayerAndMaskInformation = layerAndMaskInformation;
            m_ImageData = imageData;
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Write(stream, Encoding.GetEncoding("GB2312"));
            }
        }

        private void Read(Stream stream, Encoding encoding)
        {
            var reader = new Reader(stream, encoding);

            m_FileHeader = new FileHeaderSection();
            m_FileHeader.Parse(reader);

            m_ColorModeData = new ColorModeDataSection();
            m_ColorModeData.Parse(reader);

            m_ImageResources = new ImageResourcesSection();
            m_ImageResources.Parse(reader);

            m_LayerAndMaskInformation = new LayerAndMaskInformationSection();
            m_LayerAndMaskInformation.Parse(reader);

            m_ImageData = new ImageDataSection(m_FileHeader.ChannelCount, m_FileHeader.Width, m_FileHeader.Height, m_FileHeader.Depth, m_FileHeader.ColorMode);
            m_ImageData.Parse(reader);
        }

        private void Write(Stream stream, Encoding encoding)
        {
            var writer = new Writer(stream, encoding);
            m_FileHeader.Combine(writer);
            m_ColorModeData.Combine(writer);
            m_ImageResources.Combine(writer);
            m_LayerAndMaskInformation.Combine(writer);
            m_ImageData.Combine(writer);
        }
    }
}