using System.IO;
using System.Text;

public class PsdFile
{
    private FileHeaderSection m_FileHeader = new FileHeaderSection();
    private ColorModeDataSection m_ColorModeData = new ColorModeDataSection();
    private ImageResourcesSection m_ImageResources = new ImageResourcesSection();
    private LayerAndMaskInformationSection m_LayerAndMaskInformation = new LayerAndMaskInformationSection();
    private ImageDataSection m_ImageData = new ImageDataSection();

    private BinaryReader m_Reader;

    public PsdFile(Stream stream, Encoding encoding)
    {
        m_Reader = new BinaryReader(stream, encoding);
        m_FileHeader.Parse(m_Reader);
        m_ColorModeData.Parse(m_Reader);
        m_ImageResources.Parse(m_Reader);
        m_LayerAndMaskInformation.Parse(m_Reader);
        m_ImageData.Parse(m_Reader);
    }
}
