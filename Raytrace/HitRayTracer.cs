namespace RayTrace
{
    public class HitRayTracer : RayTracer
    {
        bool hitSphere(Vector3 center, float radius, Ray r)
        {
            var oc = r.Origin - center;
            var a = Vector3.Dot(r.Direction, r.Direction);
            var b = 2f * Vector3.Dot(oc, r.Direction);
            var c = Vector3.Dot(oc, oc) - radius * radius;
            var discriminant = b * b - 4 * a * c;
            return discriminant > 0;
        }
        public override Vector3 color(Ray r)
        {
            if (hitSphere(new Vector3(0, 0, -1), 0.5f, r))
            {
                return new Vector3(1, 0, 0);
            }
            var unitDirection = Vector3.UnitVector(r.Direction);
            var t = 0.5f * (unitDirection[1] + 1);
            return (1.0f - t) * new Vector3(1, 1, 1) + t * new Vector3(0.5f, 0.7f, 1f);
        }
    }
}
