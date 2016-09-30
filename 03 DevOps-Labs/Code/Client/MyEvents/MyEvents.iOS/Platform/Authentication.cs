using Microsoft.WindowsAzure.MobileServices;
using MyEvents.Helpers;
using MyEvents.iOS;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authentication))]
namespace MyEvents.iOS
{
    public class Authentication : IAuthentication
    {
        public async Task<bool> Authenticate()
        {
            var user = await App.DataManager.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,
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