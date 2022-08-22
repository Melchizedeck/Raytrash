using System.Collections.Generic;

namespace RayTrace
{
    public abstract class RayTracer : IRayTracer<Hitable, Vector3>
    {
        public abstract Vector3 Trace(Ray r, ICollection<IHitable<Hitable>> hitables);

        public bool Hit(ICollection<IHitable<Hitable>> hitables, Ray r, double tMin, double tMax, out HitRecord<Hitable> record)
        {
            var closestSoFar = tMax;
            var hitAnything = false;
            record = new HitRecord<Hitable>();
            foreach (var hitable in hitables)
            {
                if (hitable.Hit(r, tMin, closestSoFar, out HitRecord<Hitable> tempRecord))
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
