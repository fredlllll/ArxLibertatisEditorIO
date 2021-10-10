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
            header.ReadFrom(llf.header);

            lightColors.Clear();
            lightingHeader.ReadFrom(llf.lightingHeader);
            for (int i = 0; i < llf.lightingHeader.numLights; ++i)
            {
                lightColors.Add(IOHelper.ColorFromBGRA(llf.lightColors[i]));
            }

            lights.Clear();
            for (int i = 0; i < llf.header.numLights; ++i)
            {
                var light = new Light();
                light.ReadFrom(llf.lights[i]);
                lights.Add(light);
            }
        }

        public void WriteTo(LLF_IO llf)
        {
            header.WriteTo(ref llf.header);


            lightingHeader.WriteTo(ref llf.lightingHeader);
            IOHelper.EnsureArraySize(ref llf.lightColors, lightColors.Count);
            llf.lightingHeader.numLights = lightColors.Count;
            for (int i = 0; i < lightColors.Count; ++i)
            {
                llf.lightColors[i] = IOHelper.ColorToBGRA(lightColors[i]);
            }


            IOHelper.EnsureArraySize(ref llf.lights, lights.Count);
            llf.header.numLights = lights.Count;
            for (int i = 0; i < lights.Count; ++i)
            {
                lights[i].WriteTo(ref llf.lights[i]);
            }
        }
    }
}
