using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTrace;

namespace RaytraceTest
{
    [TestClass]
    public class VectorTest
    {
         [TestMethod]
        public void Length()
        {
            var v = new Vector3(2, 3, 6);
            Assert.AreEqual(49, v.SquaredLength);
            Assert.AreEqual(7, v.Length);

            v = new Vector3(3, 6, 2);
            Assert.AreEqual(49, v.SquaredLength);
            Assert.AreEqual(7, v.Length);

            v = new Vector3(6, 2, 3);
            Assert.AreEqual(49, v.SquaredLength);
            Assert.AreEqual(7, v.Length);
        }

        [TestMethod]
        public void Allocation()
        {
            var v = new Vector3(1, 2, 3);
            Assert.AreEqual(1, v[0]);
            Assert.AreEqual(2, v[1]);
            Assert.AreEqual(3, v[2]);

            v = new Vector3(3, 5, 7);
            Assert.AreEqual(3, v[0]);
            Assert.AreEqual(5, v[1]);
            Assert.AreEqual(7, v[2]);
        }

        [TestMethod]
        public void Negative()
        {

            var v1 = new Vector3(1, 2, 3);


            var v = -v1;
            Assert.AreEqual(-1, v[0]);
            Assert.AreEqual(-2, v[1]);
            Assert.AreEqual(-3, v[2]);

            v = -v;
            Assert.AreEqual(1, v[0]);
            Assert.AreEqual(2, v[1]);
            Assert.AreEqual(3, v[2]);
        }

        [TestMethod]
        public void Addition()
        {

            var v1 = new Vector3(1, 2, 3);
            var v2 = new Vector3(18, 12, 13);

            var v = v1 + v2;
            Assert.AreEqual(19, v[0]);
            Assert.AreEqual(14, v[1]);
            Assert.AreEqual(16, v[2]);

            v = v2 + v1;
            Assert.AreEqual(19, v[0]);
            Assert.AreEqual(14, v[1]);
            Assert.AreEqual(16, v[2]);
        }

        [TestMethod]
        public void Substraction()
        {

            var v1 = new Vector3(1, 2, 3);
            var v2 = new Vector3(18, 12, 14);

            var v = v1 - v2;
            Assert.AreEqual(-17, v[0]);
            Assert.AreEqual(-10, v[1]);
            Assert.AreEqual(-11, v[2]);

            v = v2 - v1;
            Assert.AreEqual(17, v[0]);
            Assert.AreEqual(10, v[1]);
            Assert.AreEqual(11, v[2]);
        }

        [TestMethod]
        public void Multiplication()
        {

            var v1 = new Vector3(2, 3, 4);
            var v2 = new Vector3(4, 6, 8);

            var v = v1 * v2;
            Assert.AreEqual(8, v[0]);
            Assert.AreEqual(18, v[1]);
            Assert.AreEqual(32, v[2]);

            v = v2 * v1;
            Assert.AreEqual(8, v[0]);
            Assert.AreEqual(18, v[1]);
            Assert.AreEqual(32, v[2]);

            v = 2 * v1;
            Assert.AreEqual(4, v[0]);
            Assert.AreEqual(6, v[1]);
            Assert.AreEqual(8, v[2]);

            v = v1 * 4;
            Assert.AreEqual(8, v[0]);
            Assert.AreEqual(12, v[1]);
            Assert.AreEqual(16, v[2]);
        }

        [TestMethod]
        public void Division()
        {

            var v1 = new Vector3(2, 3, 4);
            var v2 = new Vector3(4, 3, 2);

            var v = v1 / v2;
            Assert.AreEqual(0.5, v[0]);
            Assert.AreEqual(1, v[1]);
            Assert.AreEqual(2, v[2]);

            v = v2 / v1;
            Assert.AreEqual(2, v[0]);
            Assert.AreEqual(1, v[1]);
            Assert.AreEqual(0.5, v[2]);

            v = v1 / 2;
            Assert.AreEqual(1, v[0]);
            Assert.AreEqual(1.5, v[1]);
            Assert.AreEqual(2, v[2]);
        }

        [TestMethod]
        public void Cross()
        {

            var v1 = new Vector3(1, 0, 0);
            var v2 = new Vector3(0, 1, 0);

            var cross = Vector3.Cross(v1, v2);

            Assert.AreEqual(0, cross[0]);
            Assert.AreEqual(0, cross[1]);
            Assert.AreEqual(1, cross[2]);


        }

        [TestMethod]
        public void Dot()
        {

            var v1 = new Vector3(1, 2, 3);
            var v2 = new Vector3(4, 5, 6);

            var dot = Vector3.Dot(v1, v2);

            Assert.AreEqual(32, dot);
        }

        [TestMethod]
        public void UnitVector()
        {

            var v = new Vector3(1, 2, 3);
            var unit = Vector3.UnitVector(v);
            Assert.AreEqual(1, unit.Length, 0.000001);

            v = new Vector3(100, 2, 343);
            unit = Vector3.UnitVector(v);
            Assert.AreEqual(1, unit.Length, 0.000001);
        }
    }
}
