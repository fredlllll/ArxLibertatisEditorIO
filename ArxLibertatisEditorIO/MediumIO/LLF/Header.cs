using ArxLibertatisEditorIO.RawIO.LLF;
using ArxLibertatisEditorIO.Util;
using System;

namespace ArxLibertatisEditorIO.MediumIO.LLF
{
    public class Header
    {
        public Version version;
        public string lastUser;
        public DateTimeOffset time;

        internal void ReadFrom(LLF_IO_HEADER header)
        {
            string vers = header.version.ToString("0.0000");
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1]));
            lastUser = IOHelper.GetString(header.lastuser);
            time = DateTimeOffset.FromUnixTimeSeconds(header.time);
        }

        internal void WriteTo(ref LLF_IO_HEADER header)
        {
            string vers = version.Major + "." + version.Minor;
            header.version = float.Parse(vers, System.Globalization.CultureInfo.InvariantCulture);
            header.lastuser = IOHelper.GetBytes(lastUser, 256);
            header.time = (uint)time.ToUnixTimeSeconds();

            //fixed fields
            header.identifier = IOHelper.GetBytes("DANAE_LLH_FILE", 16);
        }
    }
}
