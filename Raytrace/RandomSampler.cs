using System;
using System.Collections.Generic;

namespace RayTrace
{
    public class RandomSampler : Sampler
    {
        private Random _random;
        public RandomSampler()
            : this(new Random())
        {
        }
        public RandomSampler(Random random)
        {
            _random = random;
            RayCount = 50;
        }

        public int RayCount { get; set; }
        public override Vector3 color(int x, int y, int width, int height, Camera camera, RayTracer rayTracer, ICollection<Hitable> hitables)
        {
            var col = new Vector3(0, 0, 0);
            for (var i = 0; i < RayCount; i++)
            {
                var u = (float)((x - 0.5d + _random.NextDouble()) / width);
                var v = (float)((y - 0.5d + _random.NextDouble()) / height);

                var r = camera.GetRay(u, v);
                col += rayTracer.color(r, hitables);
            }

            return col / RayCount;
        }
    }
}
