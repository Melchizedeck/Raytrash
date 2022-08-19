using System.Collections.Generic;

namespace RayTrace
{
    public interface IRenderContext
    {
        int Width { get; }
        int Height { get; }

        ICamera Camera { get;}

        IEnumerable<IHitable> Hitables { get;}

        void OnInit();
        void OnRender(int x, int y, double r, double g, double b, double alpha);
        void OnFinalise();
    }
}
