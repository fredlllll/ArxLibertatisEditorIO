using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.MediumIO.FTS
{
    public class Fts
    {
        public readonly Header header = new Header();
        public readonly List<SubHeader> subHeaders = new List<SubHeader>();
        public readonly SceneHeader sceneHeader = new SceneHeader();
        public readonly List<TextureContainer> textureContainers = new List<TextureContainer>();
        public readonly List<Cell> cells = new List<Cell>();
        public readonly List<Anchor> anchors = new List<Anchor>();
        public readonly List<Portal> portals = new List<Portal>();
        public readonly List<Room> rooms = new List<Room>();
        public readonly List<RoomDistance> roomDistances = new List<RoomDistance>();

        public Fts()
        {
            //prefill grid
            for (int i = 0; i < 160 * 160; ++i)
            {
                cells.Add(Cell.FromIndex(i, 160));
            }
            rooms.Add(new Room()); //at least 2 rooms have to exist in a level cause of an assert, and nb_rooms being off by one
            rooms.Add(new Room());
            roomDistances.Add(new RoomDistance());
        }

        public void LoadFrom(FTS_IO fts)
        {
            header.LoadFrom(ref fts.header);

            subHeaders.Clear();
            for (int i = 0; i < fts.header.count; ++i)
            {
                var subHeader = new SubHeader();
                subHeader.LoadFrom(ref fts.uniqueHeaders[i]);
                subHeaders.Add(subHeader);
            }

            sceneHeader.LoadFrom(ref fts.sceneHeader);

            textureContainers.Clear();
            for (int i = 0; i < fts.textureContainers.Length; ++i)
            {
                var tc = new TextureContainer();
                tc.LoadFrom(ref fts.textureContainers[i]);
                textureContainers.Add(tc);
            }

            cells.Clear();
            for (int i = 0; i < fts.cells.Length; ++i)
            {
                var c = Cell.FromIndex(i, fts.sceneHeader.sizex);
                c.LoadFrom(fts.cells[i]);
                cells.Add(c);
            }

            anchors.Clear();
            for (int i = 0; i < fts.anchors.Length; ++i)
            {
                var anchor = new Anchor();
                anchor.LoadFrom(fts.anchors[i]);
                anchors.Add(anchor);
            }

            portals.Clear();
            for (int i = 0; i < fts.portals.Length; ++i)
            {
                var portal = new Portal();
                portal.LoadFrom(ref fts.portals[i]);
                portals.Add(portal);
            }

            rooms.Clear();
            for (int i = 0; i < fts.rooms.Length; ++i)
            {
                var room = new Room();
                room.LoadFrom(fts.rooms[i]);
                rooms.Add(room);
            }

            roomDistances.Clear();
            for (int i = 0; i < fts.roomDistances.Length; ++i)
            {
                var rd = new RoomDistance();
                rd.LoadFrom(ref fts.roomDistances[i]);
                roomDistances.Add(rd);
            }
        }

        public void SaveTo(FTS_IO fts)
        {
            header.SaveTo(ref fts.header);

            IOHelper.EnsureArraySize(ref fts.uniqueHeaders, subHeaders.Count);
            for (int i = 0; i < subHeaders.Count; ++i)
            {
                subHeaders[i].SaveTo(ref fts.uniqueHeaders[i]);
            }

            sceneHeader.SaveTo(ref fts.sceneHeader);

            IOHelper.EnsureArraySize(ref fts.textureContainers, textureContainers.Count);
            for (int i = 0; i < textureContainers.Count; ++i)
            {
                textureContainers[i].SaveTo(ref fts.textureContainers[i]);
            }

            IOHelper.EnsureArraySize(ref fts.cells, cells.Count);
            for (int i = 0; i < cells.Count; ++i)
            {
                cells[i].SaveTo(fts.cells[i]);
            }

            IOHelper.EnsureArraySize(ref fts.anchors, anchors.Count);
            for (int i = 0; i < anchors.Count; ++i)
            {
                anchors[i].SaveTo(fts.anchors[i]);
            }

            IOHelper.EnsureArraySize(ref fts.portals, portals.Count);
            for (int i = 0; i < portals.Count; ++i)
            {
                portals[i].SaveTo(ref fts.portals[i]);
            }

            IOHelper.EnsureArraySize(ref fts.rooms, rooms.Count);
            for (int i = 0; i < rooms.Count; ++i)
            {
                rooms[i].SaveTo(fts.rooms[i]);
            }

            IOHelper.EnsureArraySize(ref fts.roomDistances, roomDistances.Count);
            for (int i = 0; i < roomDistances.Count; ++i)
            {
                roomDistances[i].SaveTo(ref fts.roomDistances[i]);
            }
        }
        public override string ToString()
        {
            return $"Header:\n{Output.Indent(header.ToString())}\n" +
                $"Sub Headers({subHeaders.Count}):\n{Output.ToString(subHeaders)}\n" +
                $"Scene Header:\n{Output.Indent(sceneHeader.ToString())}\n" +
                $"Texture Containers({textureContainers.Count}):\n{Output.ToString(textureContainers)}\n" +
                $"Cells({cells.Count}):\n{Output.ToString(cells)}\n" +
                $"Anchors({anchors.Count}):\n{Output.ToString(anchors)}\n" +
                $"Portals({portals.Count}):\n{Output.ToString(portals)}\n" +
                $"Rooms({rooms.Count}):\n{Output.ToString(rooms)}\n" +
                $"Room Distances({roomDistances.Count}):\n{Output.ToString(roomDistances)}";
        }
    }
}
