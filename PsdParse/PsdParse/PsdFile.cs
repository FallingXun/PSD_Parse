using System.IO;
using System.Text;


namespace PsdParse
{
    public class PsdFile
    {
        private FileHeaderSection m_FileHeader = new FileHeaderSection();
        private ColorModeDataSection m_ColorModeData = new ColorModeDataSection();
        private ImageResourcesSection m_ImageResources = new ImageResourcesSection();
        private LayerAndMaskInformationSection m_LayerAndMaskInformation = new LayerAndMaskInformationSection();
        private ImageDataSection m_ImageData = new ImageDataSection();

        public PsdFile(Stream stream, Encoding encoding)
        {
            var reader = new BinaryReader(stream);
            m_FileHeader.Parse(reader, encoding);
            m_ColorModeData.Parse(reader, encoding);
            m_ImageResources.Parse(reader, encoding);
            m_LayerAndMaskInformation.Parse(reader, encoding);
            m_ImageData.Parse(reader, encoding);
        }
    }
}