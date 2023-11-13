using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.CIN
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct CIN_IO_SAVEDCINEMATICTRACK
    {
        public int startframe;
        public int endframe;
        public float currframe; //why float arx???
        public float fps;
        public int nbkey;
        public int pause;
    }
}
