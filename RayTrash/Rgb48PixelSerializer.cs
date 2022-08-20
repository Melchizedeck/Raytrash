using System;
using System.Windows.Media;

namespace RayTrash
{
    public class Rgb48PixelSerializer : PixelSerializer
    {
        public override PixelFormat PixelFormat => PixelFormats.Rgb48;

        public override void Write(double r, double g, double b, double alpha, byte[] bytes, int index)
        {
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index, 2), (ushort)(ushort.MaxValue * r));
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 2, 2), (ushort)(ushort.MaxValue * g));
            BitConverter.TryWriteBytes(new Span<byte>(bytes, index + 4, 2), (ushort)(ushort.MaxValue * b));
        }
    }
}
