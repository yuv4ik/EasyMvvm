using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EasyMvvm.Core.Services;

namespace EasyMvvm.Core.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public bool IsBusy { get; set; }

        protected INavigationService NavigationService { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);

        public BaseViewModel()
        {
            NavigationService = TypeRegistry.Resolve<INavigationService>();
        }
    }
}
