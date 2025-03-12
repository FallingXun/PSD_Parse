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


        #region PSD 文件读取

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

        private void Read(Stream stream, Encoding encoding)
        {
            using (var reader = new Reader(stream, encoding))
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

        #endregion

        #region PSD 文件写入

        public PsdFile(FileHeaderSection fileHeader, ColorModeDataSection colorModeData, ImageResourcesSection imageResources, LayerAndMaskInformationSection layerAndMaskInformation, ImageDataSection imageData, string path)
        {
            FileHeader = fileHeader;
            ColorModeData = colorModeData;
            ImageResources = imageResources;
            LayerAndMaskInformation = layerAndMaskInformation;
            ImageData = imageData;
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Write(stream, Encoding.GetEncoding("GB2312"));
            }
        }

        public PsdFile(FileHeaderSection fileHeader, ColorModeDataSection colorModeData, ImageResourcesSection imageResources, LayerAndMaskInformationSection layerAndMaskInformation, ImageDataSection imageData, string path, Encoding encoding)
        {
            FileHeader = fileHeader;
            ColorModeData = colorModeData;
            ImageResources = imageResources;
            LayerAndMaskInformation = layerAndMaskInformation;
            ImageData = imageData;
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Write(stream, encoding);
            }
        }


        private void Write(Stream stream, Encoding encoding)
        {
            using (var writer = new Writer(stream, encoding))
            {
                FileHeader.Combine(writer);
                ColorModeData.Combine(writer);
                ImageResources.Combine(writer);
                LayerAndMaskInformation.Combine(writer);
                ImageData.Combine(writer);
            }
        }

        #endregion

    }
}