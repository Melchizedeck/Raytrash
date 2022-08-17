using System;
using System.Collections.Generic;

namespace RayTrace
{
    public class Renderer
    {
        public Renderer()
        {
            RayTracer = new NormalRayTracer();
            Hitables = new List<Hitable>
            {
                new Sphere{ Center= new Vector3(0,0,-1), Radius=0.5f },
                new Sphere{ Center= new Vector3(0,-100.5f,-1), Radius=100f },
            };
        }

        public RayTracer RayTracer { get; set; }

        public List<Hitable> Hitables { get; set; }

        public void Render(IRenderContext renderContext)
        {
            if (RayTracer == null)
            {
                throw new ArgumentNullException(nameof(RayTracer));
            }

            int nx = renderContext.Width;
            int ny = renderContext.Height;
            renderContext.OnInit();
            var camera = new Camera();

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {
                    var u = (float)i / nx;
                    var v = (float)j / ny;

                    var r = camera.GetRay(u, v);
                    var col = RayTracer.color(r, Hitables);

                    renderContext.OnRender(i, j, col[0], col[1], col[2], 1);
                }
            }

            renderContext.OnFinalise();
        }
    }
}
