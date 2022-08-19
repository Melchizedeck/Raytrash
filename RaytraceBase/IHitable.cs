namespace RayTrace
{
    public interface IHitable
    {
        bool Hit(Ray r, double tMin, double tMax, out HitRecord record);
    }
}
