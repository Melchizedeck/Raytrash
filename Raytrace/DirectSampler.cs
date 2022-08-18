using System.Collections.Generic;

namespace RayTrace
{
    public class DirectSampler : Sampler
    {
        public override Vector3 color(int x, int y, int width, int height, Camera camera, RayTracer rayTracer, ICollection<Hitable> hitables)
        {
            var u = (double)x / (width - 1);
            var v = (double)y / (height - 1);

            var r = camera.GetRay(u, v);
            return rayTracer.color(r, hitables);
        }
    }
}
