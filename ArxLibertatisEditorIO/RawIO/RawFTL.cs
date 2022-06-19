using ArxLibertatisEditorIO.RawIO.FTL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO
{
    public static class RawFTL
    {
        public static FTL_IO LoadModel(string path)
        {
            FTL_IO ftl = new FTL_IO();

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var s = FTL_IO.EnsureUnpacked(fs);
                ftl.ReadFrom(s);
            }
            return ftl;
        }

        public static void SaveModel(FTL_IO ftl, string path)
        {
            using (var ms = new MemoryStream())
            {
                ftl.WriteTo(ms);
                var packed = FTL_IO.EnsurePacked(ms);
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    packed.CopyTo(fs);
                }
                packed.Dispose();
            }
        }
    }
}
