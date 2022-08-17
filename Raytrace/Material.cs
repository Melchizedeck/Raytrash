namespace RayTrace
{
    public abstract class Material
    {
        public abstract bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered);
    }
}
