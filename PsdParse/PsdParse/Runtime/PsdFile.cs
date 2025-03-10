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

        public PsdFile(string path, Encoding encoding)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Init(stream, encoding);
        }

        private void Init(Stream stream, Encoding encoding)
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
    }
}