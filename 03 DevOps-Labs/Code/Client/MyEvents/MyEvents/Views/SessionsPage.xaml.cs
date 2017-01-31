using MyEvents.Models;
using MyEvents.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyEvents.Views
{
    public partial class SessionsPage : ContentPage
    {
        public SessionsPage()
        {
            InitializeComponent();
        }

        public SessionsViewModel ViewModel { get { return (BindingContext as SessionsViewModel); } }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Session;
            if (item == null)
                return;

            await Navigation.PushAsync(new SessionDetailPage() { BindingContext = new SessionDetailViewModel(item)});
           
            // Manually deselect item
            SessionsListView.SelectedItem = null;
        }

        void OnSyncClicked(object sender, EventArgs e)
        {
            ViewModel?.GetSessionsCommand?.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel.Sessions.Count == 0)
                ViewModel.GetSessionsCommand.Execute(null);
        }
    }
}
