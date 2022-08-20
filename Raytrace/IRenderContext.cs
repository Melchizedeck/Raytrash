using System.Collections.Generic;
using System.Threading.Tasks;

namespace RayTrace
{
    public interface IRenderContext
    {
        int Width { get; }
        int Height { get; }

        ICamera Camera { get; }

        IEnumerable<IHitable> Hitables { get; }

        Task OnInit();
        Task OnRender(int x, int y, double r, double g, double b, double alpha);
        Task OnFinalise();
    }
}
