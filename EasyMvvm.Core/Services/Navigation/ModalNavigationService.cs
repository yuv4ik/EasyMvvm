using System;
using System.Threading.Tasks;
using EasyMvvm.Core.ViewModels;

namespace EasyMvvm.Core.Services
{
    public class ModalNavigationService : BaseNavigationService, IModalNavigationService
    {
        public Task PushModalAsync<TViewModel>(bool animated = true) where TViewModel : BaseViewModel =>
           InternalPushModalAsync(typeof(TViewModel), animated);

        public Task PushModalAsync<TViewModel>(object parameter, bool animated = true) where TViewModel : BaseViewModel =>
            InternalPushModalAsync(typeof(TViewModel), animated, parameter);

        public Task PopModalAsync(bool animated = true)
        {
            var mainPage = GetCurrentPage();
            if (mainPage != null)
                return mainPage.Navigation.PopModalAsync(animated);

            throw new Exception("Current page is null.");
        }

        async Task InternalPushModalAsync(Type viewModelType, bool animated, object parameter = null)
        {
            var page = CreatePage(viewModelType);
            var currentNavigationPage = GetCurrentPage();

            if (currentNavigationPage != null)
            {
                await currentNavigationPage.Navigation.PushModalAsync(page, animated);
            }
            else
            {
                throw new Exception("Current page is null.");
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }
    }
}
