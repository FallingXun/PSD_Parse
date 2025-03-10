using System;


namespace PsdParse
{
    public class ByteSizeAttribute : Attribute
    {
        public int Size { get; set; }

        public ByteSizeAttribute(int size = -1)
        {
            Size = size;
        }
    }
}
