namespace RayTrace
{
    public class AutoFocus : Focus
    {
        public override double GetFocusDistance(Camera camera)
        {
            return (camera.LookFrom - camera.LookAt).Length;
        }
    }


}
