using System;

namespace RayTrace
{
    public class Lambertian : Material
    {
        public Lambertian()
            : this(new Random())
        {

        }
        public Lambertian(Random random):base(random)
        {
        }
        public Vector3 Albedo { get; set; }
        
       
        public override bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered)
        {
            var target = record.p + record.normal + RandomInUnitSphere();
            scattered = new Ray(record.p, target - record.p);
            attenuation = Albedo;
            return true;
        }
    }
}
