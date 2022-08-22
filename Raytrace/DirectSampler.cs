using System.Collections.Generic;

namespace RayTrace
{
    public class DirectSampler : Sampler
    {
        public override Vector3 Sample(int x, int y, int width, int height, ICamera camera, IRayTracer<Hitable, Vector3> rayTracer, ICollection<IHitable<Hitable>> hitables)
        {
            var u = (double)x / (width - 1);
            var v = (double)y / (height - 1);

            var r = camera.GetRay(u, v);
            return rayTracer.Trace(r, hitables);
        }
    }
}
