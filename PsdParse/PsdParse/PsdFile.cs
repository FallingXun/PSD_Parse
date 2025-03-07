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

        public PsdFile(Stream stream, Encoding encoding)
        {
            var reader = new BinaryReader(stream);

            m_FileHeader = new FileHeaderSection();
            m_FileHeader.Parse(reader, encoding);

            m_ColorModeData = new ColorModeDataSection();
            m_ColorModeData.Parse(reader, encoding);

            m_ImageResources = new ImageResourcesSection();
            m_ImageResources.Parse(reader, encoding);

            m_LayerAndMaskInformation = new LayerAndMaskInformationSection();
            m_LayerAndMaskInformation.Parse(reader, encoding);

            m_ImageData = new ImageDataSection();
            m_ImageData.Parse(reader, encoding);
        }
    }
}