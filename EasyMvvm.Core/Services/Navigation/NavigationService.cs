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
        #region Hierarchical Navigation

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

        #endregion

        #region Modal Pages

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

        #endregion

        Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            return Activator.CreateInstance(pageType) as Page;
        }

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        Page GetCurrentPage()
        {
            var mainPage = Application.Current.MainPage;

            if(mainPage is MasterDetailPage)
            {
                return ((MasterDetailPage)mainPage).Detail;
            }

            // TabbedPage : MultiPage<Page>
            // CarouselPage : MultiPage<ContentPage>
            if(mainPage is TabbedPage || mainPage is CarouselPage)
            {
                return ((MultiPage<Page>)mainPage).CurrentPage;
            }

            return mainPage;
        }

        public void SetRootPage<TViewModel>(bool shouldBeWrappedByNavigationPage) where TViewModel : BaseViewModel
        {
            var page = CreatePage(typeof(TViewModel));
            Application.Current.MainPage = shouldBeWrappedByNavigationPage ? new NavigationPage(page) : page;
        }
    }
}