using ArxLibertatisEditorIO.RawIO.Shared;

namespace ArxLibertatisEditorIO.MediumIO.Shared
{
    public class LightingHeader
    {
        public int viewMode; // unused in libertatis, used for lighting recalc it seems?
        public int modeLight; // same as above

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

        public override string ToString()
        {
            return $"View Mode: {viewMode}\n" +
                $"Mode Light: {modeLight}";
        }
    }
}
