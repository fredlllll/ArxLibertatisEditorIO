﻿using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTL_IO_3D_DATA_HEADER
    {
        public int nb_vertex;
        public int nb_faces;
        public int nb_maps;
        public int nb_groups;
        public int nb_action;
        public int nb_selections; // data will follow this order
        public int origin; // TODO this is always >= 0 replace with u32

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] name;
    }
}
