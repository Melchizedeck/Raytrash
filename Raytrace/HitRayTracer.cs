using System.Collections.Generic;

namespace RayTrace
{
    public class HitRayTracer : RayTracer
    {
        public override Vector3 color(Ray r, ICollection<Hitable> hitables)
        {
            if (Hit(hitables, r, 0, double.MaxValue, out HitRecord record))
            {
                return new Vector3(1, 0, 0);
            }
            var unitDirection = Vector3.UnitVector(r.Direction);
            var t = 0.5f * (unitDirection[1] + 1);
            return (1.0f - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5f, 0.7f, 1f);
        }
    }
}
