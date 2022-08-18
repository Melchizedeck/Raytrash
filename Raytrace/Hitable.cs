namespace RayTrace
{
    public abstract class Hitable
    {
        public abstract bool Hit(Ray r, double tMin, double tMax, out HitRecord record);

        public Material Material { get; set; }
    }
}
