namespace RayTrace
{
    public abstract class Hitable : IHitable<Hitable>
    {
        public abstract bool Hit(Ray r, double tMin, double tMax, out HitRecord<Hitable> record);

        public Material Material { get; set; }
    }
}
