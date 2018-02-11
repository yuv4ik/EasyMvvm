using System;
using Xamarin.Forms;

namespace EasyMvvm.Core.Services
{
    public abstract class BaseNavigationService
    {

        ITypeMapperService mapperService { get; }

        public BaseNavigationService()
        {
            mapperService = TypeRegistry.Resolve<ITypeMapperService>();
        }

        protected Page CreatePage(Type viewModelType)
        {
            Type pageType = mapperService.MapViewModelToView(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            return Activator.CreateInstance(pageType) as Page;
        }

        protected Page GetCurrentPage()
        {
            var mainPage = Application.Current.MainPage;

            if (mainPage is MasterDetailPage)
            {
                return ((MasterDetailPage)mainPage).Detail;
            }

            // TabbedPage : MultiPage<Page>
            // CarouselPage : MultiPage<ContentPage>
            if (mainPage is TabbedPage || mainPage is CarouselPage)
            {
                return ((MultiPage<Page>)mainPage).CurrentPage;
            }

            return mainPage;
        }

    }
}
