using System;
using System.Collections.Generic;

namespace RayTrace
{
    public class Renderer
    {
        public Renderer()
        {
            RayTracer = new MaterialRayTracer();
            Sampler = new DirectSampler();
            Hitables = new List<Hitable>
            {
                new Sphere{ Center= new Vector3(0,0,-1), Radius=0.5f },
                new Sphere{ Center= new Vector3(0,-100.5f,-1), Radius=100f },
            };
        }

        public RayTracer RayTracer { get; set; }
        public Sampler Sampler { get; set; }
        public List<Hitable> Hitables { get; set; }

        public void Render(IRenderContext renderContext)
        {
            if (RayTracer == null)
            {
                throw new ArgumentNullException(nameof(RayTracer));
            }

            if (Sampler == null)
            {
                throw new ArgumentNullException(nameof(Sampler));
            }

            if (Hitables == null)
            {
                throw new ArgumentNullException(nameof(Hitables));
            }

            int nx = renderContext.Width;
            int ny = renderContext.Height;
            renderContext.OnInit();
            var camera = new Camera();

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {
                    var col = Sampler.color(i, j, nx, ny, camera, RayTracer, Hitables);
                    col = new Vector3((float)Math.Sqrt(col[0]),(float)Math.Sqrt(col[1]),(float)Math.Sqrt(col[2]));
                    renderContext.OnRender(i, j, col[0], col[1], col[2], 1);
                }
            }

            renderContext.OnFinalise();
        }
    }
}
