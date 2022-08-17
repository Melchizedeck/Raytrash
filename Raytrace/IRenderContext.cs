namespace RayTrace
{
    public interface IRenderContext
    {
        int Width { get; }
        int Height { get; }

        void OnInit();
        void OnRender(int x, int y, double r, double g, double b, double alpha);
        void OnFinalise();
    }
}
