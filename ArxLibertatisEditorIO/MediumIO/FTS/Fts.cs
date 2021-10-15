using ArxLibertatisEditorIO.MediumIO.LLF;
using ArxLibertatisEditorIO.RawIO.FTS;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Numerics;

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

        public void LoadFrom(FTS_IO fts)
        {
            header.ReadFrom(fts.header);

            subHeaders.Clear();
            for (int i = 0; i < fts.header.count; ++i)
            {
                var subHeader = new SubHeader();
                subHeader.ReadFrom(fts.uniqueHeaders[i]);
                subHeaders.Add(subHeader);
            }

            sceneHeader.ReadFrom(fts.sceneHeader);

            textureContainers.Clear();
            for (int i = 0; i < fts.textureContainers.Length; ++i)
            {
                var tc = new TextureContainer();
                tc.ReadFrom(fts.textureContainers[i]);
                textureContainers.Add(tc);
            }

            cells.Clear();
            for (int i = 0; i < fts.cells.Length; ++i)
            {
                var c = Cell.FromIndex(i, fts.sceneHeader.sizex);
                c.ReadFrom(fts.cells[i]);
                cells.Add(c);
            }

            anchors.Clear();
            for (int i = 0; i < fts.anchors.Length; ++i)
            {
                var anchor = new Anchor();
                anchor.ReadFrom(fts.anchors[i]);
                anchors.Add(anchor);
            }

            portals.Clear();
            for (int i = 0; i < fts.portals.Length; ++i)
            {
                var portal = new Portal();
                portal.ReadFrom(fts.portals[i]);
                portals.Add(portal);
            }

            rooms.Clear();
            for (int i = 0; i < fts.rooms.Length; ++i)
            {
                var room = new Room();
                room.ReadFrom(fts.rooms[i]);
                rooms.Add(room);
            }

            roomDistances.Clear();
            for (int i = 0; i < fts.roomDistances.Length; ++i)
            {
                var rd = new RoomDistance();
                rd.ReadFrom(fts.roomDistances[i]);
                roomDistances.Add(rd);
            }
        }

        public void WriteTo(FTS_IO fts)
        {
            header.WriteTo(ref fts.header);

            IOHelper.EnsureArraySize(ref fts.uniqueHeaders, subHeaders.Count);
            for (int i = 0; i < subHeaders.Count; ++i)
            {
                subHeaders[i].WriteTo(ref fts.uniqueHeaders[i]);
            }

            sceneHeader.WriteTo(ref fts.sceneHeader);

            IOHelper.EnsureArraySize(ref fts.textureContainers, textureContainers.Count);
            for (int i = 0; i < textureContainers.Count; ++i)
            {
                textureContainers[i].WriteTo(ref fts.textureContainers[i]);
            }

            int cellCountShould = fts.sceneHeader.sizex * fts.sceneHeader.sizez;
            if (cells.Count != cellCountShould)
            {
                Logging.LogWarning($"cell count is {cells.Count} but should be {cellCountShould}");
            }
            IOHelper.EnsureArraySize(ref fts.cells, cells.Count);
            for (int i = 0; i < cells.Count; ++i)
            {
                cells[i].WriteTo(ref fts.cells[i]);
            }

            IOHelper.EnsureArraySize(ref fts.anchors, anchors.Count);
            for (int i = 0; i < anchors.Count; ++i)
            {
                anchors[i].WriteTo(ref fts.anchors[i]);
            }

            IOHelper.EnsureArraySize(ref fts.portals, portals.Count);
            for (int i = 0; i < portals.Count; ++i)
            {
                portals[i].WriteTo(ref fts.portals[i]);
            }

            IOHelper.EnsureArraySize(ref fts.rooms, rooms.Count);
            for (int i = 0; i < rooms.Count; ++i)
            {
                rooms[i].WriteTo(ref fts.rooms[i]);
            }

            IOHelper.EnsureArraySize(ref fts.roomDistances, roomDistances.Count);
            for (int i = 0; i < roomDistances.Count; ++i)
            {
                roomDistances[i].WriteTo(ref fts.roomDistances[i]);
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
