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
    public partial class SpeakersPage : ContentPage
    {
        public SpeakersPage()
        {
            InitializeComponent();
        }


        public SpeakersViewModel ViewModel { get { return (BindingContext as SpeakersViewModel); } }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Speaker;
            if (item == null)
                return;

            //viewModel.GoToDetailsCommand.Execute(item.Id);

            // Manually deselect item
            SpeakersListView.SelectedItem = null;
        }

        void OnSyncClicked(object sender, EventArgs e)
        {
            ViewModel?.GetSpeakersCommand?.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel.Speakers.Count == 0)
                ViewModel.GetSpeakersCommand.Execute(null);
        }
    }
}
