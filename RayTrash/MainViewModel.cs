﻿using Microsoft.Win32;
using RayTrace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace RayTrash
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Dispatcher _dispatcher;
        private readonly Dictionary<string, Func<BitmapEncoder>> _getEncoders;
        private readonly Renderer _renderer;

        private BitmapSource _renderedBitmap;
        public BitmapSource RenderedBitmap
        {
            get => _renderedBitmap;
            private set { Set(ref _renderedBitmap, value); }
        }
        private Command _render;
        public ICommand Render => _render;
        private Command _save;
        public ICommand Save => _save;

        public IList<RayTracer> AvailableRayTracers { get; }

        private RayTracer _selectedRayTracer;
        public RayTracer SelectedRayTracer
        {
            get => _selectedRayTracer;
            set
            {
                Set(ref _selectedRayTracer, value);
                _renderer.RayTracer = value;
            }
        }

        public IList<Sampler> AvailableSamplers { get; }
        private Sampler _selectedSampler;
        public Sampler SelectedSampler
        {
            get => _selectedSampler;
            set
            {
                Set(ref _selectedSampler, value);
                _renderer.Sampler = value;
            }
        }

        private int _renderWidth;
        public int RenderWidth
        {
            get => _renderWidth;
            set { Set(ref _renderWidth, value); }
        }

        private int _renderHeight;
        public int RenderHeight
        {
            get => _renderHeight;
            set { Set(ref _renderHeight, value); }
        }

        public float FOV
        {
            get => _renderer.Camera.FOV;
            set
            {
                _renderer.Camera.FOV = value;
                RaisePropertyChangedEvent();
            }
        }

        public Vector3 LookFrom
        {
            get => _renderer.Camera.LookFrom;
            set
            {
                _renderer.Camera.LookFrom = value;
                RaisePropertyChangedEvent();
            }
        }

        public Vector3 LookAt
        {
            get => _renderer.Camera.LookAt;
            set
            {
                _renderer.Camera.LookAt = value;
                RaisePropertyChangedEvent();
            }
        }
        public Vector3 VUP
        {
            get => _renderer.Camera.VUP;
            set
            {
                _renderer.Camera.VUP = value;
                RaisePropertyChangedEvent();
            }
        }

        public MainViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;

            _renderer = new Renderer();
            _render = new Command(OnRender, CanRender);
            _save = new Command(OnSave, CanSave);
            _getEncoders = new Dictionary<string, Func<BitmapEncoder>>
            {
                {".png", ()=> new PngBitmapEncoder()},
                {".bmp", ()=> new BmpBitmapEncoder()},
                {".tif", ()=> new TiffBitmapEncoder ()},
                {".jpg", ()=> new JpegBitmapEncoder()},
                {".gif", ()=> new GifBitmapEncoder()},
            };

            AllowModifications = true;

            AvailableRayTracers = new List<RayTracer>
            {
                new HitRayTracer(),
                new NormalRayTracer(),
                new MaterialRayTracer()
            };

            SelectedRayTracer = AvailableRayTracers[AvailableRayTracers.Count - 1];

            AvailableSamplers = new List<Sampler>
            {
                new DirectSampler(),
                new RandomSampler{ RayCount = 50}
            };

            SelectedSampler = AvailableSamplers[AvailableSamplers.Count - 1];

            RenderWidth = 200;
            RenderHeight = 100;
            FOV = 90;
        }
        private void OnRender()
        {
            lock (this)
            {
                _isRendering = true;
                AllowModifications = false;
                _render.RaiseCanExecuteChanged();
            }
            var context = new RenderContext(RenderWidth, RenderHeight, this);

            Task.Run(() => _renderer.Render(context));
        }

        private bool _isRendering;
        private bool CanRender()
        {
            lock (this)
            {
                return !_isRendering;
            }
        }

        private void OnSave()
        {
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".png",
                Filter = "PNG (*.png)|*.png | BMP (*.bmp)|*.bmp | TIFF (*.tif)|*.tif | JPG (*.jpg)|*.jpg | GIF (*.gif)|*.gif"
            };

            var result = sfd.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }
            lock (this)
            {
                _isSaving = true;
                _save.RaiseCanExecuteChanged();
            }
            using (var fileStream = File.Create(sfd.FileName))
            {
                var extension = Path.GetExtension(sfd.FileName);
                if (_getEncoders.TryGetValue(extension, out Func<BitmapEncoder> getEncoder))
                {
                    var encoder = getEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(RenderedBitmap));
                    encoder.Save(fileStream);
                }
            }
            lock (this)
            {
                _isSaving = false;
                _save.RaiseCanExecuteChanged();
            }
        }
        private bool _isSaving;
        private bool CanSave()
        {
            lock (this)
            {
                return !_isSaving && RenderedBitmap != null;
            }
        }

        private bool _allowModifications;
        public bool AllowModifications
        {
            get => _allowModifications;
            private set => Set(ref _allowModifications, value);
        }

        private class RenderContext : IRenderContext
        {
            private readonly MainViewModel _viewModel;

            public RenderContext(int width, int height, MainViewModel viewModel)
            {
                Width = width;
                Height = height;
                _viewModel = viewModel;

            }
            public int Width { get; }

            public int Height { get; }


            private PixelFormat _pixelFormat;
            private int _bytesPerPixel;
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
                lock (_viewModel)
                {
                    _viewModel._isRendering = false;

                    Action onFinalise = () =>
                    {
                        _viewModel._render.RaiseCanExecuteChanged();
                        var dpiX = 96d;
                        var dpiY = 96d;
                        _viewModel.RenderedBitmap = BitmapSource.Create(Width, Height, dpiX, dpiY, _pixelFormat, null, _bytes, _stride);
                        _viewModel._save.RaiseCanExecuteChanged();
                    };
                    _viewModel.AllowModifications = true;
                    var operation = _viewModel._dispatcher.BeginInvoke(onFinalise);
                }
            }
        }
    }
}
