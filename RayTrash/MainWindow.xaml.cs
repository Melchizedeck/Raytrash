using RayTrace;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RayTrash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly Renderer _renderer;
        public MainWindow()
        {
            _renderer = new Renderer();
            InitializeComponent();
            DataContext = this;
        }
        public BitmapSource RenderedBitmap { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var context = new RenderContext(200, 100, this);
            _renderer.Render(context);
        }

        private class RenderContext : IRenderContext
        {
            private readonly MainWindow _window;

            public RenderContext(int width, int height, MainWindow window)
            {
                Width = width;
                Height = height;
                _window = window;

            }
            public int Width { get; }

            public int Height { get; }


            private PixelFormat _pixelFormat;
            int _bytesPerPixel;
            private int _stride;
            private byte[] _bytes;
            public void OnInit()
            {
                _pixelFormat = PixelFormats.Rgb24;
                _bytesPerPixel = (_pixelFormat.BitsPerPixel + 7) / 8;
                _stride = Width * _bytesPerPixel;
                _bytes = new byte[Height * _stride];
            }

            public void OnRender(int x, int y, double r, double g, double b, double alpha)
            {
                var pixelIndex = (Height - 1 - y) * _stride + x * _bytesPerPixel;
                _bytes[pixelIndex] = (byte)(255D * r);
                _bytes[pixelIndex + 1] = (byte)(255D * g);
                _bytes[pixelIndex + 2] = (byte)(255D * b);
            }

            public void OnFinalise()
            {
                var dpiX = 96d;
                var dpiY = 96d;
                _window.RenderedBitmap = BitmapSource.Create(Width, Height, dpiX, dpiY, _pixelFormat, null, _bytes, _stride);
                _window.PropertyChanged?.Invoke(_window, new PropertyChangedEventArgs(nameof(RenderedBitmap)));
            }
        }
    }
}
