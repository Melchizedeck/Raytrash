using System;

namespace RayTrace
{
    public struct Vector3
    {
        private readonly float[] _values;
        public Vector3(float x, float y, float z)
        {
            _values = new[] { x, y, z };
        }
        public float Length => (float)Math.Sqrt(SquaredLength);
        public float SquaredLength => _values[0] * _values[0] + _values[1] * _values[1] + _values[2] * _values[2];


        public float this[int index] { get => _values[index]; }
        public static Vector3 operator -(Vector3 a) => new Vector3(-a[0], -a[1], -a[2]);
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a[0] + b[0], a[1] + b[1], a[2] + b[2]);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a[0] - b[0], a[1] - b[1], a[2] - b[2]);
        public static Vector3 operator *(Vector3 a, Vector3 b) => new Vector3(a[0] * b[0], a[1] * b[1], a[2] * b[2]);
        public static Vector3 operator /(Vector3 a, Vector3 b) => new Vector3(a[0] / b[0], a[1] / b[1], a[2] / b[2]);
        public static Vector3 operator /(Vector3 a, float b) => new Vector3(a[0] / b, a[1] / b, a[2] / b);
        public static Vector3 operator *(Vector3 a, float b) => new Vector3(a[0] * b, a[1] * b, a[2] * b);
        public static Vector3 operator *(float b, Vector3 a) => new Vector3(a[0] * b, a[1] * b, a[2] * b);

        public static float Dot(Vector3 a, Vector3 b) => a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
        public static Vector3 Cross(Vector3 a, Vector3 b) => new Vector3(a[1] * b[2] - a[2] * b[1], -(a[0] * b[2] - a[2] * b[0]), a[0] * b[1] - a[1] * b[0]);
        public static Vector3 UnitVector(Vector3 a) => a / a.Length;


    }

}
