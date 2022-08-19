using System;

namespace RayTrace
{
    public class RandomLens : CameraLens
    {
        private Vector3 _lowerLeftCorner;
        private Vector3 _horizontal;
        private Vector3 _vertical;

        private Camera _camera;

        private double _aperture;
        public double Aperture
        {
            get => _aperture;
            set { _aperture = value; }
        }

        public double Focus { get; set; }

        private FocusMode _focusMode;
        public FocusMode FocusMode
        {
            get => _focusMode;
            set
            {
                _focusMode = value;
                Refresh();
            }
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

        public override void OnUpdate(Camera camera)
        {
            _camera = camera;
            Refresh();
        }

        private void Refresh()
        {
            if (_camera == null)
            {
                return;
            }
            if (FocusMode == FocusMode.Auto)
            {
                Focus = (_camera.LookFrom - _camera.LookAt).Length;
            }

            _lowerLeftCorner = _camera.LookFrom - _camera.halfWidth * _camera.u * Focus - _camera.halfHeight * _camera.v * Focus - _camera.w * Focus;
            _horizontal = 2 * _camera.halfWidth * Focus * _camera.u;
            _vertical = 2 * _camera.halfHeight * Focus * _camera.v;
        }

        public override Ray GetRay(double u, double v)
        {
            var rd = Aperture / 2 * RandomInUnitDisk();
            var offset = _camera.u * rd[0] + _camera.v * rd[1];
            return new Ray(_camera.LookFrom + offset, _lowerLeftCorner + u * _horizontal + v * _vertical - _camera.LookFrom - offset);
        }
    }

    public enum FocusMode
    {
        Manual,
        Auto
    }
}
