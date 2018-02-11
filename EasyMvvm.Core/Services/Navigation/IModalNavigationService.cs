using System.Threading.Tasks;
using EasyMvvm.Core.ViewModels;

namespace EasyMvvm.Core.Services
{
    public interface IModalNavigationService
    {
        Task PushModalAsync<TViewModel>(bool animated = true) where TViewModel : BaseViewModel;
        Task PushModalAsync<TViewModel>(object parameter, bool animated = true) where TViewModel : BaseViewModel;
        Task PopModalAsync(bool animated = true);
    }
}
