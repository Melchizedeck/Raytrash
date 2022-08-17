namespace RayTrace
{
    public class FixFocus : Focus
    {
        private float _distance;
        public float Distance
        {
            get => _distance;
            set { _distance = value; }
        }

        public override float GetFocusDistance(Camera camera)
        {
            return Distance;
        }
    }


}
