﻿using ArxLibertatisEditorIO.Util;
using System.Runtime.InteropServices;

namespace ArxLibertatisEditorIO.RawIO.FTS
{
    public class FTS_IO_CELL
    {
        public FTS_IO_SCENE_INFO sceneInfo;
        public FTS_IO_EERIEPOLY[] polygons;
        public int[] anchors;

        public void ReadFrom(StructReader reader)
        {
            sceneInfo = reader.ReadStruct<FTS_IO_SCENE_INFO>();

            polygons = new FTS_IO_EERIEPOLY[sceneInfo.nbpoly];
            for (int i = 0; i < sceneInfo.nbpoly; i++)
            {
                polygons[i] = reader.ReadStruct<FTS_IO_EERIEPOLY>();
            }

            anchors = new int[sceneInfo.nbianchors];
            for (int i = 0; i < sceneInfo.nbianchors; i++)
            {
                anchors[i] = reader.ReadInt32();
            }
        }

        public void WriteTo(StructWriter writer)
        {
            sceneInfo.nbpoly = polygons.Length;
            sceneInfo.nbianchors = anchors.Length;
            writer.WriteStruct(sceneInfo);

            for (int i = 0; i < polygons.Length; i++)
            {
                writer.WriteStruct(polygons[i]);
            }

            for (int i = 0; i < anchors.Length; i++)
            {
                writer.Write(anchors[i]);
            }
        }

        public int CalculateWrittenSize()
        {
            int size = 0;

            size += Marshal.SizeOf<FTS_IO_SCENE_INFO>();
            size += Marshal.SizeOf<FTS_IO_EERIEPOLY>() * polygons.Length;
            size += sizeof(int) * anchors.Length;

            return size;
        }

        public override string ToString()
        {
            return $"sceneInfo: {Output.Indent(sceneInfo.ToString())}\n" +
            $"polygons: {Output.ToString(polygons)}\n" +
            $"anchors: {Output.ToString(anchors)}";
        }
    }
}
