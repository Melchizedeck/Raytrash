using System;
using System.Linq;
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
            MaxDegreOfParallelism = Environment.ProcessorCount;
        }
        public int MaxDegreOfParallelism { get; set; }

        public RayTracer RayTracer { get; set; }
        public Sampler Sampler { get; set; }

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

            var hitables = renderContext.Hitables.ToArray();


            int nx = renderContext.Width;
            int ny = renderContext.Height;
            await renderContext.OnInit();

            var progressIncrement = 1D / ny;
            var progressValue = 0D;
            async Task Render(int y)
            {
                for (var i = 0; i < nx; i++)
                {
                    var col = Sampler.Sample(i, y, nx, ny, renderContext.Camera, RayTracer, hitables);

                    await renderContext.OnRender(i, y, col[0], col[1], col[2], 1);
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

            await action.Completion.ContinueWith(async t => await renderContext.OnFinalise());
        }      
    }
}
