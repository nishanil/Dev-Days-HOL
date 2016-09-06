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
    public partial class FeedbackPage : ContentPage
    {

        public FeedbackPage(Session selectedSession) 
        {
            InitializeComponent();

            BindingContext = new FeedbackViewModel(Navigation, selectedSession);
        }


    }


}
