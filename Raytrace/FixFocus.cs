namespace RayTrace
{
    public class FixFocus : Focus
    {
        private double _distance;
        public double Distance
        {
            get => _distance;
            set { _distance = value; }
        }

        public override double GetFocusDistance(Camera camera)
        {
            return Distance;
        }
    }


}
