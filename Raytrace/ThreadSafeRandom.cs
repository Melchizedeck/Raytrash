using System;

namespace RayTrace
{
    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random((int)DateTime.Now.Ticks);
        [ThreadStatic]
        private static Random _local;
        public static double NextDouble()
        {
            var inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) { seed = _global.Next(); }
                _local = new Random(seed);
            }
            return _local.NextDouble();
        }

        public static double NextDouble(double min, double max)
            => min + (max - min) * NextDouble();

    }
}
