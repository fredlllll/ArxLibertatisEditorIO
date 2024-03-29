﻿using ArxLibertatisEditorIO.RawIO.Shared;
using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.DLF
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DLF_IO_FOG
    {
        public SavedVec3 pos;
        public SavedColor rgb;
        public float size;
        public FogSpecial special;
        public float scale;
        public SavedVec3 move;
        public SavedAnglef angle;
        public float speed;
        public float rotatespeed;
        public int tolive;
        public int blend;
        public float frequency;
        public fixed float fpadd[32];
        public fixed int lpadd[32];
        public fixed byte cpadd[256];
    }
}
