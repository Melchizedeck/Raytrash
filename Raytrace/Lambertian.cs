using System;

namespace RayTrace
{
    public class Lambertian : Material
    {
        public Vector3 Albedo { get; set; }


        public override bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered)
        {
            var scatterDirection = record.normal + RandomInUnitVector();
            if (scatterDirection.IsNearZero)
            {
                scatterDirection = record.normal;
            }
            scattered = new Ray(record.p, scatterDirection);
            attenuation = Albedo;
            return true;
        }
    }
}
