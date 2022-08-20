using System;
using System.Windows.Media;

namespace RayTrash
{
    public class Prgba128FloatPixelSerializer : PixelSerializer
    {
        public override PixelFormat PixelFormat => PixelFormats.Prgba128Float;

        public override void Write(double r, double g, double b, double a, byte[] bytes, int index)
        {

            BitConverter.TryWriteBytes(new Span<byte>(bytes, index, 4), (float)r);
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 4, 4), (float)g);
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 8, 4), (float)b);
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 12, 4), (float)a);
        }
    }
}
