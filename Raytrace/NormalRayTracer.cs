using System;

namespace RayTrace
{
    public class NormalRayTracer : RayTracer
    {
        float hitSphere(Vector3 center, float radius, Ray r)
        {
            var oc = r.Origin - center;
            var a = Vector3.Dot(r.Direction, r.Direction);
            var b = 2f * Vector3.Dot(oc, r.Direction);
            var c = Vector3.Dot(oc, oc) - radius * radius;
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                return -1f;
            }
            else
            {
                return (-b - (float)Math.Sqrt(discriminant)) / (2f * a);
            }
        }
        public override Vector3 color(Ray r)
        {
            var t = hitSphere(new Vector3(0, 0, -1), 0.5f, r);
            if (t > 0)
            {
                var n = Vector3.UnitVector(r.PointAt(t) - new Vector3(0, 0, -1));
                return 0.5f * new Vector3(n[0] + 1, n[1] + 1, n[2] + 1);
            }
            var unitDirection = Vector3.UnitVector(r.Direction);
            t = 0.5f * (unitDirection[1] + 1);
            return (1.0f - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5f, 0.7f, 1f);
        }
    }
}
