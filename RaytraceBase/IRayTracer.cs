using System.Collections.Generic;

namespace RayTrace
{
    public interface IRayTracer
    {
        Vector3 color(Ray r, ICollection<IHitable> hitables);
    }
}
