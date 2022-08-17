namespace RayTrace
{
    public class Camera
    {

        Vector3 _lowerLeftCorner;
        Vector3 _horizontal;
        Vector3 _vertical;
        Vector3 _origin;
        public Camera()
        {
            _lowerLeftCorner = new Vector3(-2, -1, -1);
            _horizontal = new Vector3(4, 0, 0);
            _vertical = new Vector3(0, 2, 0);
            _origin = new Vector3(0, 0, 0);
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(_origin, _lowerLeftCorner + u * _horizontal + v * _vertical);
        }
    }
}
