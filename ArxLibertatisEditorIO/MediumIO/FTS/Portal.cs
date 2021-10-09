using ArxLibertatisEditorIO.RawIO.FTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Portal
    {
        public readonly PortalPolygon poly = new PortalPolygon();
        public int room_1; // facing normal
        public int room_2;
        public bool useportal; //apparently overwritten every frame in the engine? why is it even here?

        internal void ReadFrom(EERIE_IO_PORTALS portal)
        {
            poly.ReadFrom(portal.poly);
            room_1 = portal.room_1;
            room_2 = portal.room_2;
            useportal = portal.useportal != 0;
        }

        internal void WriteTo(EERIE_IO_PORTALS portal)
        {
            poly.WriteTo(portal.poly);
            portal.room_1 = room_1;
            portal.room_2 = room_2;
            portal.useportal = (short)(useportal ? 1 : 0);
        }
    }
}
