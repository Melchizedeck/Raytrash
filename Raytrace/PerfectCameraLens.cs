namespace RayTrace
{
    public class PerfectLens : CameraLens
    {
        private Vector3 LowerLeftCorner ;
        private Vector3 Horizontal ;
        private Vector3 Vertical ;
        private Vector3 _lookFrom;
        public override void OnUpdate(Camera camera)
        {
            _lookFrom = camera.LookFrom;

            LowerLeftCorner = _lookFrom - camera.halfWidth * camera.u - camera.halfHeight * camera.v - camera.w;
            Horizontal = 2 * camera.halfWidth * camera.u;
            Vertical = 2 * camera.halfHeight * camera.v;
        }
        public override Ray GetRay(double u, double v)
        {
            return new Ray(_lookFrom, LowerLeftCorner + u * Horizontal + v * Vertical - _lookFrom);
        }
    }


}
