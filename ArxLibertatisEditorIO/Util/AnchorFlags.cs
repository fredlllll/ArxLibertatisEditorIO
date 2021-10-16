using System;

namespace ArxLibertatisEditorIO.Util
{
    [Flags]
    public enum AnchorFlags : short
    {
        None = 0,
        BLOCKED = 1 << 3,
    }
}
