using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class TextureContainer
    {
        public int containerId;
        public string texturePath;

        internal void ReadFrom(FTS_IO_TEXTURE_CONTAINER ftc)
        {
            containerId = ftc.tc;
            texturePath = IOHelper.GetString(ftc.fic);
        }

        internal void WriteTo(ref FTS_IO_TEXTURE_CONTAINER ftc)
        {
            ftc.tc = containerId;
            ftc.fic = IOHelper.GetBytes(texturePath, 256);
        }

        public override string ToString()
        {
            return $"ID: {containerId}\n" +
                $"Path: {texturePath}";
        }
    }
}
