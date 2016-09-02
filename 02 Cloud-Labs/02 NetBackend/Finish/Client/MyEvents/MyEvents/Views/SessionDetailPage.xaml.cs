using MyEvents.Helpers;
using MyEvents.ViewModels;
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
            {
                await Navigation.PushModalAsync(new FeedbackPage(((SessionDetailViewModel)BindingContext).SelectedSession));
            }
            else
            {
                isFeedbackPageRequested = true;

                if (Device.OS == TargetPlatform.iOS)
                {
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                await Navigation.PushModalAsync(new LoginPage());

            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (isFeedbackPageRequested && Settings.IsLoggedIn)
            {
                await Navigation.PushModalAsync(new FeedbackPage(((SessionDetailViewModel)BindingContext).SelectedSession));
                isFeedbackPageRequested = false;
                
            }

        }
    }
}
