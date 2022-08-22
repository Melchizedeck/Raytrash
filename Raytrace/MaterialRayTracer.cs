using System.Collections.Generic;

namespace RayTrace
{
    public class MaterialRayTracer : RayTracer
    {
        public MaterialRayTracer()
        {
            MaxDepth = 50;
            RayHitMin = 0.0001;
        }
        public int MaxDepth { get; set; }

        public double RayHitMin { get; set; }
        public override Vector3 Trace(Ray r, ICollection<IHitable<Hitable>> hitables)
            => color(r, hitables, RayHitMin, MaxDepth);
        public Vector3 color(Ray r, ICollection<IHitable<Hitable>> hitables, double rayHitMin, int depth)
        {
            if (depth <= 0)
            {
                return new Vector3(0, 0, 0);
            }
            if (Hit(hitables, r, rayHitMin, double.MaxValue, out HitRecord<Hitable> record))
            {
                if (record.Hit.Material.Scatter(r, record, out Vector3 attenuation, out Ray scattered))
                {
                    return attenuation * color(scattered, hitables, rayHitMin, depth - 1);
                }
            }

            var unitDirection = Vector3.UnitVector(r.Direction);
            var t = 0.5 * (unitDirection[1] + 1);
            return (1.0 - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5, 0.7, 1);
        }
    }
}
