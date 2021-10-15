﻿using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.RawIO.LLF;
using System;
using System.IO;

namespace ArxLibertatisEditorIO.RawIO
{
    public class RawArxLevel
    {
        private readonly DLF_IO dlf;
        public DLF_IO DLF
        {
            get { return dlf; }
        }
        private readonly LLF_IO llf;
        public LLF_IO LLF
        {
            get { return llf; }
        }
        private readonly FTS_IO fts;
        public FTS_IO FTS
        {
            get { return fts; }
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

        public void LoadLevel(string dlfPath, string llfPath, string ftsPath)
        {
            using (FileStream fs = new FileStream(dlfPath, FileMode.Open, FileAccess.Read))
            {
                dlf.LoadFrom(DLF_IO.EnsureUnpacked(fs));
            }

            using (FileStream fs = new FileStream(llfPath, FileMode.Open, FileAccess.Read))
            {
                llf.LoadFrom(LLF_IO.EnsureUnpacked(fs));
            }

            using (FileStream fs = new FileStream(ftsPath, FileMode.Open, FileAccess.Read))
            {
                fts.LoadFrom(FTS_IO.EnsureUnpacked(fs));
            }
        }

        public void LoadLevel(string name)
        {
            LevelName = name;

            string dlfPath = ArxPaths.GetDlfPath(name);
            string llfPath = ArxPaths.GetLlfPath(name);
            string ftsPath = ArxPaths.GetFtsPath(name);

            LoadLevel(dlfPath, llfPath, ftsPath);
        }

        public void SaveLevel(string dlfPath, string llfPath, string ftsPath)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                dlf.WriteTo(ms);
                ms.Position = 0;
                Directory.CreateDirectory(Path.GetDirectoryName(dlfPath));
                using (var packedStream = DLF_IO.EnsurePacked(ms))
                using (FileStream fs = new FileStream(dlfPath, FileMode.Create, FileAccess.Write))
                {
                    packedStream.CopyTo(fs);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                llf.WriteTo(ms);
                ms.Position = 0;
                Directory.CreateDirectory(Path.GetDirectoryName(llfPath));
                using (var packedStream = LLF_IO.EnsurePacked(ms))
                using (FileStream fs = new FileStream(llfPath, FileMode.Create, FileAccess.Write))
                {
                    packedStream.CopyTo(fs);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                fts.WriteTo(ms);

                ms.Position = 0;
                Directory.CreateDirectory(Path.GetDirectoryName(ftsPath));
                using (var packedStream = FTS_IO.EnsurePacked(ms))
                using (FileStream fs = new FileStream(ftsPath, FileMode.Create, FileAccess.Write))
                {
                    packedStream.CopyTo(fs);
                }
            }
        }

        public void SaveLevel(string name)
        {
            LevelName = name;

            string dlfPath = ArxPaths.GetDlfPath(name);
            string llfPath = ArxPaths.GetLlfPath(name);
            string ftsPath = ArxPaths.GetFtsPath(name);

            SaveLevel(dlfPath, llfPath, ftsPath);
        }

        public void SaveLevel()
        {
            SaveLevel(LevelName);
        }
    }
}
