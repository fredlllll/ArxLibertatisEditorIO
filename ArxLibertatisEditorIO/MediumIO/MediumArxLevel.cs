using ArxLibertatisEditorIO.MediumIO.DLF;
using ArxLibertatisEditorIO.MediumIO.FTS;
using ArxLibertatisEditorIO.MediumIO.LLF;
using ArxLibertatisEditorIO.RawIO;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO
{
    public class MediumArxLevel
    {
        private readonly Dlf dlf;
        public Dlf DLF
        {
            get { return dlf; }
        }
        private readonly Llf llf;
        public Llf LLF
        {
            get { return llf; }
        }
        private readonly Fts fts;
        public Fts FTS
        {
            get { return fts; }
        }

        public MediumArxLevel()
        {
            dlf = new Dlf();
            llf = new Llf();
            fts = new Fts();
        }

        public RawArxLevel LoadLevel(RawArxLevel raw)
        {
            dlf.LoadFrom(raw.DLF);
            llf.LoadFrom(raw.LLF);
            fts.LoadFrom(raw.FTS);
            return raw;
        }

        public RawArxLevel SaveLevel(RawArxLevel raw)
        {
            dlf.WriteTo(raw.DLF);
            fts.WriteTo(raw.FTS);
            llf.WriteTo(raw.LLF);
            return raw;
        }

        public override string ToString()
        {
            return $"DLF:\n{Output.Indent(dlf.ToString())}\n" +
                $"FTS:\n{Output.Indent(fts.ToString())}\n" +
                $"LLF:\n{Output.Indent(llf.ToString())}";
        }
    }
}
