using System;

namespace RayTrace
{
    public class Lambertian : Material
    {
        private readonly Random _random;
        public Lambertian()
            : this(new Random())
        {

        }
        public Vector3 Albedo { get; set; }
        public Lambertian(Random random)
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
        public override bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered)
        {
            var target = record.p + record.normal + RandomInUnitSphere();
            scattered = new Ray(record.p, target - record.p);
            attenuation = Albedo;
            return true;
        }
    }
}
