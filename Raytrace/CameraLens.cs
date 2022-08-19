namespace RayTrace
{
    public abstract class CameraLens
    {
        public abstract void OnUpdate(Camera camera);
        public abstract Ray GetRay(double u, double v);
    }
}
