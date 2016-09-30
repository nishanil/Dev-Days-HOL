using MyEvents.UWP;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyEvents.Helpers;
using Microsoft.WindowsAzure.MobileServices;

[assembly: Dependency(typeof(Authentication))]
namespace MyEvents.UWP
{
    public class Authentication : IAuthentication
    {
        public async Task<bool> Authenticate()
        {
            var user = await MyEvents.App.DataManager.CurrentClient.LoginAsync(
                MobileServiceAuthenticationProvider.Facebook);
            if (user != null)
            {
                Settings.IsLoggedIn = true;
                Settings.UserId = user.UserId;
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
            }
            return true;
        }
    }
}
