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
    public class SessionsViewModel : ViewModelBase
    {
        
        public ObservableCollection<Session> Sessions { get; set; }

        public ICommand GetSessionsCommand { get; set; }

        public SessionsViewModel()
        {
            Sessions = new ObservableCollection<Session>();
            Title = "Sessions";
            GetSessionsCommand = RefreshCommand = new Command(
                async () => await GetSessions(),
                () => !IsBusy);
        }

        async Task GetSessions()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                IsBusy = true;

                using (var client = new HttpClient())
                {
                    //grab json from server
                    var json = await client.GetStringAsync("https://demo3143189.mockable.io/sessions");

                    //Deserialize json
                    var items = JsonConvert.DeserializeObject<List<Session>>(json);

                    //Load sessions into list
                    Sessions.Clear();
                    foreach (var item in items)
                        Sessions.Add(item);
                }
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
