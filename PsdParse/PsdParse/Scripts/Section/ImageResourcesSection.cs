using System;
using System.IO;
using System.Text;

/// <summary>
/// ͼ����Դ��
/// </summary>
public class ImageResourcesSection : IStreamParse
{
    /// <summary>
    /// ͼ����Դ�γ��ȣ�4 �ֽڣ�
    /// </summary>
    public int Length
    {
        get;set;
    }

    /// <summary>
    /// ͼ����Դ������
    /// </summary>
    public ImageResourceBlock m_ImageResourceBlock;


    public void Parse(BinaryReader reader)
    {
        Length = reader.ReadInt32();
        if (Length > 0)
        {
            m_ImageResourceBlock = new ImageResourceBlock();
            m_ImageResourceBlock.Parse(reader);
        }
    }
}


/// <summary>
/// ͼ����Դ��-ͼ����Դ��
/// </summary>
public class ImageResourceBlock : IStreamParse
{
    private string m_Signature;
    /// <summary>
    /// ��ʶ����4 �ֽڣ�������Ϊ"8BIM"
    /// </summary>
    public string Signature
    {
        get
        {
            return m_Signature;
        }
        set
        {
            if (value != "8BIM")
            {
                throw new Exception(string.Format("PSD �ļ���ͼ����Դ��-ͼ����Դ�飩�쳣��Signature:{0}", value));
            }
            m_Signature = value;
        }
    }


    public void Parse(BinaryReader reader)
    {
        
    }
}