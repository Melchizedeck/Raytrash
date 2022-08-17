namespace RayTrace
{
    public abstract class Lens
    {
        public abstract Ray GetRay(Camera camera, float u, float v);
    }


}
