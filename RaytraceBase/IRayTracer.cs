using System.Collections.Generic;

namespace RayTrace
{
    public interface IRayTracer
    {
        Vector3 Trace(Ray r, ICollection<IHitable> hitables);
    }
}
