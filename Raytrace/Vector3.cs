using System;

namespace RayTrace
{
    public struct Vector3
    {
        private readonly double[] _values;

        public Vector3(double x, double y, double z)
        {
            _values = new[] { x, y, z };
        }

        public static Vector3 Random()
            => new Vector3(ThreadSafeRandom.NextDouble(), ThreadSafeRandom.NextDouble(), ThreadSafeRandom.NextDouble());

        public static Vector3 Random(double min, double max)
            => new Vector3(ThreadSafeRandom.NextDouble(min, max), ThreadSafeRandom.NextDouble(min, max), ThreadSafeRandom.NextDouble(min, max));

        public bool IsNearZero
        {
            get
            {
                var s = 1e-8;
                return Math.Abs(this[0]) < s && Math.Abs(this[1]) < s && Math.Abs(this[2]) < s;
            }
        }
        public bool IsEmpty => _values == null;
        public double Length => Math.Sqrt(SquaredLength);
        public double SquaredLength => _values[0] * _values[0] + _values[1] * _values[1] + _values[2] * _values[2];


        public double this[int index] { get => _values[index]; }
        public static Vector3 operator -(Vector3 a) => new Vector3(-a[0], -a[1], -a[2]);
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a[0] + b[0], a[1] + b[1], a[2] + b[2]);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a[0] - b[0], a[1] - b[1], a[2] - b[2]);
        public static Vector3 operator *(Vector3 a, Vector3 b) => new Vector3(a[0] * b[0], a[1] * b[1], a[2] * b[2]);
        public static Vector3 operator /(Vector3 a, Vector3 b) => new Vector3(a[0] / b[0], a[1] / b[1], a[2] / b[2]);
        public static Vector3 operator /(Vector3 a, double b) => new Vector3(a[0] / b, a[1] / b, a[2] / b);
        public static Vector3 operator *(Vector3 a, double b) => new Vector3(a[0] * b, a[1] * b, a[2] * b);
        public static Vector3 operator *(double b, Vector3 a) => new Vector3(a[0] * b, a[1] * b, a[2] * b);

        public static double Dot(Vector3 a, Vector3 b) => a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
        public static Vector3 Cross(Vector3 a, Vector3 b) => new Vector3(a[1] * b[2] - a[2] * b[1], -(a[0] * b[2] - a[2] * b[0]), a[0] * b[1] - a[1] * b[0]);
        public static Vector3 UnitVector(Vector3 a) => a / a.Length;


        public override string ToString()
        {
            if (_values == null)
            {
                return "[Empty]";
            }
            return $"{_values[0]};{_values[1]};{_values[2]}";
        }

    }

}
