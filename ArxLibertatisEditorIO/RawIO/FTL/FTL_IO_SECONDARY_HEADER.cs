﻿using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FTL_IO_SECONDARY_HEADER
    {
        //offsets of the sections in the file, -1 means the section is not present
        public int offset_3Ddata;
        public int offset_cylinder;
        public int offset_progressive_data;
        public int offset_clothes_data;
        public int offset_collision_spheres;
        public int offset_physics_box;
    }
}
