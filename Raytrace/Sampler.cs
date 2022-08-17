using System.Collections.Generic;

namespace RayTrace
{
    public abstract class Sampler
    {
        public abstract Vector3 color(int x, int y, int width, int height, Camera camera, RayTracer rayTracer, ICollection<Hitable> hitables);
    }
}
