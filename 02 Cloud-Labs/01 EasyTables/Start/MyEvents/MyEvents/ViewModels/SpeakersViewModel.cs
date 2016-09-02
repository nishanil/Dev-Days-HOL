using MyEvents.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyEvents.ViewModels
{
    public class SpeakersViewModel : ViewModelBase
    {
        public ObservableCollection<Speaker> Speakers { get; set; }

        public ICommand GetSpeakersCommand { get; set; }

        public SpeakersViewModel()
        {
            Speakers = new ObservableCollection<Speaker>();
            Title = "Speakers";
            GetSpeakersCommand = RefreshCommand = new Command(
                async () => await GetSpeakers(),
                () => !IsBusy);
        }

        //TODO: ADD UpdateSpeaker() here

        async Task GetSpeakers()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                IsBusy = true;
                
                // TODO: Get data from Azure.
                var items = new List<Speaker> ();

                Speakers.Clear();
                foreach (var item in items)
                    Speakers.Add(item);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }

            if (error != null)
                await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
        }

    }
}
