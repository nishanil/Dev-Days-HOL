using MyEvents.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyEvents
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainPage = new TabbedPage() { Title = "My Events" };
            mainPage.Children.Add(new NavigationPage(new SessionsPage()) { Title = "Sessions"});
            mainPage.Children.Add(new NavigationPage(new SpeakersPage()) { Title = "Speakers" });
            mainPage.Children.Add(new NavigationPage (new AboutPage()) { Title = "About" });


            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
