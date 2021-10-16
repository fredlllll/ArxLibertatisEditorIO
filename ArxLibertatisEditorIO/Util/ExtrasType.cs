using System;

namespace ArxLibertatisEditorIO.Util
{
    [Flags]
    public enum ExtrasType : int
    {
        None = 0,
        EXTRAS_SEMIDYNAMIC = 0x00000001,
        EXTRAS_EXTINGUISHABLE = 0x00000002, //can be ignited or doused by spells etc
        EXTRAS_STARTEXTINGUISHED = 0x00000004, //starts extinguished
        EXTRAS_SPAWNFIRE = 0x00000008, //spawns fire particle effect
        EXTRAS_SPAWNSMOKE = 0x00000010, //spawns smoke particle effect
        EXTRAS_OFF = 0x00000020,
        EXTRAS_COLORLEGACY = 0x00000040,
        EXTRAS_NOCASTED = 0x00000080, //unused, if enabled, the light should not cast shadows
        EXTRAS_FIXFLARESIZE = 0x00000100,
        EXTRAS_FIREPLACE = 0x00000200, //can prepare food
        EXTRAS_NO_IGNIT = 0x00000400,
        EXTRAS_FLARE = 0x00000800
    }
}
