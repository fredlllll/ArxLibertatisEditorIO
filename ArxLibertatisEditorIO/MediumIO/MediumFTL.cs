using ArxLibertatisEditorIO.MediumIO.FTL;
using ArxLibertatisEditorIO.RawIO;
using ArxLibertatisEditorIO.RawIO.FTL;

namespace ArxLibertatisEditorIO.MediumIO
{
    public static class MediumFTL
    {
        public static Ftl LoadFrom(FTL_IO rawFtl)
        {
            Ftl ftl = new Ftl();
            ftl.LoadFrom(rawFtl);
            return ftl;
        }

        public static Ftl LoadFrom(string path)
        {
            var rawFtl = RawFTL.LoadModel(path);
            return LoadFrom(rawFtl);
        }

        public static void SaveTo(Ftl ftl, FTL_IO rawFtl)
        {
            ftl.SaveTo(rawFtl);
        }

        public static void SaveTo(Ftl ftl, string path)
        {
            var rawFtl = new FTL_IO();
            ftl.SaveTo(rawFtl);
            RawFTL.SaveModel(rawFtl, path);
        }
    }
}
