using System;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// 颜色模式数据段
    /// </summary>
    public class ColorModeDataSection : IStreamParse
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

        public void Parse(BinaryReader reader, Encoding encoding)
        {
            Length = reader.ReadInt32();
            if (Length > 0)
            {
                ColorData = reader.ReadBytes(Length);
            }
        }
    }

}
