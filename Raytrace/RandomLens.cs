using System;

namespace RayTrace
{
    public class RandomLens : Lens
    {
        private float _aperture;
        public float Aperture
        {
            get => _aperture;
            set { _aperture = value; }
        }

        public Vector3 RandomInUnitDisk()
        {
            var p = new Vector3(0, 0, 0);
            do
            {
                p = 2f * new Vector3((float)ThreadSafeRandom.NextDouble(), (float)ThreadSafeRandom.NextDouble(), 0) - new Vector3(1, 1, 0);
            } while (Vector3.Dot(p, p) >= 1);
            return p;
        }

        public override Ray GetRay(Camera camera, float u, float v)
        {
            var rd = Aperture / 2 * RandomInUnitDisk();
            var offset = camera.u * rd[0] + camera.v * rd[1];
            return new Ray(camera.LookFrom + offset, camera.LowerLeftCorner + u * camera.Horizontal + v * camera.Vertical - camera.LookFrom - offset);
        }
    }
}
