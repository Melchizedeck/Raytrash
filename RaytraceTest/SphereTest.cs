using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;

namespace RaytraceTest
{
    [TestClass]
    public class SphereTest
    {
        [TestMethod]
        public void Allocation()
        {
            var center = new Vector3(1, 2, 3);
            var sphere = new Sphere
            {
                Center = center,
                Radius = 22
            };

            Assert.AreEqual(1, sphere.Center[0]);
            Assert.AreEqual(2, sphere.Center[1]);
            Assert.AreEqual(3, sphere.Center[2]);
            Assert.AreEqual(22, sphere.Radius);

            center = new Vector3(4, 5, 6);
            sphere = new Sphere
            {
                Center = center,
                Radius = 16
            };

            Assert.AreEqual(4, sphere.Center[0]);
            Assert.AreEqual(5, sphere.Center[1]);
            Assert.AreEqual(6, sphere.Center[2]);
            Assert.AreEqual(16, sphere.Radius);
        }

        [TestMethod]
        public void Hit()
        {
            var sphere = new Sphere
            {
                Center = new Vector3(0, 0, -1),
                Radius = 0.5
            };

            var ray = new Ray(new Vector3(0, 0, 0), new Vector3(0, 0, -1));

            Assert.IsTrue(sphere.Hit(ray, 0, 2, out HitRecord record));
            Assert.AreEqual(0, record.normal[0]);
            Assert.AreEqual(0, record.normal[1]);
            Assert.AreEqual(0, record.normal[1]);

            Assert.AreEqual(0, record.p[0]);
            Assert.AreEqual(0, record.p[1]);
            Assert.AreEqual(0, record.p[1]);

            Assert.AreEqual(0.5, record.t);

        }
    }
}
