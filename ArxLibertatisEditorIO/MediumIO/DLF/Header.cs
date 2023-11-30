using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Header
    {
        public Version version = new Version(1,44);
        public string lastUser = "ArxLibertatisEditorIO";
        public DateTimeOffset time = DateTimeOffset.UtcNow;
        public Vector3 positionEdit;
        public Vector3 eulersEdit;
        public bool lighting = false;
        public Vector3 offset;

        public void LoadFrom(ref DLF_IO_HEADER header)
        {
            string vers = header.version.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture);
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1].TrimEnd('0')));
            lastUser = IOHelper.GetString(header.lastUser);
            time = DateTimeOffset.FromUnixTimeSeconds(header.time);
            positionEdit = header.positionEdit.ToVector3();
            eulersEdit = header.angleEdit.ToEuler();
            lighting = header.lighting != 0;
            offset = header.offset.ToVector3();
        }

        public void SaveTo(ref DLF_IO_HEADER header)
        {
            string vers = version.Major + "." + version.Minor;
            header.version = float.Parse(vers, System.Globalization.CultureInfo.InvariantCulture);
            header.lastUser = IOHelper.GetBytes(lastUser, 256);
            header.time = (uint)time.ToUnixTimeSeconds();
            header.positionEdit = new RawIO.Shared.SavedVec3(positionEdit);
            header.angleEdit = new RawIO.Shared.SavedAnglef(eulersEdit);
            header.lighting = lighting ? 1 : 0;
            header.offset = new RawIO.Shared.SavedVec3(offset);

            //fixed fields
            header.identifier = IOHelper.GetBytes("DANAE_FILE", 16);
        }

        public override string ToString()
        {
            return $"Version: {version}\n" +
                $"Last User: {lastUser}\n" +
                $"Time: {time}\n" +
                $"Position Edit: {positionEdit}\n" +
                $"Eulers Edit: {eulersEdit}\n" +
                $"Lighting: {lighting}\n" +
                $"Offset: {offset}";
        }
    }
}
