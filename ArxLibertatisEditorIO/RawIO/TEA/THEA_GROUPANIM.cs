using ArxLibertatisEditorIO.RawIO.Shared;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public struct THEA_GROUPANIM
    {
        public bool key_group;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] angle; //ignored
        public ArxQuat quaternion;
        public SavedVec3 translate;
        public SavedVec3 zoom;
    }
}
