using System.Collections.Generic;

namespace RayTrace
{
    public interface ISampler<THit,TOutput>
    {
        Vector3 Sample(int x, int y, int width, int height, ICamera camera, IRayTracer<THit,TOutput> rayTracer, ICollection<IHitable<THit>> hitables);
    }
}
