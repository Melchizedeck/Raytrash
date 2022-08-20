using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;

namespace RaytraceTest
{
    [TestClass]
    public class HitRaytracerTest
    {
        [TestMethod]
        public void Allocation()
        {
            var tracer = new HitRayTracer();
        }
    }

}
