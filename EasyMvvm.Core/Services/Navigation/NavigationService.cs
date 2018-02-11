using System;
using System.Threading.Tasks;
using EasyMvvm.Core.ViewModels;
using Xamarin.Forms;

namespace EasyMvvm.Core.Services
{
    public class NavigationService : BaseNavigationService, INavigationService
    {
        public Task PopAsync(bool animated = true)
        {
            var mainPage = GetCurrentPage() as NavigationPage;
            if(mainPage != null)
                return mainPage.Navigation.PopAsync(animated);
            
            throw new Exception("PopAsync is supported only on NavigationPages.");
        }

        public Task PushAsync<TViewModel>(bool animated = true) where TViewModel : BaseViewModel =>
            InternalPushAsync(typeof(TViewModel), animated);
         
        public Task PushAsync<TViewModel>(object parameter, bool animated = true) where TViewModel : BaseViewModel =>
            InternalPushAsync(typeof(TViewModel), animated, parameter);

        async Task InternalPushAsync(Type viewModelType, bool animated, object parameter = null)
        {
            var page = CreatePage(viewModelType);
            var currentNavigationPage = GetCurrentPage() as NavigationPage;

            if (currentNavigationPage != null)
            {
                await currentNavigationPage.PushAsync(page, animated);
            }
            else
            {
                throw new Exception("PushAsync is supported only on NavigationPages.");
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        public void SetRootPage<TViewModel>(bool shouldBeWrappedByNavigationPage) where TViewModel : BaseViewModel
        {
            var page = CreatePage(typeof(TViewModel));
            Application.Current.MainPage = shouldBeWrappedByNavigationPage ? new NavigationPage(page) : page;
        }
    }
}