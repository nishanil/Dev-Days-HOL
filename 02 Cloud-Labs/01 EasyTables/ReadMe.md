# Azure Easy Tables - Build a Quick backend!

In the previous module, we looked at building our first Xamarin.Forms app that grabbed the data from a RESTFul end point. Of course being able grab data from a RESTful end point is great, but what about a full backend? This is where Azure Mobile Apps comes in. Let's upgrade our application to use an Azure Mobile Apps backend.

![MyEvents](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/DevDaysHol-Cloud.png?token=AC9rtuaIsCDbDsXGjLGE_1BeMkvfF1zpks5X0phxwA%3D%3D)

## Overview

This lab will cover

* Creating Azure Mobile Apps using EasyTables
* Connecting App to Azure Mobile Apps backend
* Update Data to the Cloud

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

## Creating Azure Mobile Apps using EasyTables

Head to [http://portal.azure.com](http://portal.azure.com) and register for an account.

Once you are in the portal select the **+ New** button and search for **mobile apps** and you will see the results as shown below. Select **Mobile Apps Quickstart**

![Azure Mobile Apps Search](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/MobileApps-Search.png?token=AC9rtgKhTbXOczkQYxn2t9D-BmP2e2qvks5X0ppnwA%3D%3D)

The Quickstart blade will open, select **Create**

![Create quickstart](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/MobileApps-Creaate.png?token=AC9rtieOmloIyWX77Hgp-sjFz-zmmp1Aks5X0ptzwA%3D%3D)

This will open a settings blade with 4 settings:

**App name**

This is a unique name for the app that you will need when you configure the back end in your app. You will need to choose a globally-unique name; for example, you could try something like *yourlastnamedevdays*.

**Subscription**
Select a subscription or create a pay-as-you-go account (this service will not cost you anything)

**Resource Group**
Select *Create new* and call it **DevDaysHOL**

A resource group is a group of related services that can be easily deleted later.

**App Service plan/Location**
Click this field and select **Create New**, give it a unique name, select a location (typically you would choose a location close to your customers), and then select the F1 Free tier (Click `View All` button to list app pricing tiers and select the Free Tier):

![service plan](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/AppServicePlan.png?token=AC9rthXITHONt9uipHwM0b3XacA9v2qKks5X0q-WwA%3D%3D)

Finally check **Pin to dashboard** and click create:

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/AppService-PinToDash.png?token=AC9rtmVAFnHtecHhrxz2TUhRvzrk_SEHks5X0q87wA%3D%3D)

This will take about 3-5 minutes to setup.

Once Done, You will be able to navigate to it from your dashboard

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/AppServiceDash.png?token=AC9rtt-JZQF7BSIXBSYOFTq-qxHq21edks5X0p0NwA%3D%3D)

Under **Features** select **Easy Tables**.

It would have created a `TodoItem`, which you should see, but we can create a new table and upload a default set of data by selecting **Add from CSV** from the menu.

Ensure that you have downloaded this repo and have the **speaker.csv** * **sessions.csv** file that is in the **Mock-Data** folder.

Select the file **sessions.csv** and enter the table name as **Session**. It will automatically find the fields that we have listed. Then hit Start Upload.
![CSV-Upload](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/Sessions-Upload.png?token=AC9rtkYBqzWvSZXoEHXrYDeVKryod6tEks5X0p4BwA%3D%3D)

Repeat the above steps for **speakers.csv**. Be sure to name the table name as **Speaker**

Once your data is uploaded, you can see them here:

![Upload completed](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/Upload-Completion.png?token=AC9rtgBn-ybTMWPTpGEVxAgPoaxg9-myks5X0q89wA%3D%3D)

### Test your endpoint

Now that we've successfully uploaded the data to our Azure backend, let's test them out. You can copy the url of your Mobile Apps Service from the **Overview** menu as shown below.

![Copy URL](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/MobileApps-CopyURL.png?token=AC9rts91ChcC2n7h3mmCZtF_qZCPU0VUks5X0q_FwA%3D%3D)

Open a Web Browser, and hit the link in the following format:

**Speaker**
```
https://<yourappservicename>.azurewebsites.net/tables/Speaker?ZUMO-API-VERSION=2.0.0 
```
**Session**
```
https://<yourappservicename>.azurewebsites.net/tables/Session?ZUMO-API-VERSION=2.0.0 
```
Both should give you the the data in `json` format.

Now, let's edit the code to access this data into our `MyEvents` app.

## Connecting App to Azure Mobile Apps backend

