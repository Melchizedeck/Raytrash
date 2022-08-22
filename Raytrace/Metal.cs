namespace RayTrace
{
    public class Metal : Material
    {
        public Vector3 Albedo { get; set; }
        public double Fuzz { get; set; }


        public override bool Scatter(Ray r, HitRecord<Hitable> record, out Vector3 attenuation, out Ray scattered)
        {
            var reflected = Reflect(Vector3.UnitVector(r.Direction), record.normal);
            scattered = new Ray(record.p, reflected + Fuzz * RandomInUnitSphere());
            attenuation = Albedo;
            return Vector3.Dot(scattered.Direction, record.normal) > 0;
        }
    }
}
