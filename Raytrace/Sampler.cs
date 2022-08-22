using System.Collections.Generic;

namespace RayTrace
{
    public abstract class Sampler : ISampler<Hitable, Vector3>
    {
        public abstract Vector3 Sample(int x, int y, int width, int height, ICamera camera, IRayTracer<Hitable, Vector3> rayTracer, ICollection<IHitable<Hitable>> hitables);
    }
}
