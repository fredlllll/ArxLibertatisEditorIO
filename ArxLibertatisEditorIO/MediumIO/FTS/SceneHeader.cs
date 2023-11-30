using ArxLibertatisEditorIO.RawIO.FTS;
using System;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class SceneHeader
    {
        public Version version = new Version(0,141);
        public int sizex = 160;
        public int sizez = 160;
        public Vector3 playerpos;
        public Vector3 Mscenepos;

        public void LoadFrom(ref FTS_IO_SCENE_HEADER header)
        {
            string vers = header.version.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1].TrimEnd('0')));
            sizex = header.sizex;
            sizez = header.sizez;
            playerpos = header.playerpos.ToVector3();
            Mscenepos = header.Mscenepos.ToVector3();
        }

        public void SaveTo(ref FTS_IO_SCENE_HEADER header)
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
