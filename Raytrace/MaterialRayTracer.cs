using System.Collections.Generic;

namespace RayTrace
{
    public class MaterialRayTracer : RayTracer
    {
        public MaterialRayTracer()
        {
            MaxDepth = 50;
        }
        public int MaxDepth { get; set; }
        public override Vector3 color(Ray r, ICollection<Hitable> hitables)
            => color(r, hitables, 0);
        public Vector3 color(Ray r, ICollection<Hitable> hitables, int depth)
        {
            if (Hit(hitables, r, 0, double.MaxValue, out HitRecord record))
            {
                if (depth < MaxDepth && record.Material.Scatter(r, record, out Vector3 attenuation, out Ray scattered))
                {
                    return attenuation * color(scattered, hitables, depth + 1);
                }
                return new Vector3(0, 0, 0);
            }

            var unitDirection = Vector3.UnitVector(r.Direction);
            var t = 0.5f * (unitDirection[1] + 1);
            return (1.0f - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5f, 0.7f, 1f);
        }
    }
}
