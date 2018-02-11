using System;
using System.Threading.Tasks;

namespace EasyMvvm.Core.Services
{
    public interface IPopUpsService
    {
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
        Task DisplayAlert(string title, string message, string cancel, string accept = null);
    }
}
