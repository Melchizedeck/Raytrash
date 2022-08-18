using System;

namespace RayTrace
{
    public class Camera
    {
        public Vector3 LowerLeftCorner { get; private set; }
        public Vector3 Horizontal { get; private set; }
        public Vector3 Vertical { get; private set; }


        public Vector3 w { get; private set; }
        public Vector3 u { get; private set; }
        public Vector3 v { get; private set; }

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
        public Lens Lens { get; set; }
        private Focus _focus;
        public Focus Focus
        {
            get => _focus;
            set { _focus = value; Refresh(); }
        }

        private void Refresh()
        {
            if (LookFrom.IsEmpty || LookAt.IsEmpty || VUP.IsEmpty || Focus == null)
            {
                return;
            }
            var theta = FOV * Math.PI / 180;
            var halfHeight = Math.Tan(theta / 2);
            var halfWidth = Aspect * halfHeight;

            var focus = Focus.GetFocusDistance(this);

            w = Vector3.UnitVector(LookFrom - LookAt);
            u = Vector3.UnitVector(Vector3.Cross(VUP, w));
            v = Vector3.Cross(w, u);

            LowerLeftCorner = LookFrom - halfWidth * u * focus - halfHeight * v * focus - w * focus;
            Horizontal = 2 * halfWidth * focus * u;
            Vertical = 2 * halfHeight * focus * v;
        }

        public Ray GetRay(double u, double v)
        {
            return Lens.GetRay(this, u, v);
        }
    }
}
