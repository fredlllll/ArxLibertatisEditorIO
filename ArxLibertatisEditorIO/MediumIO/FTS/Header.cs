using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Header
    {
        public string path;
        public Version version;

        internal void ReadFrom(FTS_IO_UNIQUE_HEADER header)
        {
            string vers = header.version.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1]));
            path = IOHelper.GetString(header.path);
        }

        internal void WriteTo(ref FTS_IO_UNIQUE_HEADER header)
        {
            string vers = version.Major + "." + version.Minor;
            header.version = float.Parse(vers, System.Globalization.CultureInfo.InvariantCulture);
            header.path = IOHelper.GetBytes(path, 256);
        }

        public override string ToString()
        {
            return $"Version: {version}\n" +
                $"Path: {path}";
        }
    }
}
