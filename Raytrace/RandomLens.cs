using System;

namespace RayTrace
{
    public class RandomLens : Lens
    {
        private double _aperture;
        public double Aperture
        {
            get => _aperture;
            set { _aperture = value; }
        }

        public Vector3 RandomInUnitDisk()
        {
            var p = new Vector3(0, 0, 0);
            do
            {
                p = 2 * new Vector3(ThreadSafeRandom.NextDouble(), ThreadSafeRandom.NextDouble(), 0) - new Vector3(1, 1, 0);
            } while (Vector3.Dot(p, p) >= 1);
            return p;
        }

        public override Ray GetRay(Camera camera, double u, double v)
        {
            var rd = Aperture / 2 * RandomInUnitDisk();
            var offset = camera.u * rd[0] + camera.v * rd[1];
            return new Ray(camera.LookFrom + offset, camera.LowerLeftCorner + u * camera.Horizontal + v * camera.Vertical - camera.LookFrom - offset);
        }
    }
}
