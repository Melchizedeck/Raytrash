using System.Collections.Generic;

namespace RayTrace
{
    public interface IRayTracer<THit>
    {
        Vector3 Trace(Ray r, ICollection<IHitable<THit>> hitables);
    }
}
