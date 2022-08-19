using System;

namespace RayTrace
{
    public class Camera : ICamera
    {
        public Vector3 w { get; private set; }
        public Vector3 u { get; private set; }
        public Vector3 v { get; private set; }

        public double halfWidth { get; private set; }
        public double halfHeight { get; private set; }

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

        private double _fov;
        public double FOV
        {
            get => _fov;
            set
            {
                _fov = value;
                Refresh();
            }
        }

        private double _aspect;
        public double Aspect
        {
            get => _aspect;
            set
            {
                _aspect = value;
                Refresh();
            }
        }

        bool _refreshed;
        private CameraLens _cameraLens;
        public CameraLens CameraLens
        {
            get => _cameraLens;
            set
            {
                _cameraLens = value;
                if (_refreshed)
                {
                    _cameraLens.OnUpdate(this);
                }
            }
        }

        private void Refresh()
        {
            if (LookFrom.IsEmpty || LookAt.IsEmpty || VUP.IsEmpty || CameraLens == null)
            {
                return;
            }
            var theta = FOV * Math.PI / 180D;
            halfHeight = Math.Tan(theta / 2D);
            halfWidth = Aspect * halfHeight;

            w = Vector3.UnitVector(LookFrom - LookAt);
            u = Vector3.UnitVector(Vector3.Cross(VUP, w));
            v = Vector3.Cross(w, u);

            CameraLens.OnUpdate(this);
            _refreshed = true;
        }

        public Ray GetRay(double u, double v)
        {
            return CameraLens.GetRay(u, v);
        }
    }
}
