using ArxLibertatisEditorIO.MediumIO.DLF;
using ArxLibertatisEditorIO.MediumIO.FTS;
using ArxLibertatisEditorIO.MediumIO.LLF;
using ArxLibertatisEditorIO.RawIO;

namespace ArxLibertatisEditorIO.MediumIO
{
    public class MediumArxLevel
    {
        public readonly RawArxLevel rawLevel;

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

        public string LevelName
        {
            get;
            private set;
        }

        public MediumArxLevel()
        {
            dlf = new Dlf();
            llf = new Llf();
            fts = new Fts();

            LevelName = "unknown";

            rawLevel = new RawArxLevel();
        }

        public void LoadLevel(string name)
        {
            LevelName = name;

            rawLevel.LoadLevel(name);

            //TODO: load from raw level dlf fts and llf
        }

        public void SaveLevel(string name)
        {
            LevelName = name;

            //TODO: write to raw dlf fts and llf

            rawLevel.SaveLevel(name);

            /*using (MemoryStream ms = new MemoryStream())
            {
                dlf.WriteTo(ms);
                ms.Position = 0;
                using (var packedStream = DLF_IO.EnsurePacked(ms))
                using (FileStream fs = new FileStream(ArxPaths.GetDlfPath(name), FileMode.Create, FileAccess.Write))
                {
                    packedStream.CopyTo(fs);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                llf.WriteTo(ms);
                ms.Position = 0;
                using (var packedStream = LLF_IO.EnsurePacked(ms))
                using (FileStream fs = new FileStream(ArxPaths.GetLlfPath(name), FileMode.Create, FileAccess.Write))
                {
                    packedStream.CopyTo(fs);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                fts.header.uncompressedsize = fts.CalculateWrittenSize(true);

                fts.sceneHeader.nb_textures = fts.textureContainers.Length;
                fts.sceneHeader.nb_polys = fts.CalculatePolyCount();
                fts.sceneHeader.nb_anchors = fts.anchors.Length;
                fts.sceneHeader.nb_portals = fts.portals.Length;
                fts.sceneHeader.nb_rooms = fts.rooms.Length - 1;

                for (int i = 0; i < fts.rooms.Length; ++i)
                {
                    fts.rooms[i].data.nb_polys = fts.rooms[i].polygons.Length;
                    fts.rooms[i].data.nb_portals = fts.rooms[i].portals.Length;
                }

                fts.WriteTo(ms);

                ms.Position = 0;
                using (var packedStream = FTS_IO.EnsurePacked(ms))
                using (FileStream fs = new FileStream(ArxPaths.GetFtsPath(name), FileMode.Create, FileAccess.Write))
                {
                    packedStream.CopyTo(fs);
                }
            }*/
        }
    }
}
