using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RaytraceTest
{
    [TestClass]
    public class RendererTest
    {
        class TestContext : IRenderContext<Hitable>
        {
            private readonly int _width;
            private readonly int _height;
            private readonly ICamera _camera;
            readonly IEnumerable<IHitable<Hitable>> _hitables;
            public TestContext(int width, int height, ICamera camera, IEnumerable<IHitable<Hitable>> hitables)
            {
                _width = width;
                _height = height;
                _camera = camera;
                _hitables = hitables;
            }
            public int Width => _width;

            public int Height => _height;

            public ICamera Camera => _camera;

            public IEnumerable<IHitable<Hitable>> Hitables => _hitables;

            public event EventHandler Finalized;
            public Task OnFinalise()
            {

                Finalized?.Invoke(this, new EventArgs());
                return Task.Run(() => { });
            }
            public event EventHandler Initialized;
            public Task OnInit()
            {
                Initialized?.Invoke(this, new EventArgs());
                return Task.Run(() => { });
            }
            public event EventHandler<RenderEventArgs> Rendered;
            public Task OnRender(int x, int y, double r, double g, double b, double alpha)
            {
                Rendered?.Invoke(this, new RenderEventArgs(x, y, r, g, b, alpha));
                return Task.Run(() => { });
            }
        }

        public class RenderEventArgs : EventArgs
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public double R { get; private set; }
            public double G { get; private set; }
            public double B { get; private set; }
            public double A { get; private set; }
            public RenderEventArgs(int x, int y, double r, double g, double b, double alpha)
            {
                X = x;
                Y = y;
                R = r;
                G = g;
                B = b;
                A = alpha;
            }
        }

        class CameraDummy : ICamera
        {
            public Ray GetRay(double u, double v)
            {
                return new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            }
        }

        [TestMethod]
        public void Allocation()
        {
            var renderer = new Renderer();

            Assert.IsNotNull(renderer.Sampler);
            Assert.IsNotNull(renderer.RayTracer);
        }

        [TestMethod]
        public void AssertExceptions()
        {

            var camera = new CameraDummy();
            var hitables = new List<IHitable<Hitable>>();


            var progress = new Progress<double>();
            var context = new TestContext(1, 1, camera, hitables);

            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            var renderer = new Renderer { RayTracer = null };
            Assert.ThrowsExceptionAsync<ArgumentException>(() => renderer.Render(context, progress, cancellationTokenSource.Token));

            renderer = new Renderer { Sampler = null };
            Assert.ThrowsExceptionAsync<ArgumentException>(() => renderer.Render(context, progress, cancellationTokenSource.Token));
        }

        [TestMethod]
        public void RenderingWorkFlow()
        {
            var renderer = new Renderer();

            var camera = new CameraDummy();
            var hitables = new List<IHitable<Hitable>>();


            var progress = new Progress<double>();

            var progressRatio = 0D;
            progress.ProgressChanged += (o, e) => progressRatio = e;


            var initializeCount = 0;
            var finalizeCount = 0;
            var renderCount = 0;
            var context = new TestContext(1, 1, camera, hitables);
            context.Initialized += (o, e) => initializeCount++; ;
            context.Finalized += (o, e) => finalizeCount++; ;
            context.Rendered += (o, e) => renderCount++; ;

            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            var timeSpan = TimeSpan.FromSeconds(1);
            var succeed = renderer.Render(context, progress, cancellationTokenSource.Token).Wait(timeSpan);

            Assert.AreEqual(1D, progressRatio, "not progressed");

            Assert.IsTrue(succeed);

            Assert.AreEqual(1, initializeCount, "not initialized");
            Assert.AreEqual(1, renderCount, "not rendered once");
            Assert.AreEqual(1, finalizeCount, "not finalized");
        }
    }

}
