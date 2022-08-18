using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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
                new Sphere{ Center= new Vector3(0,0,-1), Radius=0.5, Material = new Lambertian{ Albedo=new Vector3(0.8, 0.3, 0.3) } },
                new Sphere{ Center= new Vector3(0,-100.5,-1), Radius=100, Material = new Lambertian{ Albedo=new Vector3(0.8, 0.8, 0) } },
                new Sphere{ Center= new Vector3(1,0,-1), Radius=0.5, Material = new Metal{ Albedo=new Vector3(0.8, 0.6, 0.2), Fuzz=0.3 } },
                new Sphere{ Center= new Vector3(-1,0,-1), Radius=0.5, Material = new Dielectric{  RefractionIndex=1.5 } },
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
            MaxDegreOfParallelism = Environment.ProcessorCount;
        }
        public int MaxDegreOfParallelism { get; set; }
        public Camera Camera { get; set; }
        public RayTracer RayTracer { get; set; }
        public Sampler Sampler { get; set; }
        public List<Hitable> Hitables { get; set; }

        public async Task Render(IRenderContext renderContext, IProgress<double> progress, CancellationToken cancellation)
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

            await Task.Run(() =>
            {
                int nx = renderContext.Width;
                int ny = renderContext.Height;
                renderContext.OnInit();

                Camera.Aspect = (double)nx / ny;

                var progressIncrement = 1D / ny;
                var progressValue = 0D;
                void Render(int y)
                {
                    for (var i = 0; i < nx; i++)
                    {
                        var col = Sampler.color(i, y, nx, ny, Camera, RayTracer, Hitables);
                        col = new Vector3(Math.Sqrt(col[0]), Math.Sqrt(col[1]), Math.Sqrt(col[2]));
                        renderContext.OnRender(i, y, col[0], col[1], col[2], 1);
                    }
                    progressValue += progressIncrement;
                    progress.Report(progressValue);
                }
                var options = new ExecutionDataflowBlockOptions
                {
                    CancellationToken = cancellation,
                    MaxDegreeOfParallelism = MaxDegreOfParallelism
                };
                var action = new ActionBlock<int>(Render, options);

                for (var j = ny - 1; j >= 0; j--)
                {
                    action.Post(j);
                }

                action.Complete();

                action.Completion.Wait(cancellation);
            }, cancellation)
                .ContinueWith(t => renderContext.OnFinalise());
        }
        public void Render(IRenderContext renderContext)
        {
            Render(renderContext, new Progress<double>(), CancellationToken.None).Wait();
        }
    }
}
