using MyEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyEvents.ViewModels
{
    public class SessionDetailViewModel : ViewModelBase
    {

        public string SessionName { get { return SelectedSession?.Title; } }

        public string SpeakerName { get { return SelectedSession?.Presenter; } }

        public string Abstract { get { return SelectedSession?.Abstract; } }

        public string Time { get { return formattedTime; } }

        private Session selectedSession;

        public Session SelectedSession
        {
            get { return selectedSession; }
            set { SetProperty(ref selectedSession, value); }
        }

        public ICommand SpeakCommand { get; set; }

        string formattedTime = null;
        public SessionDetailViewModel(Session selectedSession = null)
        {
            SelectedSession = selectedSession;

            var start = SelectedSession?.Start;
            var startString = start?.ToLocalTime().ToString("t");
            var end = SelectedSession?.End;
            var endString = end?.ToLocalTime().ToString("t");
            var day = start?.ToString("M");
            formattedTime = $"{day}, {startString}–{endString}";

            SpeakCommand = new Command(()=>
            {
                DependencyService.Get<ITextToSpeech>().Speak($"Session {SessionName} presented by {SpeakerName} is on {Time}");
            });
        }
    }
}
