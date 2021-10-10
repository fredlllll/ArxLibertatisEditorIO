using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System;
using System.Numerics;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Header
    {
        public Version version;
        public string lastUser;
        public DateTimeOffset time;
        public Vector3 positionEdit;
        public Vector3 eulersEdit;
        public bool lighting;
        public Vector3 offset;

        internal void ReadFrom(DLF_IO_HEADER header)
        {
            string vers = header.version.ToString("0.0000");
            string[] parts = vers.Split('.');
            version = new Version(int.Parse(parts[0]), int.Parse(parts[1]));
            lastUser = IOHelper.GetString(header.lastUser);
            time = DateTimeOffset.FromUnixTimeSeconds(header.time);
            positionEdit = header.positionEdit.ToVector3();
            eulersEdit = header.angleEdit.ToEuler();
            lighting = header.lighting != 0;
            offset = header.offset.ToVector3();
        }

        internal void WriteTo(ref DLF_IO_HEADER header)
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
            IOHelper.EnsureArraySize(ref header.ipad1, 256);
            IOHelper.EnsureArraySize(ref header.ipad2, 250);
            IOHelper.EnsureArraySize(ref header.fpad1, 253);
            IOHelper.EnsureArraySize(ref header.cpad1, 4096);
            IOHelper.EnsureArraySize(ref header.ipad3, 256);
        }
    }
}
