using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Fts
    {
        public readonly Header header = new Header();
        public readonly List<SubHeader> subHeaders = new List<SubHeader>();
        public readonly SceneHeader sceneHeader = new SceneHeader();
        public readonly List<Polygon> polygons = new List<Polygon>();
        public readonly List<Anchor> anchors = new List<Anchor>();
        public readonly List<Portal> portals = new List<Portal>();

        internal readonly Dictionary<int, string> tcToPath = new Dictionary<int, string>();
        internal readonly Dictionary<string, int> pathToTc = new Dictionary<string, int>();

        public void LoadFrom(FTS_IO fts)
        {
            header.ReadFrom(fts.header);

            subHeaders.Clear();
            for (int i = 0; i < fts.header.count; ++i)
            {
                var subHeader = new SubHeader();
                subHeader.ReadFrom(fts.uniqueHeaders[i]);
                subHeaders.Add(subHeader);
            }

            sceneHeader.ReadFrom(fts.sceneHeader);

            tcToPath.Clear();
            for (int i = 0; i < fts.textureContainers.Length; ++i)
            {
                var tc = fts.textureContainers[i];
                tcToPath[tc.tc] = IOHelper.GetString(tc.fic);
            }

            portals.Clear();
            for (int i = 0; i < fts.portals.Length; ++i)
            {
                var portal = new Portal();
                portal.ReadFrom(fts.portals[i]);
                portals.Add(portal);
            }

            polygons.Clear();
            for (int i = 0; i < fts.cells.Length; ++i)
            {
                var c = fts.cells[i];
                for (int j = 0; j < c.polygons.Length; ++j)
                {
                    var p = c.polygons[j];
                    var poly = new Polygon();
                    poly.ReadFrom(this, p);
                    polygons.Add(poly);
                }
            }

            anchors.Clear();
            for (int i = 0; i < fts.anchors.Length; ++i)
            {
                var anchor = new Anchor();
                anchor.ReadFrom(fts.anchors[i]);
                anchors.Add(anchor);
                //TODO: make anchor links references instead of indices
            }


            tcToPath.Clear();
        }

        public void WriteTo(FTS_IO fts)
        {
            header.WriteTo(fts.header);

            IOHelper.EnsureArraySize(ref fts.uniqueHeaders, subHeaders.Count);
            for(int i = 0; i < subHeaders.Count;++i)
            {
                subHeaders[i].WriteTo(ref fts.uniqueHeaders[i]);
            }

            sceneHeader.WriteTo(fts.sceneHeader);

            //TODO: rest 
        }
    }
}
