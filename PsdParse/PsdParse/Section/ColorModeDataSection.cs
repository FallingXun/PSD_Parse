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
                reader.BaseStream.Position = endPosition;
            }
            else
            {
                throw new Exception(string.Format("PSD �ļ�����ɫģʽ���ݶΣ��쳣�����ݳ���:{0}��Length:{1}", reader.BaseStream.Position - startPosition, Length));
            }
        }
    }

}
