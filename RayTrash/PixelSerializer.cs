using System.Windows.Media;

namespace RayTrash
{
    public abstract class PixelSerializer
    {
        public abstract PixelFormat PixelFormat { get; }

        public abstract void Write(double r, double v, double b, double alpha, byte[] bytes, int index);
    }
}
