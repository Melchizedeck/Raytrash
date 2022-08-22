using System;

namespace RayTrace
{
    public class Dielectric : Material
    {
        public double RefractionIndex { get; set; }

        // Use Schlick's approximation for reflectance.
        double Reflectance(double cosine, double ref_idx)
        {
            var r0 = (1D - ref_idx) / (1D + ref_idx);
            r0 = r0 * r0;
            return r0 + (1D - r0) * Math.Pow(1D - cosine, 5D);
        }
        public Vector3 Refract(Vector3 uv, Vector3 n, double etaiOverEtat)
        {
            var cosTheta = Math.Min(Vector3.Dot(-uv, n), 1D);
            var rOutPerp = etaiOverEtat * (uv + cosTheta * n);
            var rOutParallel = -Math.Sqrt(Math.Abs(1D - rOutPerp.SquaredLength)) * n;
            return rOutPerp + rOutParallel;
        }

        public override bool Scatter(Ray r, HitRecord<Hitable> record, out Vector3 attenuation, out Ray scattered)
        {
            attenuation = new Vector3(1, 1, 1);


            var refractionRatio = record.frontFace ? (1D / RefractionIndex) : RefractionIndex;
            var unitDirection = Vector3.UnitVector(r.Direction);

            var cosTheta = Math.Min(Vector3.Dot(-unitDirection, record.normal), 1D);
            var sinTheta = Math.Sqrt(1D - cosTheta * cosTheta);
            var cannotRefract = refractionRatio * sinTheta > 1D;

            Vector3 direction; // 
            if (cannotRefract || Reflectance(cosTheta, refractionRatio) > ThreadSafeRandom.NextDouble())
            {
                direction = Reflect(unitDirection, record.normal);
            }
            else
            {
                direction = Refract(unitDirection, record.normal, refractionRatio);
            }



            scattered = new Ray(record.p, direction);
            return true;
        }
    }
}