To connect to the Azure Mobile Apps backend & to take advantage of offline data sync support, you need two Nuget Packages `Microsoft.Azure.Mobile.Client`  and `Microsoft.Azure.Mobile.Client.SQLiteStore` added to all your projects. To save time, this has been already added. You can verify them by right clicking your solution and hit **Manage Nuget Packages for solution...**

![Azure Nuget Packages](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/Azure-Nuget-Packages.png?token=AC9rtmcdDk5_XxPfM26Vts8NNRBto9O0ks5X0sP5wA%3D%3D)

Now, let's write some code.

Open the **MyEvents/Constants.cs** file and update the `ApplicationURL` to your Mobile App Service URL in the specified format. Please note: You don't need to append the table names. This is taken care automatically by the Azure Client SDKs.

```csharp
public static string ApplicationURL = @"https://<yourappservicename>.azurewebsites.net";
```

### Connect to Cloud

The code to connect to cloud is neatly abstracted away in the `AzureDataManager` class. Azure Client SDKs have a great support for offline data sync. Offline data sync is a client and server SDK feature of Azure Mobile Apps that makes it easy for developers to create apps that are functional without a network connection. When your app is in offline mode, users can still create and modify data, which will be saved to a local store. When the app is back online, it can synchronize local changes with your Azure Mobile App backend. The feature also includes support for detecting conflicts when the same record is changed on both the client and the backend. Conflicts can then be handled either on the server or the client. 
To take advantage of this feature, this demo uses `SyncTable` and initializes a local store. 

Here's the Initialize code for your reference

```csharp
private void Initialize()
{
    this.client = new MobileServiceClient(
        Constants.ApplicationURL);

    var store = new MobileServiceSQLiteStore("localstore.db");
    store.DefineTable<Session>();
    store.DefineTable<Speaker>();

    //Initializes the SyncContext using the default IMobileServiceSyncHandler.
    this.client.SyncContext.InitializeAsync(store);
}
```

To get data for `Sessions` and `Speakers`, the `AzureDataManager` has two methods `GetSessionsAsync()` and `GetSpeakersAsync()` which can be accessed by `IDataManager` implementation.

### Get Sessions

Open **MyEvents\ViewModels\SessionsViewModel.cs**, in the `GetSessions()` method, replace the code 
```csharp
var items = new List<Session>();
```
with the code to connect to cloud
```csharp
var items = await App.DataManager.GetSessionsAsync();
```

### Get Speakers

Open **MyEvents\ViewModels\SpeakersViewModel.cs**, in the `GetSpeakers()` method, replace the code 
```csharp
var items = new List<Speaker>();
```
with the code to connect to cloud
```csharp
var items = await App.DataManager.GetSpeakersAsync();
```
That's it. Now run the app and see the data being pulled in from your Azure Easy Tables backend.

## Update Data to the Cloud

Now that we have seen how to get the data from the cloud, let's look at how to update the data back to the cloud from the app. We will keep it simple - Let's edit the speaker title and save them back in the cloud. To do this, we need to 

* Edit the IDataManager and add a method `SaveSpeaker`
* Implement this method in `AzureDataManager` class
* Call this method appropriately from the UI

### IDataManager

Open **MyEvents\IDataManager.cs** and add this method

```csharp
Task SaveSpeakerAsync(Speaker speaker);
```
#### AzureDataManager

Open **MyEvents\Cloud\AzureDataManager** and implement the method
```csharp
public async Task SaveSpeakerAsync(Speaker speaker)
{
    await SaveItemAsync<Speaker>(speaker);
}
```

### SpeakersViewModel

Open **MyEvents\ViewModels\SpeakersViewModel** and add a method

```csharp
public async Task UpdateSpeaker(Speaker speaker)
{
    await App.DataManager.SaveSpeakerAsync(speaker);
    RefreshCommand.Execute(null);
}
```

Finally, this method needs to be called from the `SaveButton`'s event handler of the `SpeakerDetailPage`.

### SpeakerDetailPage

Open **MyEvents\Views\SpeakerDetailPage.xaml.cs** and in the `ButtonSave_Clicked` add this code:

```csharp
await vm.UpdateSpeaker(speaker);
```

That's it! Now, run the app.

## Run the App!
Run the app on all available platforms. Head to Speakers tab and click on any speaker, edit the title and save. See if the data changes. Also, hit the url again to see if the data has been updated back in the cloud.


## Wrapping Up!
We did great again! We looked at creating a no code backend with EasyTables and consumed the data in our app. Sessions and Speakers data were pulled from the cloud and updated the Speaker titles from the app. In the next module, we will look at creating a scalable backend using .NET APIs and authenticating users.
