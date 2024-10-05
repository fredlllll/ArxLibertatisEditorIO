using ArxLibertatisEditorIO.Util;
using System.Numerics;

namespace ArxLibertatisEditorIO.WellDoneIO
{
    public class Light
    {
        public Vector3 position;
        public Color color;
        public float fallStart;
        public float fallEnd;
        public float intensity;
        public Color ex_flicker;
        public float ex_radius;
        public float ex_frequency;
        public float ex_size;
        public float ex_speed;
        public float ex_flaresize;
        public ExtrasType extras;

        public void LoadFrom(MediumIO.Shared.Light light)
        {
            position = light.pos;
            color = light.color;
            fallStart = light.fallStart;
            fallEnd = light.fallEnd;
            intensity = light.intensity;
            ex_flicker = light.ex_flicker;
            ex_radius = light.ex_radius;
            ex_frequency = light.ex_frequency;
            ex_size = light.ex_size;
            ex_speed = light.ex_speed;
            ex_flaresize = light.ex_flaresize;
            extras = light.extras;
        }

        public void SaveTo(MediumIO.Shared.Light light)
        {
            light.pos = position;
            light.color = color;
            light.fallStart = fallStart;
            light.fallEnd = fallEnd;
            light.intensity = intensity;
            light.ex_flicker = ex_flicker;
            light.ex_radius = ex_radius;
            light.ex_frequency = ex_frequency;
            light.ex_size = ex_size;
            light.ex_speed = ex_speed;
            light.ex_flaresize = ex_flaresize;
            light.extras = extras;
        }
    }
}
