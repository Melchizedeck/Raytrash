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
            if (discriminant > 0)
            {
                var temp = (-b - Math.Sqrt(b * b - a * c)) / a;
                if (tMin < temp && temp < tMax)
                {
                    var p = r.PointAt(temp);
                    record.t = temp;
                    record.p = p;
                    record.normal = (p - Center) / Radius;
                    record.Material = Material;
                    return true;
                }

                temp = (-b + Math.Sqrt(b * b - a * c)) / a;
                if (tMin < temp && temp < tMax)
                {
                    var p = r.PointAt(temp);
                    record.t = temp;
                    record.p = p;
                    record.normal = (p - Center) / Radius;
                    record.Material = Material;
                    return true;
                }
            }
            return false;
        }
    }
}
