﻿using ArxLibertatisEditorIO.RawIO.LLF;
using ArxLibertatisEditorIO.Util;
using System;

namespace ArxLibertatisEditorIO.MediumIO.LLF
{
    public class Header
    {
        public Version version = new Version(1,44);
        public string lastUser = "ArxLibertatisEditorIO";
        public DateTimeOffset time = DateTimeOffset.UtcNow;

        public void LoadFrom(LLF_IO_HEADER header)
        {
            string vers = header.version.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1].TrimEnd('0')));
            lastUser = IOHelper.GetString(header.lastuser);
            time = DateTimeOffset.FromUnixTimeSeconds(header.time);
        }

        public void SaveTo(ref LLF_IO_HEADER header)
        {
            string vers = version.Major + "." + version.Minor;
            header.version = float.Parse(vers, System.Globalization.CultureInfo.InvariantCulture);
            header.lastuser = IOHelper.GetBytes(lastUser, 256);
            header.time = (uint)time.ToUnixTimeSeconds();

            //fixed fields
            header.identifier = IOHelper.GetBytes("DANAE_LLH_FILE", 16);
        }

        public override string ToString()
        {
            return $"Version: {version}\n" +
                $"Last User: {lastUser}\n" +
                $"Time: {time}";
        }
    }
}
