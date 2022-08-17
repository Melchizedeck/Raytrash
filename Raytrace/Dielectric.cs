using System;

namespace RayTrace
{
    public class Dielectric : Material
    {
        public float RefractionIndex { get; set; }

        float Schlick(float cosine, float ref_idx)
        {
            var r0 = (1 - ref_idx) / (1 + ref_idx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * (float)Math.Pow(1 - cosine, 5);
        }
        public bool Refract(Vector3 v, Vector3 n, float niOverNt, out Vector3 refracted)
        {
            var uv = Vector3.UnitVector(v);
            var dt = Vector3.Dot(uv, n);
            var discriminant = 1 - niOverNt * niOverNt * (1 - dt * dt);

            if (discriminant > 0)
            {
                refracted = niOverNt * (v - n * dt) - n * (float)Math.Sqrt(discriminant);
                return true;
            }
            refracted = new Vector3(0, 0, 0);
            return false;
        }

        public override bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 outwardNormal;
            var reflected = Reflect(r.Direction, record.normal);
            float niOverNt;
            attenuation = new Vector3(1, 1, 1);
            Vector3 refracted;
            float reflectProb;
            float cosine;

            if (Vector3.Dot(r.Direction, record.normal) > 0)
            {
                outwardNormal = -record.normal;
                niOverNt = RefractionIndex;
                cosine = RefractionIndex * Vector3.Dot(r.Direction, record.normal) / r.Direction.Length;
            }
            else
            {
                outwardNormal = record.normal;
                niOverNt = 1 / RefractionIndex;
                cosine = -Vector3.Dot(r.Direction, record.normal) / r.Direction.Length;
            }
            if (Refract(r.Direction, outwardNormal, niOverNt, out refracted))
            {
                reflectProb = Schlick(cosine, RefractionIndex);
            }
            else
            {
                reflectProb = 1;
            }

            if (Random.NextDouble() < reflectProb)
            {
                scattered = new Ray(record.p, reflected);
            }
            else
            {
                scattered = new Ray(record.p, refracted);
            }

            return true;
        }
    }
}
