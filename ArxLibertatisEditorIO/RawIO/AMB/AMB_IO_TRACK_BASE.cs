using ArxLibertatisEditorIO.Util;
using System;

namespace ArxLibertatisEditorIO.RawIO.AMB
{
    [Flags]
    public enum AMB_IO_TRACK_FLAGS : uint
    {
        POSITION = 0x00000001,
        MASTER = 0x00000004,
        PAUSED = 0x00000010,
        PREFETCHED = 0x00000020
    }

    public abstract class AMB_IO_TRACK_BASE
    {
        public byte[] sampleName;
        public AMB_IO_TRACK_FLAGS flags;
        public uint keyCount;
        public AMB_IO_KEY[] keys;

        public abstract void ReadFrom(StructReader reader);
        public abstract void WriteTo(StructWriter writer);
    }
}
