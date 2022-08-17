namespace RayTrace
{
    public class PerfectLens : Lens
    {
        public override Ray GetRay(Camera camera, float u, float v)
        {
            return new Ray(camera.LookFrom, camera.LowerLeftCorner + u * camera.Horizontal + v * camera.Vertical - camera.LookFrom);
        }
    }


}
