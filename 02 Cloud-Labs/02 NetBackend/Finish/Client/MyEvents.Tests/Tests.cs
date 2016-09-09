using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace MyEvents.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;
        const string UserName = "";
        const string Password = "";


        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .Android
                .StartApp();
        }

        [Test]
        public void WhenAppStartsLoadTheSessions()
        {
            app.WaitForElement(a => a.Marked("SessionsList"), timeout: TimeSpan.FromSeconds(45));
            app.ScrollToVerticalEnd(a => a.Marked("SessionsList"));
        }

        [Test]
        public void GivenASessionsListWhenITapOnTheSessionThenShowDetails()
        {
            app.WaitForElement("SessionCell");
            app.Tap("SessionCell");
            app.WaitForElement("Abstract");
        }

        [Test]
        public void GivenSessionsDetailPageWhenTappedOnBackThenSessionsListShowShouldShow()
        {
            app.WaitForElement("SessionCell");
            app.Tap("SessionCell");

            app.Screenshot("Session Details");
            app.Back();

            app.WaitForElement("SessionCell");
           
        }

        [Test]
        public void GivenASessionWhenWhenTappedOnFeedbackThenAskToLogin()
        {
            LoginToFeedback();

            app.WaitForElement("Authenticate");
            app.Screenshot("Facebook login page");
        }

        [Test]
        public void WhenPresentedLoginPageThenLoginWithFacebook()
        {
            LoginToFeedback();

            app.EnterText(c => c.WebView().Css("input").Index(0), UserName);
            app.Back();
            app.EnterText(c => c.WebView().Css("input").Index(1), Password);
            app.Back();
            app.Tap(c => c.WebView().Css("Button"));

            var continueButtone = app.Query(c => c.Css("Button#u_0_a"));

            if (continueButtone.Length > 0)
            {
                app.Query(c => c.Css("Button#u_0_a"));
                app.Screenshot("Continue with authentication");
            }

            app.WaitForElement("FEEDBACK", timeout:TimeSpan.FromSeconds(45));
        }

        [Test]
        public void WhenOnFeedbackPageLeaveFeedback()
        {
            LoginToFeedback();

            app.EnterText(c => c.WebView().Css("input").Index(0), UserName);
            app.Back();
            app.EnterText(c => c.WebView().Css("input").Index(1), Password);
            app.Back();
            app.Tap(c => c.WebView().Css("Button"));

            var continueButtone = app.Query(c => c.Css("Button#u_0_a"));

            if (continueButtone.Length > 0)
            {
                app.Query(c => c.Css("Button#u_0_a"));
            }

            app.WaitForElement("FEEDBACK");
            
            app.Tap(c => c.Class("FormsImageView").Index(4));
            app.EnterText("FeedbackText", $"Meh! on {DateTime.Now}");

            app.Screenshot("Entered the feedback");

            app.Back();
            app.Tap("Send");

            app.WaitForElement(a => a.Button("Feedback"));
        }

        [Test]
        public void LoadSpeakersList()
        {  
            app.Tap("Speakers");
            app.ScrollToVerticalEnd("SpeakerList");
        }

        [Test]
        public void GivenASpeakersOnSelectingOneDiscloseTheDetails()
        {
            app.Tap("Speakers");
            app.Tap(c => c.Class("TextView"));
            app.WaitForElement("SpeakerImage");
        }

        [Test]
        public void WhenSelectedAboutTabThenLoadAboutPage()
        {
            app.Tap("About");
            app.WaitForElement("Learn More");
        }

        void LoginToFeedback()
        {
            app.WaitForElement("SessionCell");
            app.Tap("SessionCell");

            app.ScrollDownTo(c => c.Marked("Feedback"));
            app.Tap("Feedback");
            app.Tap("Login");
            app.Screenshot("Login page");
        }
    }
}

