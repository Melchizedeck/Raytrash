using System.Collections.Generic;

namespace RayTrace
{
    public class NormalRayTracer : RayTracer
    {
        public override Vector3 Trace(Ray r, ICollection<IHitable<Hitable>> hitables)
        {
            if (Hit(hitables, r, 0, double.MaxValue, out HitRecord<Hitable> record))
            {
                return 0.5 * (record.normal + new Vector3(1, 1, 1));
            }

            var unitDirection = Vector3.UnitVector(r.Direction);
            var t = 0.5 * (unitDirection[1] + 1);
            return (1.0 - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5, 0.7, 1);
        }
    }
}
