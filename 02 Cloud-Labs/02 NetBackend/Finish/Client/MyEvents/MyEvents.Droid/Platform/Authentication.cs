using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyEvents.Droid;
using MyEvents.Helpers;
using Microsoft.WindowsAzure.MobileServices;

[assembly: Dependency(typeof(Authentication))]
namespace MyEvents.Droid
{
    public class Authentication : IAuthentication
    {
        public async Task<bool> Authenticate()
        {
            var user = await App.DataManager.CurrentClient.LoginAsync(Forms.Context,
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