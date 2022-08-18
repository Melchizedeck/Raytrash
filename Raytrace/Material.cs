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
            while (true)
            {
                var p = Vector3.Random(-1, 1);
                if(p.SquaredLength>=1)
                {
                    continue;
                }
                return p;
            }
        }
        public abstract bool Scatter(Ray r, HitRecord record, out Vector3 attenuation, out Ray scattered);
    }
}
