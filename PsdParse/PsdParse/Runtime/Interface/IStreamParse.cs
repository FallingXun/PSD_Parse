
using System.IO;
using System.Text;
using System;

namespace PsdParse
{
    public interface IStreamParse
    {
        void Parse(Reader reader);
    }

}