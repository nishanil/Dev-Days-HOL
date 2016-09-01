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

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
