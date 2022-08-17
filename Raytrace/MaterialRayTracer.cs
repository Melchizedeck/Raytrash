using System;
using System.Collections.Generic;

namespace RayTrace
{
    public class MaterialRayTracer : RayTracer
    {
        private readonly Random _random;
        public MaterialRayTracer()
            : this(new Random())
        {

        }
        public MaterialRayTracer(Random random)
        {
            _random = random;
        }
        Vector3 RandomInUnitSphere()
        {
            var p = new Vector3(0, 0, 0);
            do
            {
                p = 2f * new Vector3((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble()) - new Vector3(1, 1, 1);
            } while (Vector3.Dot(p, p) >= 1);
            return p;
        }
        public override Vector3 color(Ray r, ICollection<Hitable> hitables)
        {
            if (Hit(hitables, r, 0, float.MaxValue, out HitRecord record))
            {
                var target = record.p + record.normal + RandomInUnitSphere();
                return 0.5f * color(new Ray(record.p, target - record.p), hitables);
            }

            var unitDirection = Vector3.UnitVector(r.Direction);
            var t = 0.5f * (unitDirection[1] + 1);
            return (1.0f - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5f, 0.7f, 1f);
        }
    }
}
