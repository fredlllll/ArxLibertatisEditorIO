using ArxLibertatisEditorIO.MediumIO.Shared;
using ArxLibertatisEditorIO.RawIO.DLF;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.MediumIO.DLF
{
    public class Dlf
    {
        public readonly Header header = new Header();
        public readonly List<Scene> scenes = new List<Scene>();
        public readonly List<Inter> inters = new List<Inter>();
        public readonly List<Fog> fogs = new List<Fog>();
        public readonly List<Path> paths = new List<Path>();
        public readonly LightingHeader lightingHeader = new LightingHeader();
        public readonly List<Color> lightColors = new List<Color>();
        public readonly List<Light> lights = new List<Light>();

        private byte[] nodesData = new byte[0];

        public void LoadFrom(DLF_IO dlf)
        {
            header.ReadFrom(dlf.header);

            scenes.Clear();
            for (int i = 0; i < dlf.header.numScenes; i++)
            {
                var scene = new Scene();
                scene.ReadFrom(dlf.scenes[i]);
                scenes.Add(scene);
            }

            inters.Clear();
            for (int i = 0; i < dlf.header.numInters; i++)
            {
                var inter = new Inter();
                inter.ReadFrom(dlf.inters[i]);
                inters.Add(inter);
            }

            fogs.Clear();
            for (int i = 0; i < dlf.header.numFogs; ++i)
            {
                var fog = new Fog();
                fog.ReadFrom(dlf.fogs[i]);
                fogs.Add(fog);
            }

            paths.Clear();
            for (int i = 0; i < dlf.header.numPaths; ++i)
            {
                var path = new Path();
                path.ReadFrom(dlf.paths[i]);
                paths.Add(path);
            }

            lightColors.Clear();
            if (header.lighting)
            {
                lightingHeader.ReadFrom(dlf.lightingHeader);
                for (int i = 0; i < dlf.lightingHeader.numLights; ++i)
                {
                    lightColors.Add(IOHelper.ColorFromBGRA(dlf.lightColors[i]));
                }
            }

            lights.Clear();
            for (int i = 0; i < dlf.header.numLights; ++i)
            {
                var light = new Light();
                light.ReadFrom(dlf.lights[i]);
                lights.Add(light);
            }

            nodesData = dlf.nodesData;
        }

        public void WriteTo(DLF_IO dlf)
        {
            header.WriteTo(ref dlf.header);

            IOHelper.EnsureArraySize(ref dlf.scenes, scenes.Count);
            dlf.header.numScenes = scenes.Count;
            for (int i = 0; i < scenes.Count; ++i)
            {
                scenes[i].WriteTo(ref dlf.scenes[i]);
            }

            IOHelper.EnsureArraySize(ref dlf.inters, inters.Count);
            dlf.header.numInters = inters.Count;
            for (int i = 0; i < inters.Count; ++i)
            {
                inters[i].WriteTo(ref dlf.inters[i]);
            }

            IOHelper.EnsureArraySize(ref dlf.fogs, fogs.Count);
            dlf.header.numFogs = fogs.Count;
            for (int i = 0; i < fogs.Count; ++i)
            {
                fogs[i].WriteTo(ref dlf.fogs[i]);
            }

            IOHelper.EnsureArraySize(ref dlf.paths, paths.Count);
            dlf.header.numPaths = paths.Count;
            for (int i = 0; i < paths.Count; ++i)
            {
                paths[i].WriteTo(ref dlf.paths[i]);
            }


            if (header.lighting)
            {
                lightingHeader.WriteTo(ref dlf.lightingHeader);
                IOHelper.EnsureArraySize(ref dlf.lightColors, lightColors.Count);
                dlf.lightingHeader.numLights = lightColors.Count;
                for (int i = 0; i < lightColors.Count; ++i)
                {
                    dlf.lightColors[i] = IOHelper.ColorToBGRA(lightColors[i]);
                }
            }

            IOHelper.EnsureArraySize(ref dlf.lights, lights.Count);
            dlf.header.numLights = lights.Count;
            for (int i = 0; i < lights.Count; ++i)
            {
                lights[i].WriteTo(ref dlf.lights[i]);
            }

            dlf.nodesData = nodesData;
        }

        public override string ToString()
        {
            return $"Header:\n{Output.Indent(header.ToString())}\n" +
                $"Scenes({scenes.Count}):\n{Output.ToString(scenes)}\n" +
                $"Inters({inters.Count}):\n{Output.ToString(inters)}\n" +
                $"Fogs({fogs.Count}):\n{Output.ToString(fogs)}\n" +
                $"Paths({paths.Count}):\n{Output.ToString(paths)}\n" +
                (header.lighting ? $"LightingHeader:\n{lightingHeader}\n" +
                $"Light Colors({lightColors.Count}):\n{Output.ToString(lightColors)}\n" : "") +
                $"Lights({lights.Count}):\n{Output.ToString(lights)}";
        }
    }
}
