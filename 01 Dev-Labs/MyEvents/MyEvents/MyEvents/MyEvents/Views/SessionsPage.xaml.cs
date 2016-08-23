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

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Session;
            if (item == null)
                return;

            //viewModel.GoToDetailsCommand.Execute(item.Id);

            // Manually deselect item
            SessionsListView.SelectedItem = null;
        }

        void OnGetClicked(object sender, EventArgs e)
        {
            ViewModel?.GetSessionsCommand?.Execute(null);
        }
    }
}
