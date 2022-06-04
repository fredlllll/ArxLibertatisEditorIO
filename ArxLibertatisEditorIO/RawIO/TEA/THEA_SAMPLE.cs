using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArxLibertatisEditorIO.RawIO.TEA
{
    public struct THEA_SAMPLE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] sample_name;
        public int sample_size;
    }
}
