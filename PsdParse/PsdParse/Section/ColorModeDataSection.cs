using System;
using System.IO;
using System.Text;

namespace PsdParse
{
    /// <summary>
    /// ��ɫģʽ���ݶ�
    /// </summary>
    public class ColorModeDataSection : IStreamParse
    {
        /// <summary>
        /// ��ɫ���ݳ��ȣ�4 �ֽڣ�
        /// </summary>
        public int Length
        {
            get; set;
        }

        /// <summary>
        /// ��ɫ���ݣ�ֻ��������ɫ��˫ɫ��������ɫģʽ���ݡ�
        ///     ������ɫͼ�񣺳���Ϊ768����ɫ�����Էǽ���˳�����ͼ�����ɫ��
        ///     ˫ɫ��ͼ����ɫ���ݰ���˫ɫ���淶�����ʽδ��¼����������ȡPhotoshop�ļ���Ӧ�ó�����Խ�˫ɫ��ͼ����Ϊ�Ҷ�ͼ���ڶ�ȡ��д���ļ�ʱֻ����˫ɫ����Ϣ�����ݡ�
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
