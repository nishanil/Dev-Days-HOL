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
    public partial class SpeakerDetailPage : ContentPage
    {
        public SpeakerDetailPage()
        {
            InitializeComponent();
        }

        Speaker speaker;
        SpeakersViewModel vm;
        public SpeakerDetailPage(Speaker selectedSpeaker, SpeakersViewModel vm) : this()
        {
            BindingContext = this.speaker= selectedSpeaker;
            this.vm = vm;
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            speaker.Title = EntryTitle.Text;
            await vm.UpdateSpeaker(speaker);
            await Navigation.PopAsync();
        }
    }
}
