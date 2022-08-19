namespace RayTrace
{
    public interface ICamera
    {
        Ray GetRay(double u, double v);
    }
}
