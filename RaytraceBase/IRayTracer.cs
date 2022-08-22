using System.Collections.Generic;

namespace RayTrace
{
    public interface IRayTracer<THit,TOutput>
    {
        TOutput Trace(Ray r, ICollection<IHitable<THit>> hitables);
    }
}
