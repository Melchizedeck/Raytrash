using System.Collections.Generic;

namespace RayTrace
{
    public interface ISampler<THit>
    {
        Vector3 Sample(int x, int y, int width, int height, ICamera camera, IRayTracer<THit> rayTracer, ICollection<IHitable<THit>> hitables);
    }
}
