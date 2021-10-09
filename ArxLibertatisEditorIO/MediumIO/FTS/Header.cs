using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Header
    {
        public Version version;
        public string path;
        public DateTimeOffset time;

        internal void ReadFrom(FTS_IO_UNIQUE_HEADER header)
        {
            string vers = header.version.ToString("0.0000");
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        internal void WriteTo(FTS_IO_UNIQUE_HEADER header)
        {
            string vers = version.Major + "." + version.Minor;
            header.version = float.Parse(vers, System.Globalization.CultureInfo.InvariantCulture);
            header.path = IOHelper.GetBytes(path, 256);

            IOHelper.EnsureArraySize(ref header.pad,3);
        }
    }
}
