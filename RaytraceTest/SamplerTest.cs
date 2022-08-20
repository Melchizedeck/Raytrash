using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;

namespace RaytraceTest
{
    [TestClass]
    public class SamplerTest
    {
        [TestMethod]
        public void Allocation()
        {
            var sampler  = new DirectSampler();
        }
    }

}
