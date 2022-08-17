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
                new Sphere{ Center= new Vector3(0,0,-1), Radius=0.5f, Material = new Lambertian{ Albedo=new Vector3(0.8f, 0.3f, 0.3f) } },
                new Sphere{ Center= new Vector3(0,-100.5f,-1), Radius=100f, Material = new Lambertian{ Albedo=new Vector3(0.8f, 0.8f, 0f) } },
                new Sphere{ Center= new Vector3(1,0,-1), Radius=0.5f, Material = new Metal{ Albedo=new Vector3(0.8f, 0.6f, 0.2f), Fuzz=0.3f } },
                new Sphere{ Center= new Vector3(-1,0,-1), Radius=0.5f, Material = new Dielectric{  RefractionIndex=1.5f } },
            };
            Camera = new Camera
            {
                FOV = 90,
                LookFrom = new Vector3(-2, 2, 1),
                LookAt = new Vector3(0, 0, -1),
                VUP = new Vector3(0, 1, 0),
                Focus = new AutoFocus(),
                Lens = new PerfectLens()
            };
        }
        public Camera Camera { get; set; }
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

            if (Camera == null)
            {
                throw new ArgumentNullException(nameof(Camera));
            }

            int nx = renderContext.Width;
            int ny = renderContext.Height;
            renderContext.OnInit();

            Camera.Aspect = (float)nx / (float)ny;

            for (var j = ny - 1; j >= 0; j--)
            {
                for (var i = 0; i < nx; i++)
                {
                    var col = Sampler.color(i, j, nx, ny, Camera, RayTracer, Hitables);
                    col = new Vector3((float)Math.Sqrt(col[0]), (float)Math.Sqrt(col[1]), (float)Math.Sqrt(col[2]));
                    renderContext.OnRender(i, j, col[0], col[1], col[2], 1);
                }
            }

            renderContext.OnFinalise();
        }
    }
}
