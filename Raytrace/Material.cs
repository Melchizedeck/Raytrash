using System.Threading;

namespace RayTrace
{
    public abstract class Material
    {
        public Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.Dot(v, n) * n;
        }

        public Vector3 RandomInUnitSphere()
        {
            var p = new Vector3(0, 0, 0);
            do
            {
                p = 2f * new Vector3(ThreadSafeRandom.NextDouble(), ThreadSafeRandom.NextDouble(), ThreadSafeRandom.NextDouble()) - new Vector3(1, 1, 1);
            } while (Vector3.Dot(p, p) >= 1);
            return p;
        }
        public abstract bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered);
    }
}
