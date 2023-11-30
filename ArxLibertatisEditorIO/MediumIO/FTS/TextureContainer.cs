using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class TextureContainer
    {
        public int containerId;
        public string texturePath;

        public void LoadFrom(ref FTS_IO_TEXTURE_CONTAINER ftc)
        {
            containerId = ftc.tc;
            texturePath = IOHelper.GetString(ftc.fic);
        }

        public void SaveTo(ref FTS_IO_TEXTURE_CONTAINER ftc)
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
