using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;

namespace RaytraceTest
{
    [TestClass]
    public class RayTest
    {
        [TestMethod]
        public void Allocation()
        {
            var v = new Vector3(1, 2, 3);
            var direction = new Vector3(4, 5, 6);
            var ray = new Ray(v, direction);

            Assert.AreEqual(1, ray.Origin[0]);
            Assert.AreEqual(2, ray.Origin[1]);
            Assert.AreEqual(3, ray.Origin[2]);
            Assert.AreEqual(4, ray.Direction[0]);
            Assert.AreEqual(5, ray.Direction[1]);
            Assert.AreEqual(6, ray.Direction[2]);

            v = new Vector3(4, 5, 6);
            direction = new Vector3(1, 2, 3);
            ray = new Ray(v, direction);

            Assert.AreEqual(4, ray.Origin[0]);
            Assert.AreEqual(5, ray.Origin[1]);
            Assert.AreEqual(6, ray.Origin[2]);
            Assert.AreEqual(1, ray.Direction[0]);
            Assert.AreEqual(2, ray.Direction[1]);
            Assert.AreEqual(3, ray.Direction[2]);
        }

        [TestMethod]
        public void PointAt()
        {
            var v = new Vector3(1, 2, 3);
            var direction = new Vector3(4, 5, 6);
            var ray = new Ray(v, direction);

            var point = ray.PointAt(1);
            Assert.AreEqual(5, point[0]);
            Assert.AreEqual(7, point[1]);
            Assert.AreEqual(9, point[2]);

            v = new Vector3(1, 2, 3);
            direction = new Vector3(4, 5, 6);
            ray = new Ray(v, direction);

            point = ray.PointAt(2);
            Assert.AreEqual(9, point[0]);
            Assert.AreEqual(12, point[1]);
            Assert.AreEqual(15, point[2]);
        }
    }
}
