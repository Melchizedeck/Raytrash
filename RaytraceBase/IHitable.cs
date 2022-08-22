namespace RayTrace
{
    public interface IHitable<THit>
    {
        bool Hit(Ray r, double tMin, double tMax, out HitRecord<THit> record);
    }
}
