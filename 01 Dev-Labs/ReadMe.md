# Dev Days Hands On Lab - Let's build an App!

In this module, we will be building our first [Xamarin.Forms](http://xamarin.com/forms) app leveraging Visual Studio and targeting iOS, Android, and UWP platforms. The app that we will be building is called **MyEvents** that gets Session and Speaker information from a rest endpoint and displays them beautifully in a tabular UI.

![MyEvents](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/DevDaysHol.png?token=AC9rtpBmFfQKj9epUgPmYpJ8IBqxXsgkks5X0l_BwA%3D%3D)

## Overview

This lab will cover

* Creating Models and ViewModels
* Creating UI in XAML & DataBinding
* Page Navigations
* Platform Customizations
    * Device.OnPlatform
    * Custom Renderer
    * Dependency Service

In the **Start** folder of this repository is the partially completed **MyEvents** solution. Open the solution **MyEvents.sln**

![Solution](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/Solution-Overview.png?token=AC9rtsR3IlM7k__1H_Rdwfg34JDtMWeEks5X0Z4YwA%3D%3D)

This solution contains 4 projects

* MyEvents (Portable) - Portable Class Library that will have all shared code (model, views, and view models).
* MyEvents.Droid - Xamarin.Android application
* MyEvents.iOS - Xamarin.iOS application
* MyEvents.UWP - Windows 10 UWP application (can only be run from VS 2015 on Windows 10)

## Pre-requisites 

 Before executing the labs be sure to setup your macOS or Windows machines as mentioned in the Lab Setup guidelines. The steps & screenshots in this guide focuses on Visual Studio development in Windows. However, the same labs can be executed in Xamarin Studio on a macOS too. 

#### NuGet Restore

All projects have the required NuGet packages already installed, so there will be no need to install additional packages during the Hands on Lab. The first thing that we must do is restore all of the NuGet packages from the internet.

This can be done by **Right-clicking** on the **Solution** and clicking on **Restore NuGet packages...**

![Restore NuGets](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/Restore-NugetPackages.png?token=AC9rtpnblrgShE67VR5AsTAEqYvp6Hd9ks5X0Z5VwA%3D%3D)

Now **Right-click** on the **Solution** build & Run the app!


## Run the App!

Set the iOS, Android, or UWP (Windows/VS2015 only) as the startup project and start debugging.

![Startup project](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/StartProject-RunTheApp.png?token=AC9rtmrS-LmiPbZdHHyJ6b6paJqyBzo1ks5X0Z5-wA%3D%3D)

#### iOS
If you are on a PC then you will need to be connected to a macOS device with Xamarin installed to run and debug the app.

If connected, you will see a Green connection status. Select `iPhoneSimulator` as your target, and then select the Simulator to debug on.

![iOS Setup](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/iOS-RunTheApp.png?token=AC9rtm_7JJGO6hUfVpmY2JQrB31hgrLYks5X0Z6jwA%3D%3D)

#### Android

Simply set the MyEvents.Droid as the startup project and select a simulator to run on. The first compile may take some additional time as Support Packages are downloaded, so please be patient. 

If you run into an issue building the project with an error such as:

**aapt.exe exited with code** or **Unsupported major.minor version 52** then your Java JDK may not be setup correctly, or you have newer build tools installed then what is supported. See this technical bulletin for support: https://releases.xamarin.com/technical-bulletin-android-sdk-build-tools-24/

Additionally, see James' blog for visual reference: http://motzcod.es/post/149717060272/fix-for-unsupported-majorminor-version-520

![Android Setup](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/Android-RunTheApp.png?token=AC9rtkpzx2lUuuUJd8Ra-WEVCSCnRSGLks5X0Z7BwA%3D%3D)

#### Windows 10

Ensure that you have the SQLite extension installed for UWP apps:

Go to **Tools->Extensions & Updates**

Under Online search for *SQLite* and ensure that you have SQlite for Univeral Windows Platform installed (current version 3.14.1)

If there is a new version then install it, remove the SQLite for Universal Windows Platform from the References in the UWP app. Then add a new Reference and under **Universal Windows -> Extensions** you will see SQlite for Universal Windows Platform. Add that in and you will be good to go.

![Sqlite](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/SQLite.png?token=AC9rtp6J3FsI5nv8KJWgzrTWn-0Du1IHks5X0Z8vwA%3D%3D)

Simply set the MyEvents.UWP as the startup project and select debug to **Local Machine**.

![UWP Setup](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/UWP-RunTheApp.png?token=AC9rtiiGnxLyq5C69pN2j3Yc34prl7LWks5X0Z8ZwA%3D%3D)

## Result

When you run the app, you will see that it successfully runs with two tabs **Sessions** and **About** page. As part of this exercise, you will be building the **Speakers** page and attaching it to the main `TabbedPage`. 

## Creating Models and ViewModels

## Model

The data for speakers will be pulled down from a rest endpoint which we will look at it in the **ViewModel** section. The class **Speaker** is the model that is used to hold the data. Open the **MyEvents/Models/Speaker.cs** file and add the following properties inside of the **Speaker** class:

```csharp
[JsonProperty("name")]
public string Name { get; set; }

[JsonProperty("description")]
public string Description { get; set; }

[JsonProperty("image")]
public string Image { get; set; }

[JsonProperty("title")]
public string Title { get; set; }

[JsonProperty("company")]
public string Company { get; set; }

[JsonProperty("website")]
public string Website { get; set; }

[JsonProperty("blog")]
public string Blog { get; set; }

[JsonProperty("twitter")]
public string Twitter { get; set; }

[JsonProperty("email")]
public string Email { get; set; }

[JsonProperty("avatar")]
public string Avatar { get; set; }

[JsonProperty("webiste")]
public string Webiste { get; set; }

[JsonProperty("titile")]
public string Titile { get; set; }

[JsonProperty("biography")]
public string Biography { get; set; }

```
### ViewModel

The **SpeakersViewModel.cs** will provide all of the functionality for the Xamarin.Forms view to display data. It will consist of a list of speakers and a method that can be called to get the speakers from the server. It will also contain a boolean flag that will indicate if we are getting data in a background task. Open the **MyEvents/ViewModels/SpeakersViewModel.cs** file to work on.

Use an **ObservableCollection<Speaker>** for storing the speakers data inside the ViewModel. **ObservableCollection** is used because it has built-in support for **CollectionChanged** event that the view subscribes to and automatically updates when the data is added or removed.

Above the constructor of the SpeakersViewModel class definition, declare an auto-property:

```csharp
public ObservableCollection<Speaker> Speakers { get; set; }
```

Inside of the constructor, create a new instance of the `ObservableCollection`:

```csharp
public SpeakersViewModel()
{
    Speakers = new ObservableCollection<Speaker>();
    Title = "Speakers";
}
```
##### GetSpeakers Method

Create a method named **GetSpeakers** which will retrieve the speaker data from the internet. We will first implement this with a simple HTTP request, but later, in the next module, we will update it to grab and sync the data from Azure.

Create a method called **GetSpeakers** which is of type *async Task* (it is a Task because it is using Async methods):

```csharp
private async Task GetSpeakers()
{

}
```
The following code will be written INSIDE of this method:

First is to check if we are already grabbing data:

```csharp
private async Task GetSpeakers()
{
    if(IsBusy)
        return;
}
```

Next we will create some scaffolding for try/catch/finally blocks:

```csharp
private async Task GetSpeakers()
{
    if (IsBusy)
        return;

    Exception error = null;
    try
    {
        IsBusy = true;

    }
    catch (Exception ex)
    {
        error = ex;
    }
    finally
    {
       IsBusy = false;
    }

}
```

Notice, that the *IsBusy* is set to true and then false when we start to call to the server and when we finish.

Now, we will use *HttpClient* to grab the json from the server inside of the **try** block.

 ```csharp
using(var client = new HttpClient())
{
    //grab json from server
    var json = await client.GetStringAsync("https://demo4404797.mockable.io/speakers");
} 
```

Still inside of the **using**, we will Deserialize the json and turn it into a list of Speakers with Json.NET:

```csharp
var items = JsonConvert.DeserializeObject<List<Speaker>>(json);
```

Still inside of the **using**, we will clear the speakers and then load them into the ObservableCollection:

```csharp
Speakers.Clear();
foreach (var item in items)
    Speakers.Add(item);
```
If anything goes wrong the **catch** will save the exception and AFTER the finally block we can pop up an alert:

```csharp
if (error != null)
    await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
```

The completed code should look like:

```csharp
private async Task GetSpeakers()
{
    if (IsBusy)
        return;

    Exception error = null;
    try
    {
        IsBusy = true;
        
        using(var client = new HttpClient())
        {
            //grab json from server
            var json = await client.GetStringAsync("https://demo4404797.mockable.io/speakers");
            
            //Deserialize json
            var items = JsonConvert.DeserializeObject<List<Speaker>>(json);
            
            //Load speakers into list
            Speakers.Clear();
            foreach (var item in items)
                Speakers.Add(item);
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
```

Our main method for getting data is now complete!

##### GetSpeakers Command

Instead of invoking this method directly, we will expose it with a **Command**. A Command has an interface that knows what method to invoke and has an optional way of describing if the Command is enabled.

Create a new Command called **GetSpeakersCommand**:

```csharp
public Command GetSpeakersCommand { get; set; }
```

Inside of the `SpeakersViewModel` constructor, create the `GetSpeakersCommand` and pass it two methods: one to invoke when the command is executed and another that determines whether the command is enabled. Both methods can be implemented as lambda expressions as shown below:

```csharp
GetSpeakersCommand = new Command(
                async () => await GetSpeakers(),
                () => !IsBusy);
```

## Creating UI in XAML & DataBinding

It is now finally time to build out our first Xamarin.Forms user interface in the **Views/SpeakersPage.xaml**

### SpeakersPage.xaml

In this page we will add an `AbsoluteLayout` to the page and add controls to it. Between the `ContentPage` tags add the following:

```xml
<AbsoluteLayout HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">

</AbsoluteLayout>
```

We will use a ListView that binds to the Speakers collection to display all of the items. We can use a special property called *x:Name=""* to name any control:

```xml
<StackLayout
      AbsoluteLayout.LayoutFlags="All"
      AbsoluteLayout.LayoutBounds="0,0,1,1">
    <ListView x:Name="ListViewSpeakers"
                ItemsSource="{Binding Speakers}">
            <!--Add ItemTemplate Here-->
    </ListView>
</StackLayout>
```

We still need to describe what each item looks like, and to do so, we can use an ItemTemplate that has a DataTemplate with a specific View inside of it. Xamarin.Forms contains a few default Cells that we can use, and we will use the **ImageCell** that has an image and two rows of text.

Replace <!--Add ItemTemplate Here--> with: 

```xml
<ListView.ItemTemplate>
    <DataTemplate>
        <ImageCell Text="{Binding Name}"
                    Detail="{Binding Title}"
                    ImageSource="{Binding Avatar}"/>
    </DataTemplate>
</ListView.ItemTemplate>
```
Under the `ListView` we can display a loading bar when we are gathering data from the server. We can use an ActivityIndicator to do this and bind to the IsBusy property we created:

```xml
<StackLayout IsVisible="{Binding IsBusy}"
                     Padding="32"
                     AbsoluteLayout.LayoutFlags="PositionProportional"
                     AbsoluteLayout.LayoutBounds="0.5,0.5,-1,-1">
      <ActivityIndicator IsRunning="{Binding IsBusy}"/>
    </StackLayout>
```

We can take two strategies to initiate the retrieval of data from the server

* Provide a Sync Button (load data on demand) or
* Load them when the view appears

#### Sync Button
Add a `ToolBarButton` to the page and bind the `Command` property to the `GetSpeakersCommand`. The command takes the place of a clicked handler and will be executed whenever the user taps the button. Add the below code within the `ContentPage` tags.

```xml
  <ContentPage.ToolbarItems>
    <ToolbarItem Text="Sync" Command="{Binding GetSpeakersCommand}" />
  </ContentPage.ToolbarItems>
```

#### Load data in `OnAppearing()` method

Open **SpeakersPage.xaml.cs** and add the following code inside `OnAppearing()` method below the line `base.OnAppearing()`

```csharp
if (ViewModel.Speakers.Count == 0)
     ViewModel.GetSpeakersCommand.Execute(null);
```

The above code ensures that the command is executed only once. This will help in not hitting the server each time the View Appears on screen.

### DataBinding
Finally, for the view to bind to data we need to attach the **SpeakersViewModel** to the `BindingContext` of the View. Add this code below the `<ContentPage.ToolbarItems></ContentPage.ToolbarItems>`

```xml
  <ContentPage.BindingContext>
    <vm:SpeakersViewModel/>
  </ContentPage.BindingContext>
```

Xamarin.Forms will automatically download, cache, and display the image from the server.

### Add the SpeakersPage to the MainPage

Since our MainPage is a **TabbedPage**, we will add the SpeakersPage as a Child of it. Open **App.Xaml.cs** and within the **constructor** add this code:

```csharp
var speakersPage = new NavigationPage(new SpeakersPage()) { Title = "Speakers" };
```

And, add **speakersPage** to the **MainPage**

```csharp
mainPage.Children.Add(speakersPage);
```

Final code should look like this:

```csharp
public App()
{
    InitializeComponent();

    var mainPage = new TabbedPage();
    var sessionsPage = new NavigationPage(new SessionsPage()) { Title = "Sessions" };
    var speakersPage = new NavigationPage(new SpeakersPage()) { Title = "Speakers" };
    var aboutPage = new NavigationPage(new AboutPage()) { Title = "About" };

    mainPage.Children.Add(sessionsPage);
    mainPage.Children.Add(speakersPage);
    mainPage.Children.Add(aboutPage);


    MainPage = mainPage;
}
```

Notice that we used the `NavigationPage` there. We will learn more about it in the next section.

### Run the App!

Hey! You just created the SpeakersPage, pulled the data from the internet and bound to the view.

## Page Navigations

Xamarin.Forms provides a number of different page navigation experiences, depending upon the Page type being used. We used the `TabbedPage` in our **MainPage** for an easy navigation. Now, we will build a master-Detail experience to our `Session` page i.e. when users click on an item in the Sessions List, we will navigate them to a detailed page. We will use Hierarchical Navigation using the `NavigationPage` class which provides a hierarchical navigation experience where the user is able to navigate through pages, forwards and backwards, as desired. 

The first page added to a navigation stack is referred to as the root page of the application, which is already done in the **App.xaml.cs** file.

```csharp
var sessionsPage = new NavigationPage(new SessionsPage()) { Title = "Sessions" };
```

Open **SessionsPage.xaml** and add an `ItemSelected` event handler to the ListView **SessionsListView**. 

```xml
ItemSelected="OnItemSelected"
``` 

Finally, add the EventHandlder to the code behind **SessionsPage.xaml.cs**

```csharp
async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
{
    var item = args.SelectedItem as Session;
    if (item == null)
        return;

    await Navigation.PushAsync(new SessionDetailPage() { BindingContext = new SessionDetailViewModel(item)});
    
    // Manually deselect item
    SessionsListView.SelectedItem = null;
}
```

 It is necessary to invoke the `PushAsync` method on the Navigation property of the current page, as demonstrated above. This causes the `SessionDetailPage` instance to be pushed onto the navigation stack, where it becomes the active page. 
 Similarly, the active page can be popped  method from the navigation stack by pressing the Back button on the device, regardless of whether this is a physical button on the device or an on-screen button. Alternatively, you can use the `PopAsync` method in code.

### Run the App!

Run the app on all available platforms and notice the differences. 

* On iOS, a navigation bar is present at the top and a Back button that returns to the previous page.
* On Android, a navigation bar is present at the top of the page that displays an icon, and a Back button that returns to the previous page. Android's on-screen button also works exactly as expected.
* On Windows Phone, a navigation bar is present at the top of the page. Windows Phone lacks the Back button on the navigation bar because an on-screen Back button is present at the bottom of the screen.

## Platform Customizations

Until now, we wrote every line of code in Portable Class Library (MyEvents) that helped us share 100% of code on all platforms. Xamarin.Forms is extensible and lets you incorporate platform-specific features. You can use the `Device` class to create platform-specific behavior in shared code and the user interface (including using XAML) for simple customizations. If you have complex customizations you can use `DependencyService` to invoke a platform code from your shared code. `CustomRenderers` can be used for small styling changes or sophisticated platform-specific layout and behavior customization.

### Device.OnPlatform

In iOS, Tabs can display icons along with the Title. In our app, since this requriement is specific to iOS, we will use the `Device.OnPlatform` method to set the icons within the shared code.

Add this code in the constructor of the **App.xaml.cs**

```chsarp
Device.OnPlatform(iOS: () => {
    sessionsPage.Icon = "tab_feed.png";
    speakersPage.Icon = "tab_person.png";
    aboutPage.Icon = "tab_about.png";

});
```

Resulting page will display the icons in the Tabs as shown below.

![iOS-Tabs](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/iOS-Tab-icons.png?token=AC9rtoWVK4eOTAWDV69qFcZyy9veMPJKks5X0mezwA%3D%3D)

### DependencyService
We will use the native Text to Speech APIs on every platform to read out the text for the users. Since each platform provides its own APIs for **Text to Speech**, we will use the `DependencyService` to invoke the platform implementation from shared code.

First, create the interface `ITextToSpeech` and define `Speak()` method in it inside the PCL project. Open **MyEvents\ITextToSpeech.cs** file andd add this code

```csharp
public interface ITextToSpeech
{
    void Speak(string text);
}
```
Open the **TextToSpeech.cs** in the Android project **MyEvents.Droid** and add this code

```csharp
public class TextToSpeechImplementation : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
{
    TextToSpeech speaker;
    string toSpeak;

    public TextToSpeechImplementation() { }

    public void Speak(string text)
    {
        var ctx = Forms.Context; // useful for many Android SDK features
        toSpeak = text;
        if (speaker == null)
        {
            speaker = new TextToSpeech(ctx, this);
        }
        else
        {
            var p = new Dictionary<string, string>();
            speaker.Speak(toSpeak, QueueMode.Flush, p);
        }
    }

    #region IOnInitListener implementation
    public void OnInit(OperationResult status)
    {
        if (status.Equals(OperationResult.Success))
        {
            var p = new Dictionary<string, string>();
            speaker.Speak(toSpeak, QueueMode.Flush, p);
        }
    }
    #endregion
}
```
Uncomment the `assembly` attribute (above the  namespace) in the same file.

```csharp
[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechImplementation))]
```
This attribute registers the class as an implementation of the ITextToSpeech interface, which means that `DependencyService.Get<ITextToSpeech>()` can be used in the shared code to create an instance of it.

For convinience, the `TextToSpeech` implementation for iOS and UWP have already been added to their respective projects. You can refer them for implementation details. For iOS, open **MyEvents.iOS/TextToSpeech.cs** and **MyEvents.UWP/TextToSpeech.cs** for UWP.

Finally, Call the platform implementations in the Shared Code using DependencyService. Open **MyEvents\SessionDetailViewModel** and in the **SpeakCommand** Initialization add this code.

```csharp
DependencyService.Get<ITextToSpeech>().Speak($"Session {SessionName} presented by {SpeakerName} is on {Time}");
```

### Run the App!

Run the app on all available platforms and hit the speak button.

### CustomRenderers

`CustomRenderers` can be used for small styling changes or sophisticated platform-specific layout. In this app, we will create a custom button called `SpeakButton` and add a Custom Renderer in Android project to display an image alongside the button text.

Open the ***MyEvents/Controls/SpeakButton.cs* and notice the empty class that derives from the existing `Button` class. This is a placeholder class to add `BindableProperty`s for your control and extend them as you wish to. We will keep it simple for this lab, so leave it empty.

Open the Android project's platform implementation **MyEvents.Droid/Renderers/SpeakButtonRenderer.cs** and override the `OnElementChanged()` method to add an image to the button. Here's the code

```csharp
protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
{
    base.OnElementChanged(e);

    if (Control != null)
    {
        Control.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.speakerphone, 0, 0);
        
    }
}
```
Now, open the **MyEvents\Views\SessionDetailPage.xaml** and replace the existing button for **Speak** with the **SpeakButton** code below.

```xml
<local:SpeakButton
        Margin="0,10,0,0"
        Text="Speak" Command="{Binding SpeakCommand}" />
```
Build and run the Android project to see the button change. iOS and Android projects do not have custom implementations currently so they fall back to displaying regular buttons of the platform. Feel free to modify those custom renderers and add your creativity to them.

## Wrapping Up!


We did great! Our first Xamarin.Forms app with beautiful UI pulling the data from a rest end point with few platform customizations came out so well. In the next module, we will look at building a scalable backend in Azure and consuming them in an app.
