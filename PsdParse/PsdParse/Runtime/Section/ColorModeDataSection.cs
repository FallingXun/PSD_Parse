using System;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 颜色模式数据段
    /// </summary>
    public class ColorModeDataSection : IStreamHandler
    {
        /// <summary>
        /// 颜色数据长度（4 字节）
        /// </summary>
        public int Length
        {
            get; set;
        }

        /// <summary>
        /// 颜色数据，只有索引颜色和双色调具有颜色模式数据。
        ///     索引彩色图像：长度为768，颜色数据以非交错顺序包含图像的颜色表。
        ///     双色调图像：颜色数据包含双色调规范（其格式未记录）。其他读取Photoshop文件的应用程序可以将双色调图像视为灰度图像，在读取和写入文件时只保留双色调信息的内容。
        /// </summary>
        public byte[] ColorData
        {
            get; set;
        }

        public void Parse(Reader reader)
        {
            Length = reader.ReadInt32();
            var startPosition = reader.BaseStream.Position;
            var endPosition = startPosition + Length;
            if (Length > 0)
            {
                ColorData = reader.ReadBytes(Length);
            }
            if (reader.BaseStream.Position <= endPosition)
            {
                reader.ReadPadding((uint)(endPosition - reader.BaseStream.Position));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（颜色模式数据段）异常，数据超长:{0}，Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }


        public void Combine(Writer writer)
        {
            writer.WriteInt32(Length);
            var startPosition = writer.BaseStream.Position;
            var endPosition = startPosition + Length;
            if (Length > 0)
            {
                writer.WriteBytes(ColorData);
            }
            if (writer.BaseStream.Position <= endPosition)
            {
                writer.WritePadding((uint)(endPosition - writer.BaseStream.Position));
            }
            else
            {
                throw new Exception(string.Format("PSD 文件（颜色模式数据段）异常，数据超长:{0}，Length:{1}", writer.BaseStream.Position - startPosition, Length));
            }
        }

    }

}
