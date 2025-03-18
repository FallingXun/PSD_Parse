using System.IO;
using System.Text;


namespace PsdParse
{
    public class PsdFile
    {
        public FileHeaderSection FileHeader
        {
            get; private set;
        }
        public ColorModeDataSection ColorModeData
        {
            get; private set;
        }
        public ImageResourcesSection ImageResources
        {
            get; private set;
        }
        public LayerAndMaskInformationSection LayerAndMaskInformation
        {
            get; private set;
        }
        public ImageDataSection ImageData
        {
            get; private set;
        }

        private string m_Path;
        private Encoding m_Encoding;

        public PsdFile(string path)
        {
            m_Path = path;
            m_Encoding = Encoding.GetEncoding("GB2312");
        }

        public PsdFile(string path, Encoding encoding)
        {
            m_Path = path;
            m_Encoding = encoding;
        }


        public PsdFile(string path, FileHeaderSection fileHeader, ColorModeDataSection colorModeData, ImageResourcesSection imageResources, LayerAndMaskInformationSection layerAndMaskInformation, ImageDataSection imageData) : this(path)
        {
            FileHeader = fileHeader;
            ColorModeData = colorModeData;
            ImageResources = imageResources;
            LayerAndMaskInformation = layerAndMaskInformation;
            ImageData = imageData;
        }

        public PsdFile(string path, Encoding encoding, FileHeaderSection fileHeader, ColorModeDataSection colorModeData, ImageResourcesSection imageResources, LayerAndMaskInformationSection layerAndMaskInformation, ImageDataSection imageData) : this(path, encoding)
        {
            FileHeader = fileHeader;
            ColorModeData = colorModeData;
            ImageResources = imageResources;
            LayerAndMaskInformation = layerAndMaskInformation;
            ImageData = imageData;
        }


        public void Read()
        {
            using (FileStream stream = new FileStream(m_Path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new Reader(stream, m_Encoding))
                {
                    FileHeader = new FileHeaderSection();
                    FileHeader.Parse(reader);

                    ColorModeData = new ColorModeDataSection();
                    ColorModeData.Parse(reader);

                    ImageResources = new ImageResourcesSection();
                    ImageResources.Parse(reader);

                    LayerAndMaskInformation = new LayerAndMaskInformationSection();
                    LayerAndMaskInformation.Parse(reader);

                    ImageData = new ImageDataSection(FileHeader.ChannelCount, FileHeader.Width, FileHeader.Height, FileHeader.Depth, FileHeader.ColorMode);
                    ImageData.Parse(reader);
                }
            }
        }


        public void Write()
        {
            using (FileStream stream = new FileStream(m_Path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var writer = new Writer(stream, m_Encoding))
                {
                    FileHeader.Combine(writer);
                    ColorModeData.Combine(writer);
                    ImageResources.Combine(writer);
                    LayerAndMaskInformation.Combine(writer);
                    ImageData.Combine(writer);
                }
            }
        }

    }
}