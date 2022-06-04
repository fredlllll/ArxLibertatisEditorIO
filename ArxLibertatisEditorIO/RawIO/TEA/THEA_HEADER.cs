using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public struct THEA_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] identity; // Theo Animation File\0
        public uint version; // libertatis code says has to be >=2014
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] anim_name;
        public int nb_frames;
        public int nb_groups;
        public int nb_key_frames;
    };
}
