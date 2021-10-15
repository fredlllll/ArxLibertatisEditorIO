using ArxLibertatisEditorIO.RawIO.FTS;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class SceneHeader
    {
        public Version version;
        public int sizex; //TODO: put all cells into a special structure that handles this and coordinates
        public int sizez;
        public Vector3 playerpos;
        public Vector3 Mscenepos;

        internal void ReadFrom(FTS_IO_SCENE_HEADER header)
        {
            string vers = header.version.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1]));
            sizex = header.sizex;
            sizez = header.sizez;
            playerpos = header.playerpos.ToVector3();
            Mscenepos = header.Mscenepos.ToVector3();
        }

        internal void WriteTo(ref FTS_IO_SCENE_HEADER header)
        {
            string vers = version.Major + "." + version.Minor;
            header.version = float.Parse(vers, System.Globalization.CultureInfo.InvariantCulture);
            header.sizex = sizex;
            header.sizez = sizez;
            header.playerpos = new RawIO.Shared.SavedVec3(playerpos);
            header.Mscenepos = new RawIO.Shared.SavedVec3(Mscenepos);
        }

        public override string ToString()
        {
            return $"Version: {version}\n" +
                $"SizeX: {sizex}\n" +
                $"SizeZ: {sizez}\n" +
                $"Player Pos: {playerpos}\n" +
                $"Scene Pos: {Mscenepos}";
        }
    }
}
