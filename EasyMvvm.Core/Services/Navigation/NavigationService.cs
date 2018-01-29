using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using EasyMvvm.Core.ViewModels;
using Xamarin.Forms;

namespace EasyMvvm.Core.Services
{
    public class NavigationService : INavigationService
    {
        public async Task PopAsync(bool animated = true)
        {
            var mainPage = Application.Current.MainPage as NavigationPage;
            await mainPage.Navigation.PopAsync(animated);
        }

        public Task PushAsync<TViewModel>(bool animated = true) where TViewModel : BaseViewModel =>
            InternalPushAsync(typeof(TViewModel), animated);

        public Task PushAsync<TViewModel>(object parameter, bool animated = true) where TViewModel : BaseViewModel =>
            InternalPushAsync(typeof(TViewModel), animated, parameter);

        async Task InternalPushAsync(Type viewModelType, bool animated, object parameter = null)
        {
            var page = CreatePage(viewModelType);
            var navigationPage = Application.Current.MainPage as NavigationPage;

            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page, animated);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = ViewModelLocator.Resolve(viewModelType);
            page.BindingContext = viewModel;

            return page;
        }

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
    }
}
