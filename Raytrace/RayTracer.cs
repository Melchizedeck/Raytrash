using System.Collections.Generic;

namespace RayTrace
{
    public abstract class RayTracer : IRayTracer
    {
        public abstract Vector3 Trace(Ray r, ICollection<IHitable> hitables);

        public bool Hit(ICollection<IHitable> hitables, Ray r, double tMin, double tMax, out HitRecord record)
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
