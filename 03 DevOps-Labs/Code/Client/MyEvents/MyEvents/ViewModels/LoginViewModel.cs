using MyEvents.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyEvents.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public INavigation Navigation { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;

            LoginCommand = new Command(async () =>
            {
                if (!Settings.IsLoggedIn)
                {
                    var authenticator = DependencyService.Get<IAuthentication>();
                    await authenticator.Authenticate();
                }
                if (Device.OS == TargetPlatform.iOS)
                {
                    await Navigation?.PopAsync();
                }
                else
                    await Navigation?.PopModalAsync();

            });
        }
    }
}
