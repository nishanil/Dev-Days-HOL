using MyEvents.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyEvents.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void LoginClicked(object sender, EventArgs e)
        {
            if (!Settings.IsLoggedIn)
            {
                var authenticator = DependencyService.Get<IAuthentication>();
                await authenticator.Authenticate();

            }
            await Navigation.PopModalAsync();

        }
    }
}
