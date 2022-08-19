using RayTrace;
using System.Collections.Generic;

namespace RayTrash
{
    public class RandomCameraLensViewModel : CameraLensViewModel
    {
        private readonly RandomLens _cameraLens;
        public RandomCameraLensViewModel()
        {
            _cameraLens = new RandomLens();
            AvailableFocusModes = new List<FocusMode> {
                FocusMode.Manual,
                FocusMode.Auto
            };
        }
        public IList<FocusMode> AvailableFocusModes { get; }
        public FocusMode SelectedFocusMode
        {
            get => _cameraLens.FocusMode;
            set
            {
                _cameraLens.FocusMode = value;
                RaisePropertyChangedEvent();
            }
        }
        public double Focus
        {
            get => _cameraLens.Focus;
            set
            {
                _cameraLens.Focus = value;
                RaisePropertyChangedEvent();
            }
        }

        private double _aperture;
        public double Aperture
        {
            get => _cameraLens.Aperture;
            set
            {
                _cameraLens.Aperture = value;
                RaisePropertyChangedEvent();
            }
        }

        public override CameraLens CameraLens => _cameraLens;
    }
}
