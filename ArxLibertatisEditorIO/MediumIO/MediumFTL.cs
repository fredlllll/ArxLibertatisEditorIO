using ArxLibertatisEditorIO.MediumIO.FTL;
using ArxLibertatisEditorIO.RawIO;
using ArxLibertatisEditorIO.RawIO.FTL;

namespace ArxLibertatisEditorIO.MediumIO
{
    public static class MediumFTL
    {
        public static Ftl LoadModel(FTL_IO rawFtl)
        {
            Ftl ftl = new Ftl();
            ftl.LoadFrom(rawFtl);
            return ftl;
        }

        public static Ftl LoadModel(string path)
        {
            var rawFtl = RawFTL.LoadModel(path);
            return LoadModel(rawFtl);
        }

        public static void SaveModel(Ftl ftl, FTL_IO rawFtl)
        {
            ftl.WriteTo(rawFtl);
        }

        public static void SaveModel(Ftl ftl, string path)
        {
            var rawFtl = new FTL_IO();
            ftl.WriteTo(rawFtl);
            RawFTL.SaveModel(rawFtl, path);
        }
    }
}
