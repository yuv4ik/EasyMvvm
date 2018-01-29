using System.Threading.Tasks;
using EasyMvvm.Core.ViewModels;

namespace EasyMvvm.Core.Services
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(bool animated = true) where TViewModel : BaseViewModel;
        Task PushAsync<TViewModel>(object parameter, bool animated = true) where TViewModel : BaseViewModel;
        Task PopAsync(bool animated = true);
    }
}
