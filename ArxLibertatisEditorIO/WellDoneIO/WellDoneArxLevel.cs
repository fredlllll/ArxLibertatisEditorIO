using ArxLibertatisEditorIO.MediumIO;
using ArxLibertatisEditorIO.Util;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ArxLibertatisEditorIO.WellDoneIO
{
    public class WellDoneArxLevel
    {
        public readonly List<Polygon> polygons = new List<Polygon>();

        internal readonly Dictionary<int, string> tcToTex = new Dictionary<int, string>();
        internal readonly Dictionary<string, int> texToTc = new Dictionary<string, int>();

        public WellDoneArxLevel LoadFrom(MediumArxLevel mal)
        {
            //make texture dict
            tcToTex.Clear();
            for (int i = 0; i < mal.FTS.textureContainers.Count; ++i)
            {
                var tc = mal.FTS.textureContainers[i];
                tcToTex[tc.containerId] = tc.texturePath;
            }

            int vertIndex = 0;
            polygons.Clear();
            for (int i = 0; i < mal.FTS.cells.Count; ++i)
            {
                var c = mal.FTS.cells[i];
                for (int j = 0; j < c.polygons.Count; j++)
                {
                    var p = new Polygon();
                    p.ReadFrom(this, c.polygons[j]);
                    for (int k = 0; k < p.VertexCount; ++k)
                    {
                        //load color from llf
                        p.vertices[k].color = mal.LLF.lightColors[vertIndex++];
                    }
                    polygons.Add(p);
                }
            }

            tcToTex.Clear();
            return this;
        }

        public MediumArxLevel SaveTo(MediumArxLevel mal)
        {
            texToTc.Clear();

            //create cells
            Saving.Cell[] cells = new Saving.Cell[mal.FTS.sceneHeader.sizex * mal.FTS.sceneHeader.sizez];
            int sizex = mal.FTS.sceneHeader.sizex;
            int sizez = mal.FTS.sceneHeader.sizez;
            for (int z = 0, index = 0; z < sizez; z++)
            {
                for (int x = 0; x < sizex; x++, index++)
                {
                    var cell = new Saving.Cell(x, z);
                    cells[index] = cell;
                }
            }
            mal.FTS.cells.Clear();
            mal.FTS.cells.AddRange(cells);

            short maxRoomId = 0; //seems to be 1 based for some reason

            //write polygons to cells
            for (int i = 0; i < polygons.Count; ++i)
            {
                var p = polygons[i];
                var (cellx, cellz) = GetPolygonCellPos(p);
                var cell = cells[IOHelper.XZToCellIndex(cellx, cellz, sizex, sizez)];
                var poly = new MediumIO.FTS.Polygon();
                p.WriteTo(this, poly);
                cell.polygons.Add(poly);

                //append colors
                for (int k = 0; k < p.VertexCount; ++k)
                {
                    cell.colors.Add(p.vertices[k].color);
                }

                maxRoomId = Math.Max(maxRoomId, p.room);
            }

            //write anchors to cells
            for (int i = 0; i < mal.FTS.anchors.Count; ++i)
            {
                var a = mal.FTS.anchors[i];
                var (cellx, cellz) = GetCellPos(a.pos.X, a.pos.Z);
                var cell = cells[IOHelper.XZToCellIndex(cellx, cellz, sizex, sizez)];
                cell.anchors.Add(i);
            }

            //create rooms
            int numRooms = maxRoomId + 1;
            mal.FTS.rooms.Clear();
            for (int i = 0; i < numRooms; ++i)
            {
                var r = new MediumIO.FTS.Room();
                mal.FTS.rooms.Add(r);
            }

            //add portals to rooms
            for (int i = 0; i < mal.FTS.portals.Count; ++i)
            {
                var p = mal.FTS.portals[i];
                var room = mal.FTS.rooms[p.room_1];
                room.portals.Add(i);
                room = mal.FTS.rooms[p.room_2];
                room.portals.Add(i);
            }

            //add polygons to rooms

            for (int i = 0; i < mal.FTS.cells.Count; ++i)
            {
                var (cellx, cellz) = IOHelper.CellIndexToXZ(i, sizex);
                var c = mal.FTS.cells[i];
                for (int j = 0; j < c.polygons.Count; ++j)
                {
                    var p = c.polygons[j];
                    if (p.room >= 0)
                    {
                        var rp = new MediumIO.FTS.RoomPolygon
                        {
                            cell_x = (short)cellx,
                            cell_z = (short)cellz,
                            idx = (short)j
                        };
                        var room = mal.FTS.rooms[p.room];
                        room.polygons.Add(rp);
                    }
                }
            }

            //calculate room distances
            mal.FTS.roomDistances.Clear();
            for (int j = 0; j < mal.FTS.rooms.Count; ++j)
            {
                for (int i = 0; i < mal.FTS.rooms.Count; ++i)
                {
                    var room1 = mal.FTS.rooms[i];
                    var room2 = mal.FTS.rooms[j];

                    var center1 = room1.CalculateCenter(mal.FTS);
                    var center2 = room2.CalculateCenter(mal.FTS);

                    var rd = new MediumIO.FTS.RoomDistance
                    {
                        distance = Vector3.Distance(center1, center2),
                        startpos = center1,
                        endpos = center2
                    };
                    mal.FTS.roomDistances.Add(rd);
                }
            }

            //write light colors to llf
            mal.LLF.lightColors.Clear();
            for (int i = 0; i < cells.Length; ++i)
            {
                mal.LLF.lightColors.AddRange(cells[i].colors);
            }

            //write texture containers

            mal.FTS.textureContainers.Clear();
            for (int i = 0; i < texToTc.Count; ++i)
            {
                mal.FTS.textureContainers.Add(new MediumIO.FTS.TextureContainer());
            }
            foreach (var kv in texToTc)
            {
                var tc = mal.FTS.textureContainers[kv.Value - 1]; //we start at 1 because 0 means no texture
                tc.containerId = kv.Value;
                tc.texturePath = kv.Key;
            }

            texToTc.Clear();
            return mal;
        }

        private static Tuple<int, int> GetPolygonCellPos(Polygon polygon)
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

        private static Tuple<int, int> GetCellPos(float x, float z)
        {
            return new Tuple<int, int>((int)(x / 100), (int)(z / 100));
        }
    }
}
