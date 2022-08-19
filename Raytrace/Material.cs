using System.Threading;

namespace RayTrace
{
    public abstract class Material
    {
        public Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.Dot(v, n) * n;
        }

        public Vector3 RandomInUnitVector()
            => Vector3.UnitVector(RandomInUnitSphere());



        private static Vector3 Random(double min, double max)
            => new Vector3(ThreadSafeRandom.NextDouble(min, max), ThreadSafeRandom.NextDouble(min, max), ThreadSafeRandom.NextDouble(min, max));

        public Vector3 RandomInUnitSphere()
        {
            while (true)
            {
                var p = Random(-1, 1);
                if (p.SquaredLength >= 1)
                {
                    continue;
                }
                return p;
            }
        }
        public Vector3 RandomInUnitSphere(Vector3 normal)
        {
            var inUnitSphere = RandomInUnitSphere();
            if (Vector3.Dot(inUnitSphere, normal) > 0)
            {
                return inUnitSphere;
            }
            else
            {
                return -inUnitSphere;
            }
        }
        public abstract bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered);
    }
}
