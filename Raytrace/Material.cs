using System;

namespace RayTrace
{
    public abstract class Material
    {
        private readonly Random _random;
        public Material()
            : this(new Random())
        {

        }
        public Material(Random random)
        {
            _random = random;
        }
        public Vector3 RandomInUnitSphere()
        {
            var p = new Vector3(0, 0, 0);
            do
            {
                p = 2f * new Vector3((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble()) - new Vector3(1, 1, 1);
            } while (Vector3.Dot(p, p) >= 1);
            return p;
        }
        public abstract bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered);
    }
}
