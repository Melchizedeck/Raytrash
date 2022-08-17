using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RayTrash
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T>(ref T variable, T value, [CallerMemberName] string propertyName = null)
        {
            variable = value;
            RaisePropertyChangedEvent(propertyName);
        }
    }
}
