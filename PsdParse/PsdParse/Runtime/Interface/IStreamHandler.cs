namespace PsdParse
{
    public interface IStreamHandler
    {
        void Parse(Reader reader);

        void Combine(Writer writer);
    }

}