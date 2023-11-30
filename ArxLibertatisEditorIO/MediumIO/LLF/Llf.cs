using ArxLibertatisEditorIO.MediumIO.Shared;
using ArxLibertatisEditorIO.RawIO.LLF;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.MediumIO.LLF
{
    public class Llf
    {
        public readonly Header header = new Header();
        public readonly LightingHeader lightingHeader = new LightingHeader();
        public readonly List<Color> lightColors = new List<Color>();
        public readonly List<Light> lights = new List<Light>();

        public void LoadFrom(LLF_IO llf)
        {
            header.LoadFrom(llf.header);

            lightColors.Clear();
            lightingHeader.ReadFrom(ref llf.lightingHeader);
            for (int i = 0; i < llf.lightingHeader.numLights; ++i)
            {
                lightColors.Add(IOHelper.ColorFromBGRA(llf.lightColors[i]));
            }

            lights.Clear();
            for (int i = 0; i < llf.header.numLights; ++i)
            {
                var light = new Light();
                light.ReadFrom(ref llf.lights[i]);
                lights.Add(light);
            }
        }

        public void SaveTo(LLF_IO llf)
        {
            header.SaveTo(ref llf.header);

            lightingHeader.SaveTo(ref llf.lightingHeader);
            IOHelper.EnsureArraySize(ref llf.lightColors, lightColors.Count);
            for (int i = 0; i < lightColors.Count; ++i)
            {
                llf.lightColors[i] = IOHelper.ColorToBGRA(lightColors[i]);
            }

            IOHelper.EnsureArraySize(ref llf.lights, lights.Count);
            for (int i = 0; i < lights.Count; ++i)
            {
                lights[i].WriteTo(ref llf.lights[i]);
            }
        }

        public override string ToString()
        {
            return $"Header:\n{Output.Indent(header.ToString())}\n" +
                $"LightingHeader:\n{Output.Indent(lightingHeader.ToString())}\n" +
                $"Lights({lights.Count}):\n{Output.ToString(lights)}";
        }
    }
}
