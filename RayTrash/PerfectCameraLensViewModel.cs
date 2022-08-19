using RayTrace;

namespace RayTrash
{
    public class PerfectCameraLensViewModel : CameraLensViewModel
    {
        private readonly PerfectLens _cameraLens;
        public PerfectCameraLensViewModel()
        {
            _cameraLens = new PerfectLens();
        }

        public override CameraLens CameraLens => _cameraLens;
    }
}
