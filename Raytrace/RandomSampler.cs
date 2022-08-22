using System.Collections.Generic;

namespace RayTrace
{
    public class RandomSampler : Sampler
    {
        public RandomSampler()
        {
            RayCount = 50;
        }

        public int RayCount { get; set; }
        public override Vector3 Sample(int x, int y, int width, int height, ICamera camera, IRayTracer<Hitable> rayTracer, ICollection<IHitable<Hitable>> hitables)
        {
            var col = new Vector3(0, 0, 0);
            for (var i = 0; i < RayCount; i++)
            {
                var u = (x - 0.5 + ThreadSafeRandom.NextDouble()) / (width - 1);
                var v = (y - 0.5 + ThreadSafeRandom.NextDouble()) / (height - 1);

                var r = camera.GetRay(u, v);
                col += rayTracer.Trace(r, hitables);
            }

            return col / RayCount;
        }
    }
}
