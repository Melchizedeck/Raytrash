using System.Collections.Generic;

namespace RayTrace
{
    public class DirectSampler : Sampler
    {
        public override Vector3 Sample(int x, int y, int width, int height, ICamera camera, IRayTracer rayTracer, ICollection<IHitable> hitables)
        {
            var u = (double)x / (width - 1);
            var v = (double)y / (height - 1);

            var r = camera.GetRay(u, v);
            return rayTracer.Trace(r, hitables);
        }
    }
}
