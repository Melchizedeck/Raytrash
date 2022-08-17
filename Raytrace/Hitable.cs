namespace RayTrace
{
    public abstract class Hitable
    {
        public abstract bool Hit(Ray r, float tMin, float tMax, out HitRecord record);

        public Material Material { get; set; }
    }
}
