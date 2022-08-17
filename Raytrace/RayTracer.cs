using System.Collections.Generic;

namespace RayTrace
{
    public abstract class RayTracer
    {
        public abstract Vector3 color(Ray r, ICollection<Hitable> hitables);

        public bool Hit(ICollection<Hitable> hitables, Ray r, float tMin, float tMax, out HitRecord record)
        {
            var closestSoFar = tMax;
            var hitAnything = false;
            record = new HitRecord();
            foreach (var hitable in hitables)
            {
                if (hitable.Hit(r, tMin, closestSoFar, out HitRecord tempRecord))
                {
                    hitAnything = true;
                    closestSoFar = tempRecord.t;
                    record = tempRecord;
                }
            }

            return hitAnything;
        }
    }
}
