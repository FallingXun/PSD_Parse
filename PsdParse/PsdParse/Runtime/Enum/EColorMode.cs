namespace PsdParse
{/// <summary>
 /// �ļ�����ɫģʽ
 /// </summary>
    public enum EColorMode : short
    {
        /// <summary>
        /// λͼ
        /// </summary>
        Bitmap = 0,
        /// <summary>
        /// �Ҷ�
        /// </summary>
        Grayscale = 1,
        /// <summary>
        /// ����
        /// </summary>
        Indexed = 2,
        /// <summary>
        /// RGB
        /// </summary>
        RGB = 3,
        /// <summary>
        /// CMYK
        /// </summary>
        CMYK = 4,
        /// <summary>
        /// ��ͨ��
        /// </summary>
        Multichannel = 7,
        /// <summary>
        /// ˫��
        /// </summary>
        Duotone = 8,
        /// <summary>
        /// ʵ����
        /// </summary>
        Lab = 9,
    }
}