namespace RayTrace
{
    public abstract class Lens
    {
        public abstract Ray GetRay(Camera camera, double u, double v);
    }


}
