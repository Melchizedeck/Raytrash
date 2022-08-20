using System;
using System.Windows.Media;

namespace RayTrash
{
    public class Rgba64PixelSerializer : PixelSerializer
    {
        public override PixelFormat PixelFormat => PixelFormats.Rgba64;

        public override void Write(double r, double g, double b, double a, byte[] bytes, int index)
        {

            BitConverter.TryWriteBytes(new Span<byte>(bytes, index, 2), (ushort)(ushort.MaxValue * r));
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 2, 2), (ushort)(ushort.MaxValue * g));
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 4, 2), (ushort)(ushort.MaxValue * b));
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 6, 2), (ushort)(ushort.MaxValue * a));
        }
    }
}
