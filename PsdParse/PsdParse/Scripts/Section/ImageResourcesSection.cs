using System;
using System.IO;
using System.Text;

/// <summary>
/// 图像资源段
/// </summary>
public class ImageResourcesSection : IStreamParse
{
    /// <summary>
    /// 图像资源段长度（4 字节）
    /// </summary>
    public int Length
    {
        get;set;
    }

    /// <summary>
    /// 图像资源块数据
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
/// 图像资源段-图像资源块
/// </summary>
public class ImageResourceBlock : IStreamParse
{
    private string m_Signature;
    /// <summary>
    /// 标识符（4 字节），必须为"8BIM"
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
                throw new Exception(string.Format("PSD 文件（图像资源段-图像资源块）异常，Signature:{0}", value));
            }
            m_Signature = value;
        }
    }


    public void Parse(BinaryReader reader)
    {
        
    }
}