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
        public FeedbackPage()
        {
            InitializeComponent();
        }

        async void FinishButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }


}
