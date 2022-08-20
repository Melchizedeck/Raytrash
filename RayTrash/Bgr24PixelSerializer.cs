using System.Windows.Media;

namespace RayTrash
{
    public class Bgr24PixelSerializer : PixelSerializer
    {
        public override PixelFormat PixelFormat => PixelFormats.Bgr24;

        public override void Write(double r, double g, double b, double alpha, byte[] bytes, int index)
        {
            bytes[index] = (byte)(255D * b);
            bytes[index + 1] = (byte)(255D * g);
            bytes[index + 2] = (byte)(255D * r);
        }
    }
}
