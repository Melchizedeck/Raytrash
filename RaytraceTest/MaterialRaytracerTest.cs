using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;

namespace RaytraceTest
{
    [TestClass]
    public class MaterialRaytracerTest
    {
        [TestMethod]
        public void Allocation()
        {
            var tracer = new MaterialRayTracer();
        }
    }

}
