using System;
using System.Threading.Tasks;

namespace EasyMvvm.Core.Services
{
    public class PopUpsService : BaseNavigationService, IPopUpsService
    {
        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons) =>
            GetCurrentPage().DisplayActionSheet(title, cancel, destruction, buttons);

        public Task DisplayAlert(string title, string message, string cancel, string accept = null) =>
            GetCurrentPage().DisplayAlert(title, message, accept, cancel);
    }
}
