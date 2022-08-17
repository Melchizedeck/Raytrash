namespace RayTrace
{
    public class AutoFocus : Focus
    {
        public override float GetFocusDistance(Camera camera)
        {
            return (camera.LookFrom - camera.LookAt).Length;
        }
    }


}
