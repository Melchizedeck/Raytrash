﻿using Microsoft.Win32;
using RayTrace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private Command _randomizeScene;
        public ICommand RandomizeScene => _randomizeScene;
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

        public IList<Lens> AvailableLenses { get; }
        private Lens _selectedLens;
        public Lens SelectedLens
        {
            get => _renderer.Camera.Lens;
            set
            {
                Set(ref _selectedLens, value);
                _renderer.Camera.Lens = value;
            }
        }
        public IList<Focus> AvailableFocuses { get; }
        private Focus _selectedFocuses;
        public Focus SelectedFocus
        {
            get => _renderer.Camera.Focus;
            set
            {
                Set(ref _selectedFocuses, value);
                _renderer.Camera.Focus = value;
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

        private readonly Stopwatch _renderWatch;
        private TimeSpan _renderingDelay;
        public TimeSpan RenderingDelay { get => _renderingDelay; private set => Set(ref _renderingDelay, value); }
        public MainViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _renderWatch = new Stopwatch();
            _renderer = new Renderer();
            _render = new Command(OnRender, CanRender);
            _save = new Command(OnSave, CanSave);
            _randomizeScene = new Command(OnRandomizeScene);
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

            AvailableFocuses = new List<Focus>
            {
                new FixFocus{ Distance = 1},
                new AutoFocus()
            };

            SelectedFocus = AvailableFocuses[AvailableFocuses.Count - 1];

            AvailableLenses = new List<Lens>
            {
                new PerfectLens(),
                new RandomLens{  Aperture= 2},
            };

            SelectedLens = AvailableLenses[AvailableLenses.Count - 1];

            RenderWidth = 200;
            RenderHeight = 100;
            FOV = 90;
        }
        private void OnRender()
        {
            lock (this)
            {
                IsRendering = true;
                AllowModifications = false;
                RenderProgress = 0;
                _render.RaiseCanExecuteChanged();
                _renderWatch.Restart();
            }
            var context = new RenderContext(RenderWidth, RenderHeight, this);

            Task.Run(() => _renderer.Render(context));
        }

        private bool _isRendering;

        public bool IsRendering
        {
            get => _isRendering;
            private set => Set(ref _isRendering, value);
        }

        private double _renderProgress;

        public double RenderProgress
        {
            get => _renderProgress;
            private set => Set(ref _renderProgress, value);
        }

        private bool CanRender()
        {
            lock (this)
            {
                return !IsRendering;
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

        private void OnRandomizeScene()
        {
            var hitables = new List<Hitable>
            {
                new Sphere{ Center=new Vector3(0,-1000, 0), Radius=1000, Material =new Lambertian{ Albedo= new Vector3(0.5f, 0.5f, 0.5f) }},

                new Sphere{ Center=new Vector3(0,1, 0), Radius=1, Material =new Dielectric{ RefractionIndex=1.5f }},
                new Sphere{ Center=new Vector3(-4,1, 0), Radius=1, Material =new Lambertian{ Albedo= new Vector3(0.4f, 0.2f, 0.1f) }},
                new Sphere{ Center=new Vector3(4,1, 0), Radius=1, Material =new Metal{ Albedo= new Vector3(0.7f, 0.6f, 0.5f), Fuzz=0 }},
            };
            var random = new Random();
            for (var a = -11; a < 11; a++)
            {
                for (var b = -11; b < 11; b++)
                {
                    var center = new Vector3(a + (float)(0.9 * random.NextDouble()), 0.2f, b + (float)(0.9 * random.NextDouble()));

                    if ((center - new Vector3(4, 0.2f, 0)).Length > 0.9)
                    {
                        Material material;
                        var choseMaterial = random.NextDouble();
                        if (choseMaterial < 0.8f)
                        {
                            material = new Lambertian { Albedo = new Vector3((float)(random.NextDouble() * random.NextDouble()), (float)(random.NextDouble() * random.NextDouble()), (float)(random.NextDouble() * random.NextDouble())) };
                        }
                        else if (choseMaterial < 0.95f)
                        {
                            material = new Metal { Albedo = new Vector3((float)(0.5 * (1 + random.NextDouble())), (float)(0.5 * (1 + random.NextDouble())), (float)(0.5 * (1 + random.NextDouble()))), Fuzz = (float)(0.5 + random.NextDouble()) };
                        }
                        else
                        {
                            material = new Dielectric { RefractionIndex = 1.5f };
                        }
                        hitables.Add(new Sphere { Center = center, Radius = 0.2f, Material = material });
                    }
                }
            }

            LookFrom = new Vector3(10, 2, 2.5f);
            LookAt = new Vector3(0, 0, 0);
            FOV = 30;

            _renderer.Hitables = hitables;
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
                    _viewModel.IsRendering = false;
                    _viewModel._renderWatch.Stop();
                    _viewModel.RenderingDelay = _viewModel._renderWatch.Elapsed;
                    Action onFinalise = () =>
                    {
                        _viewModel._render.RaiseCanExecuteChanged();
                        var dpiX = 96d;
                        var dpiY = 96d;
                        _viewModel.RenderedBitmap = BitmapSource.Create(Width, Height, dpiX, dpiY, _pixelFormat, null, _bytes, _stride);
                        _viewModel._save.RaiseCanExecuteChanged();
                    };
                    _viewModel.AllowModifications = true;
                    _viewModel.RenderProgress = 1;
                    var operation = _viewModel._dispatcher.BeginInvoke(onFinalise);
                }
            }
        }
    }
}
