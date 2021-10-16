using System;

namespace ArxLibertatisEditorIO.Util
{
    [Flags]
    public enum PathFlags : short
    {
        None = 0,
        AMBIANCE = 1 << 1,
        RGB = 1 << 2,
        FARCLIP = 1 << 3,
    }
}
