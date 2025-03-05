
using System.IO;
using System.Text;
using System;

namespace PsdParse
{
    public interface IStreamParse
    {
        void Parse(BinaryReader reader, Encoding encoding);
    }

}