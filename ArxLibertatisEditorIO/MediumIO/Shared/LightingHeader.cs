using ArxLibertatisEditorIO.RawIO.Shared;

namespace ArxLibertatisEditorIO.MediumIO.Shared
{
    public class LightingHeader
    {
        public int viewMode; // unused
        public int modeLight; // unused

        internal void ReadFrom(DANAE_IO_LIGHTINGHEADER header)
        {
            viewMode = header.viewMode;
            modeLight = header.modeLight;
        }

        internal void WriteTo(ref DANAE_IO_LIGHTINGHEADER header)
        {
            header.viewMode = viewMode;
            header.modeLight = modeLight;
            header.ipad = 0;
        }
    }
}
