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
        public readonly List<Polygon> polygons = new List<Polygon>();
        public readonly List<Anchor> anchors = new List<Anchor>();
        public readonly List<Portal> portals = new List<Portal>();

        internal readonly Dictionary<int, string> tcToPath = new Dictionary<int, string>();
        internal readonly Dictionary<string, int> pathToTc = new Dictionary<string, int>();
        internal readonly Dictionary<Anchor, int> anchorToIndex = new Dictionary<Anchor, int>();

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

            tcToPath.Clear();
            for (int i = 0; i < fts.textureContainers.Length; ++i)
            {
                var tc = fts.textureContainers[i];
                tcToPath[tc.tc] = IOHelper.GetString(tc.fic);
            }

            portals.Clear();
            for (int i = 0; i < fts.portals.Length; ++i)
            {
                var portal = new Portal();
                portal.ReadFrom(fts.portals[i]);
                portals.Add(portal);
            }

            polygons.Clear();
            for (int i = 0; i < fts.cells.Length; ++i)
            {
                var c = fts.cells[i];
                for (int j = 0; j < c.polygons.Length; ++j)
                {
                    var p = c.polygons[j];
                    var poly = new Polygon();
                    poly.ReadFrom(this, p);
                    polygons.Add(poly);
                }
            }

            anchors.Clear();
            for (int i = 0; i < fts.anchors.Length; ++i)
            {
                var anchor = new Anchor();
                anchor.ReadFrom(fts.anchors[i]);
                anchors.Add(anchor);
                //TODO: make anchor links references instead of indices
            }


            tcToPath.Clear();
        }

        internal class LevelCell
        {
            public readonly int x, z;
            public readonly List<Polygon> polygons = new List<Polygon>();
            public readonly List<int> anchors = new List<int>();

            public LevelCell(int x, int z)
            {
                this.x = x;
                this.z = z;
            }

            public void AddPolygon(Polygon poly)
            {
                polygons.Add(poly);
            }

            public void AddAnchor(int index)
            {
                anchors.Add(index);
            }

            public void WriteTo(Fts fts, ref FTS_IO_CELL cell)
            {
                cell.sceneInfo.nbianchors = anchors.Count;
                cell.sceneInfo.nbpoly = polygons.Count;
                IOHelper.EnsureArraySize(ref cell.polygons, polygons.Count);
                IOHelper.EnsureArraySize(ref cell.anchors, anchors.Count);

                for (int i = 0; i < polygons.Count; ++i)
                {
                    polygons[i].WriteTo(fts, ref cell.polygons[i]);
                }
                for (int i = 0; i < anchors.Count; ++i)
                {
                    cell.anchors[i] = anchors[i];
                }
            }
        }

        internal class RoomPolygon
        {
            public short cellX, cellZ, polyIndex;

            public void WriteTo(ref FTS_IO_EP_DATA rp)
            {
                rp.cell_x = cellX;
                rp.cell_z = cellZ;
                rp.idx = polyIndex;
            }
        }

        internal class LevelRoom
        {
            public readonly List<RoomPolygon> polygons = new List<RoomPolygon>();
            public readonly List<int> portals = new List<int>();
            //TODO: portals

            public void WriteTo(ref FTS_IO_ROOM room)
            {
                room.data.nb_polys = polygons.Count;
                room.data.nb_portals = portals.Count;

                IOHelper.EnsureArraySize(ref room.polygons, polygons.Count);
                IOHelper.EnsureArraySize(ref room.portals, portals.Count);

                for (int i = 0; i < polygons.Count; ++i)
                {
                    polygons[i].WriteTo(ref room.polygons[i]);
                }
                for (int i = 0; i < portals.Count; ++i)
                {
                    room.portals[i] = portals[i];
                }
            }
        }

        internal static Tuple<int, int> GetPolygonCellPos(Polygon polygon)
        {
            float x = 0;
            float z = 0;

            int vertCount = polygon.VertexCount;
            for (int i = 0; i < vertCount; i++)
            {
                var v = polygon.vertices[i];
                x += v.position.X;
                z += v.position.Z;
            }
            x /= vertCount;
            z /= vertCount;
            return GetCellPos(x, z);
        }

        internal static Tuple<int, int> GetCellPos(float x, float z)
        {
            return new Tuple<int, int>((int)(x / 100), (int)(z / 100));
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

            pathToTc.Clear();

            LevelCell[] cells = new LevelCell[sceneHeader.sizex * sceneHeader.sizez];
            for (int z = 0, index = 0; z < sceneHeader.sizez; z++)
            {
                for (int x = 0; x < sceneHeader.sizex; x++, index++)
                {
                    //int index = ArxIOHelper.XZToCellIndex(x, z, sizex, sizez);
                    var cell = new LevelCell(x, z);
                    cells[index] = cell;
                }
            }

            IOHelper.EnsureArraySize(ref fts.textureContainers, pathToTc.Count);
            //save texture containers
            foreach (var kv in pathToTc)
            {
                fts.textureContainers[kv.Value].fic = IOHelper.GetBytes(kv.Key, 256);
                fts.textureContainers[kv.Value].tc = kv.Value;
            }

            Dictionary<int, LevelRoom> rooms = new Dictionary<int, LevelRoom>();

            //add polygons to list in cell
            for (int i = 0; i < polygons.Count; ++i)
            {
                var poly = polygons[i];
                var (x, z) = GetPolygonCellPos(poly);
                var cell = cells[IOHelper.XZToCellIndex(x, z, sceneHeader.sizex, sceneHeader.sizez)];
                cell.AddPolygon(poly);

                if (!rooms.TryGetValue(poly.room, out LevelRoom room))
                {
                    room = new LevelRoom();
                    rooms[poly.room] = room;
                }
                room.polygons.Add(new RoomPolygon() { cellX = (short)cell.x, cellZ = (short)cell.z, polyIndex = (short)(cell.polygons.Count - 1) });
            }

            //add anchors indices to list in cell
            IOHelper.EnsureArraySize(ref fts.anchors, anchors.Count);
            for (int i = 0; i < anchors.Count; ++i)
            {
                var anchor = anchors[i];
                var (x, z) = GetCellPos(anchor.pos.X, anchor.pos.Z);
                var cell = cells[IOHelper.XZToCellIndex(x, z, sceneHeader.sizex, sceneHeader.sizez)];
                cell.AddAnchor(i);
                anchor.WriteTo(ref fts.anchors[i]);
            }

            //write cell to fts cell
            IOHelper.EnsureArraySize(ref fts.cells, cells.Length);
            for (int i = 0; i < cells.Length; ++i)
            {
                var c = cells[i];
                c.WriteTo(this, ref fts.cells[i]);
            }

            IOHelper.EnsureArraySize(ref fts.portals, portals.Count);
            for (int i = 0; i < portals.Count; ++i)
            {
                var port = portals[i];
                port.WriteTo(ref fts.portals[i]);
                if (!rooms.TryGetValue(port.room_1, out LevelRoom room))
                {
                    room = new LevelRoom();
                    rooms[port.room_1] = room;
                }
                room.portals.Add(i);
                if (!rooms.TryGetValue(port.room_2, out room))
                {
                    room = new LevelRoom();
                    rooms[port.room_2] = room;
                }
                room.portals.Add(i);
            }

            int maxRoomId = 0;
            foreach (var kv in rooms)
            {
                if (kv.Key > maxRoomId)
                {
                    maxRoomId = kv.Key;
                }
            }
            int roomCount = maxRoomId + 1;
            IOHelper.EnsureArraySize(ref fts.rooms, roomCount);
            for (int i = 0; i < roomCount; ++i)
            {
                if (rooms.TryGetValue(i, out LevelRoom room))
                {
                    room.WriteTo(ref fts.rooms[i]);
                }
            }

            //calculate room distances
            IOHelper.EnsureArraySize(ref fts.roomDistances, roomCount * roomCount);
            for (int i = 0; i < roomCount; ++i)
            {
                for (int j = 0; j < roomCount; ++j)
                {
                    var center_i = fts.rooms[i].CalculateCenter(fts);
                    var center_j = fts.rooms[j].CalculateCenter(fts);
                    var idx = i + j * roomCount;
                    fts.roomDistances[idx].distance = Vector3.Distance(center_i, center_j);
                    fts.roomDistances[idx].startpos = new RawIO.Shared.SavedVec3(center_i);
                    fts.roomDistances[idx].endpos = new RawIO.Shared.SavedVec3(center_j);
                }
            }
        }
        public override string ToString()
        {
            return $"Header:\n{Output.Indent(header.ToString())}\n" +
                $"Sub Headers({subHeaders.Count}):\n{Output.ToString(subHeaders)}\n" +
                $"Scene Header:\n{Output.Indent(sceneHeader.ToString())}\n" +
                $"Polygons({polygons.Count}):\n{Output.ToString(polygons)}\n" +
                $"Anchors({anchors.Count}):\n{Output.ToString(anchors)}\n" +
                $"Portals({portals.Count}):\n{Output.ToString(portals)}";
        }
    }
}
