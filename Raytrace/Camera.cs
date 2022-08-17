using System;

namespace RayTrace
{
    public class Camera
    {
        Vector3 _lowerLeftCorner;
        Vector3 _horizontal;
        Vector3 _vertical;
        Vector3 _origin;

        private Vector3 _lookFrom;
        public Vector3 LookFrom
        {
            get => _lookFrom;
            set
            {
                _lookFrom = value;
                Refresh();
            }
        }

        private Vector3 _lookAt;
        public Vector3 LookAt
        {
            get => _lookAt;
            set
            {
                _lookAt = value;
                Refresh();
            }
        }

        private Vector3 _vup;
        public Vector3 VUP
        {
            get => _vup;
            set
            {
                _vup = value;
                Refresh();
            }
        }

        private float _fov;
        public float FOV
        {
            get => _fov;
            set
            {
                _fov = value;
                Refresh();
            }
        }

        private float _aspect;
        public float Aspect
        {
            get => _aspect;
            set
            {
                _aspect = value;
                Refresh();
            }
        }

        private void Refresh()
        {
            if (LookFrom.IsEmpty || LookAt.IsEmpty || VUP.IsEmpty)
            {
                return;
            }
            var theta = FOV * Math.PI / 180;
            var halfHeight = (float)Math.Tan(theta / 2);
            var halfWidth = Aspect * halfHeight;

            _origin = LookFrom;

            var w = Vector3.UnitVector(LookFrom - LookAt);
            var u = Vector3.UnitVector(Vector3.Cross(VUP, w));
            var v = Vector3.Cross(w, u);

            _lowerLeftCorner = _origin - halfWidth * u - halfHeight * v - w;
            _horizontal = 2 * halfWidth * u;
            _vertical = 2 * halfHeight * v;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(_origin, _lowerLeftCorner + u * _horizontal + v * _vertical - _origin);
        }
    }
}
