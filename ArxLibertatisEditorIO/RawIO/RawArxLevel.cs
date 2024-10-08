﻿using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.RawIO.LLF;
using ArxLibertatisEditorIO.Util;
using System.IO;

namespace ArxLibertatisEditorIO.RawIO
{
    public class RawArxLevel
    {
        private DLF_IO dlf;
        public DLF_IO DLF
        {
            get { return dlf; }
            set { dlf = value; }
        }
        private LLF_IO llf;
        public LLF_IO LLF
        {
            get { return llf; }
            set { llf = value; }
        }
        private FTS_IO fts;
        public FTS_IO FTS
        {
            get { return fts; }
            set { fts = value; }
        }

        public string LevelName
        {
            get;
            set;
        }

        public RawArxLevel()
        {
            dlf = new DLF_IO();
            llf = new LLF_IO();
            fts = new FTS_IO();

            LevelName = "unknown";
        }

        /// <summary>
        /// Loads the level from the 3 level files
        /// </summary>
        /// <param name="dlfPath">path to dlf</param>
        /// <param name="llfPath">path to llf</param>
        /// <param name="ftsPath">path to fts</param>
        /// <param name="allowPartialLevel">if true, allows any of the 3 files to be missing. otherwise exceptions will be thrown for missing files. default false</param>
        public void LoadLevel(string dlfPath, string llfPath, string ftsPath, bool allowPartialLevel = false)
        {
            if (!allowPartialLevel || File.Exists(dlfPath))
            {
                using (FileStream fs = new FileStream(dlfPath, FileMode.Open, FileAccess.Read))
                {
                    dlf.ReadFrom(DLF_IO.EnsureUnpacked(fs));
                }
            }
            if (!allowPartialLevel || File.Exists(llfPath))
            {
                using (FileStream fs = new FileStream(llfPath, FileMode.Open, FileAccess.Read))
                {
                    llf.ReadFrom(LLF_IO.EnsureUnpacked(fs));
                }
            }
            if (!allowPartialLevel || File.Exists(ftsPath))
            {
                using (FileStream fs = new FileStream(ftsPath, FileMode.Open, FileAccess.Read))
                {
                    fts.ReadFrom(FTS_IO.EnsureUnpacked(fs));
                }
            }
        }

        public RawArxLevel LoadLevel(string name)
        {
            LevelName = name;

            string dlfPath = ArxPaths.GetDlfPath(name);
            string llfPath = ArxPaths.GetLlfPath(name);
            string ftsPath = ArxPaths.GetFtsPath(name);

            LoadLevel(dlfPath, llfPath, ftsPath);
            return this;
        }

        public void SaveLevel(string dlfPath, string llfPath, string ftsPath, bool compressFts)
        {
            if (dlf.scenes.Length > 0)
            {
                //make sure the fts we save is loaded as scene, if scene is present in dlf
                dlf.scenes[0].name = IOHelper.GetBytes("Graph\\Levels\\" + Path.GetFileName(Path.GetDirectoryName(ftsPath)), 512);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                dlf.WriteTo(ms);
                ms.Position = 0;
                Directory.CreateDirectory(Path.GetDirectoryName(dlfPath));
                using var packedStream = DLF_IO.EnsurePacked(ms);
                using FileStream fs = new FileStream(dlfPath, FileMode.Create, FileAccess.Write);
                packedStream.CopyTo(fs);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                llf.WriteTo(ms);
                ms.Position = 0;
                Directory.CreateDirectory(Path.GetDirectoryName(llfPath));
                using var packedStream = LLF_IO.EnsurePacked(ms);
                using FileStream fs = new FileStream(llfPath, FileMode.Create, FileAccess.Write);
                packedStream.CopyTo(fs);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                fts.WriteTo(ms, compressFts);

                ms.Position = 0;
                Directory.CreateDirectory(Path.GetDirectoryName(ftsPath));
                using FileStream fs = new FileStream(ftsPath, FileMode.Create, FileAccess.Write);
                if (compressFts)
                {
                    using var packedStream = FTS_IO.EnsurePacked(ms);
                    packedStream.CopyTo(fs);
                }
                else
                {
                    ms.CopyTo(fs);
                }
            }
        }

        public void SaveLevel(string name, bool compressFts)
        {
            LevelName = name;

            string dlfPath = ArxPaths.GetDlfPath(name);
            string llfPath = ArxPaths.GetLlfPath(name);
            string ftsPath = ArxPaths.GetFtsPath(name);

            SaveLevel(dlfPath, llfPath, ftsPath, compressFts);
        }

        public void SaveLevel(bool compressFts)
        {
            SaveLevel(LevelName, compressFts);
        }
    }
}
