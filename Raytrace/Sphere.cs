using System;

namespace RayTrace
{
    public class Sphere : Hitable
    {
        public Vector3 Center { get; set; }
        public double Radius { get; set; }
        public override bool Hit(Ray r, double tMin, double tMax, out HitRecord record)
        {
            var oc = r.Origin - Center;
            var a = Vector3.Dot(r.Direction, r.Direction);
            var b = Vector3.Dot(oc, r.Direction);
            var c = Vector3.Dot(oc, oc) - Radius * Radius;
            record = new HitRecord();
            var discriminant = b * b - a * c;
            if (discriminant < 0)
            {
                return false;
            }

            var sqrd = Math.Sqrt(discriminant);


            var temp = (-b - sqrd) / a;
            if (temp < tMin || tMax < temp)
            {
                temp = (-b + sqrd) / a;
                if (temp < tMin || tMax < temp)
                {
                    return false;
                }
            }

            var p = r.PointAt(temp);
            record.t = temp;
            record.p = p;

            var outwardNormal = (p - Center) / Radius;
            record.SetFaceNormal(r, outwardNormal);
            record.Material = Material;
            return true;
        }
    }
}
