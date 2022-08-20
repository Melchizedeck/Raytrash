using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;

namespace RaytraceTest
{
    [TestClass]
    public class NormaalRaytracerTest
    {
        [TestMethod]
        public void Allocation()
        {
            var tracer = new NormalRayTracer();
        }
    }

}
