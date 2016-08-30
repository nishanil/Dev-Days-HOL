using MyEvents.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyEvents.Views
{
    public partial class SessionDetailPage : ContentPage
    {
        bool isFeedbackPageRequested = false;
        public SessionDetailPage()
        {
            InitializeComponent();
        }

        private async void FeedBackButton_Clicked(object sender, EventArgs e)
        {
            if (Settings.IsLoggedIn)
                await Navigation.PushModalAsync(new FeedbackPage());
            else
            {
                isFeedbackPageRequested = true;
                await Navigation.PushModalAsync(new LoginPage());
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (isFeedbackPageRequested && Settings.IsLoggedIn)
            {
                await Navigation.PushModalAsync(new FeedbackPage());
                isFeedbackPageRequested = false;
                
            }

        }
    }
}
