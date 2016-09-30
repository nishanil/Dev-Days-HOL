using MyEvents.Helpers;
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
    public class FeedbackViewModel : ViewModelBase
    {
        public INavigation Navigation { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        private Session currentSession;

        public Session CurrentSession
        {
            get { return currentSession; }
            set { SetProperty(ref currentSession, value); }
        }

        private string feedbackText;

        public string FeedbackText
        {
            get { return feedbackText; }
            set { SetProperty(ref feedbackText, value); }
        }


        public FeedbackViewModel(INavigation navigation, Session currentSession)
        {
            Navigation = navigation;
            CurrentSession = currentSession;

            CloseCommand = new Command(async () => {
               
                    await Navigation?.PopModalAsync(); });

            SaveCommand = new Command(async (rating) =>
            {

                var r = (int)rating;
                if ( r> 0 && !string.IsNullOrEmpty(FeedbackText))
                {
                    var feedback = new Feedback { Rating = r, SessionId = currentSession.Id, Text = FeedbackText, UserId = Settings.UserId };
                    try
                    {
                        IsBusy = true;

                        await App.DataManager.SaveFeedbackAsync(feedback);
                    }
                    finally { IsBusy = false; }
                }
                await Navigation?.PopModalAsync();

            });
        }
    }
}
