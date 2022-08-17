namespace RayTrace
{
    public class Metal : Material
    {
        public Vector3 Albedo { get; set; }

        public Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.Dot(v, n) * n;
        }

        public override bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered)
        {
            var reflected = Reflect(Vector3.UnitVector(r.Direction), record.normal);
            scattered = new Ray(record.p, reflected);
            attenuation = Albedo;
            return Vector3.Dot(scattered.Direction, record.normal) > 0;
        }
    }
}
