# Azure Bobile Apps - Build a scalable .NET backend!

In the previous module, we looked at creating a no code backend with EasyTables and consumed the data in our app. Sessions and Speakers data were pulled from the cloud and updated the Speaker title from the app. In this module, we will look at creating a scalable backend using .NET APIs and authenticating users.

![MyEvents](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/DevDaysHol-Cloud.png?token=AC9rtuaIsCDbDsXGjLGE_1BeMkvfF1zpks5X0phxwA%3D%3D)

## Overview

This lab will cover

* Creating Azure Mobile Apps using .NET backend
* Connecting App to Azure Mobile Apps .NET backend
* Implementing a Session Feedback feature
    * Authenticating Users

In the **Start** folder of this repository ontains two folders `Server` and `Client`. For ease of use, the `Server` project has been already added to the `Client`'s solution. Navigate to the  `Client` folder and open the **MyEvents.sln** solution. This project has partially completed code which we will be completing as part of this excersise.

![Solution](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/netbackend-Solution.png?token=AC9rtni5Ih59NB5ALtxYQ4CfGxVMehSNks5X2HGjwA%3D%3D)

This solution now contains 5 projects

* MyEvents (Portable) - Portable Class Library that will have all shared code (model, views, and view models).
* MyEvents.Droid - Xamarin.Android application
* MyEvents.iOS - Xamarin.iOS application
* MyEvents.UWP - Windows 10 UWP application (can only be run from VS 2015 on Windows 10)
* `MyEvents.Server` - All Azure backend implementation

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

## Creating Azure Mobile Apps using .NET backend

Head to [http://portal.azure.com](http://portal.azure.com) and register for an account.

Once you are in the portal select the **+ New** button and search for **mobile apps** and you will see the results as shown below. Select **Mobile Apps**. Note: Previously, for EasyTables we used **Quickstart**, here: We are not.

![Azure Mobile Apps Search](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Search.png?token=AC9rtoRNfjRbJg6dvKw6vxzl1uOeq67Iks5X2HINwA%3D%3D)

The Quickstart blade will open, select **Create**

![Create quickstart](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Search-Select.png?token=AC9rtvb9gvbvgz0YuIXBSpwK_oo8vWS-ks5X2HIlwA%3D%3D)

This will open a settings blade with 4 settings:

**App name**

This is a unique name for the app that you will need when you configure the back end in your app. You will need to choose a globally-unique name; for example, you could try something like *yourlastnamedevdays*.

**Subscription**
Select a subscription or create a pay-as-you-go account (this service will not cost you anything)

**Resource Group**
You may reuse the existing resource group that was created in the previous labs or Select *Create new* and call it **DevDaysHOL-Net**

A resource group is a group of related services that can be easily deleted later.

**App Service plan/Location**
You may reuse the existing resource group that was created in the previous labs or Click this field and select **Create New**, give it a unique name, select a location (typically you would choose a location close to your customers), and then select the F1 Free tier:

![service plan](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/AppServicePlan.png?token=AC9rthXITHONt9uipHwM0b3XacA9v2qKks5X0q-WwA%3D%3D)

Finally check **Pin to dashboard** and click create:

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/AppService-PinToDash.png?token=AC9rtmVAFnHtecHhrxz2TUhRvzrk_SEHks5X0q87wA%3D%3D)

This will take about 3-5 minutes to setup.

Once Done, You will be able to navigate to it from your dashboard

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Dash.png?token=AC9rthj-sRRhtw35UvBhvxDJiTWb_XVVks5X1_oywA%3D%3D)

### Setup SQL Server Database
You can use SQLite or any other database of your choice on the cloud. However, for this lab we will setup a SQLServer as our backend database.

First, navigate to **Mobile/Data Connections** or search for **Data Connections** and **Add** a DataConnection

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-DataConnections.png?token=AC9rtqDHHcF5b7SHXMPEH4ZTtq8kCCKoks5X1_tLwA%3D%3D)

#### Create a Database

Choose the type as  **SQL Database** and create a **Create a new database**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-CreateDatabase-1.png?token=AC9rtj-cnYEL5P9RWKRuaiG56EFfKtc5ks5X1_utwA%3D%3D)

Be sure to select the **Free Tier**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-CreateDatabase-2.png?token=AC9rti7eLo9_FBSZ782kx2W9KZgEocWWks5X1_xdwA%3D%3D)

Next, select the **Target Server** and create a server (or use existing)

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-SQLServer-NewConnection.png?token=AC9rtp1KpExcOo3hZ2BRBSdAZMxc18tqks5X1_y_wA%3D%3D)

Provide a unique **Server Name**, **Server admin Login**, **Password** & **Location** (select the same location as your app service).

Once you've completed all the operations, you will be able to see a **ConnectionString** with the name `MS_TableConnectionString`. 

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-CreateDatabase-Final.png?token=AC9rtjqU9JP1hJJxLErnp67y0lSagmEpks5X1_3RwA%3D%3D)

That's it! The database is ready for consumption in our apps.

#### Deploy the .NET Backend code to cloud

Now from your Solution, right click on **MyEvents.Server** and click on **Publish**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Publish-0.png?token=AC9rto-q-9VxppuOXXhdbv7bx0abRoeiks5X2HJowA%3D%3D)

Select **Microsoft Azure App Service** and login to your account. Then select your **Subscription**, **ResourceGroup** and **App Service**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Publish-1.png?token=AC9rtojq3ll7m3rNHrAbkDiOk5BJIrKYks5X2HKIwA%3D%3D)

Now, valdiate your connection and hit **Publish**. Once it has successfully published, test the Endpoint.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Publish-3.png?token=AC9rtu92SByUVGTL2cBkd3AHoL_DMqZvks5X2HLDwA%3D%3D)

### Test the Endpoint

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

### How did the data get there?

Did you notice that the `Sessions` and `Speakers` data magically got into your newly created App Service? Let's decode and find the trick. Mobile Apps .NET backend uses **Entity Framework**'s Code-First approach and the Database intialization strategy has a code to seed the intitial data. 

Open **MyEvents.Server\App_Start\Startup.MobileApp.cs** file and check the `MyEventsDataInitializer` class.
Here's the code:

```csharp
var sesionsJson = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/sessions.json"));
var sessions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Session>>(sesionsJson);
foreach (Session session in sessions)
{
    session.Id = Guid.NewGuid().ToString();
    context.Set<Session>().Add(session);
}

var speakersJson = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/speakers.json"));
var speakers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Speaker>>(speakersJson);
foreach (Speaker speaker in speakers)
{
    speaker.Id = Guid.NewGuid().ToString();
    context.Set<Speaker>().Add(speaker);
}

try
{
    context.SaveChanges();
}
catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
{
    foreach (var validationErrors in dbEx.EntityValidationErrors)
    {
        foreach (var validationError in validationErrors.ValidationErrors)
        {
            System.Diagnostics.Trace.TraceInformation(
                    "Class: {0}, Property: {1}, Error: {2}",
                    validationErrors.Entry.Entity.GetType().FullName,
                    validationError.PropertyName,
                    validationError.ErrorMessage);
        }
    }
}
catch (Exception ex)
{
    System.Diagnostics.Trace.TraceError(ex.Message + "\n" + ex.InnerException.Message + "\n" + ex.StackTrace);
}
```

## Connecting App to Azure Mobile Apps .NET backend

To connect to the Azure Mobile Apps backend & to take advantage of offline data sync support, you need two Nuget Packages `Microsoft.Azure.Mobile.Client`  and `Microsoft.Azure.Mobile.Client.SQLiteStore` added to all your projects. To save time, this has been already added. You can verify them by right clicking your solution and hit **Manage Nuget Packages for solution...**

![Azure Nuget Packages](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/Azure-Nuget-Packages.png?token=AC9rtmcdDk5_XxPfM26Vts8NNRBto9O0ks5X0sP5wA%3D%3D)

Now, let's write some code.

Open the **MyEvents/Constants.cs** file and update the `ApplicationURL` to your Mobile App Service URL in the specified format. Please note: You don't need to append the table names. This is taken care automatically by the Azure Client SDKs.

```csharp
public static string ApplicationURL = @"https://<yourappservicename>.azurewebsites.net";
```

That's it! Now, run the app.

## Run the App!
Run the app on all available platforms. Head to Speakers tab and click on any speaker, edit the title and save. See if the data changes. Also, hit the url again to see if the data has been updated back in the cloud.

### Try This!

**_Update your new .NET Backend URL to the `Constants.cs` class of your previously executed EasyTables Lab's Sample and see if it works_**

## Implementing a Session Feedback feature
![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/DevDaysHol-Feedback.png?token=AC9rtuYC7oTtfJ0V3fozM-VlOKPYv7gqks5X2F54wA%3D%3D)

We learnt how to create a .NET backend, run it on cloud & consume them in the app. Let's learn this in detail by implementing a Session **Feedback** feature. To start with, we need to 

* Create a Model called `Feedback`
* Create a `FeedbackController` on the Server (to receive user Feedback)
* Authenticating & Authorizing Users (We need to know who's giving Feedback)
* UI to enter Session Feedback

### Create a Model

Head straight to **MyEvents.Server/DataObjects** folder and create a class `Feedback` and add the following code

```csharp
public class Feedback : EntityData
{

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("rating")]
    public int Rating { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("sessionId")]
    public string SessionId { get; set; }
}
```
Note that `Feedback` class inherits from `EntityData` class which is the base class for all the Models which are serialized when communicating with clients when using Entity Framework for accessing the backend store.

### Create a FeedbackController

Time to add some TableControllers! These will serve as the RESTful endpoints that our mobile app hits in order to receive data or other information from the backend. Right-click the **MyEvents.Server/Controllers** folder and select **Add->New Scaffolder Item...**.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-AddController.png?token=AC9rtomCpRWdaaO7id6H4yBpQ0x1dlSEks5X2HM7wA%3D%3D)

Select the Azure Mobile Apps Table Controller, as seen below, then click **Add**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-AddController-2.png?token=AC9rtowB8y3TZ0JiLeUk5abjt0ygpezYks5X2HODwA%3D%3D)

Set the Model Class to `Feedback` and the Data context class to `MyEventsContext` and controller name to `FeedbackController`. Click Add. This will "scaffold" out a new TableController for us and configure some additional settings.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-AddController-3.png?token=AC9rtottuH862KFcO5PAy1Tatl0EipEpks5X2HOHwA%3D%3D)

#### Authorize incoming Requests

Since the feedbacks given are user specific, we have to Authorize the `Users` to update or delete the data. To do this on server, we have to add an attribute `[Authorize]` to the web methods. This will restrict the server from unwanted usage of the API. Later in the lab, we will look at how to authorize users from the client code. For now, add `[Authorize]` attribute to your methods. Here's the full source code of `FeedbackController` class.

```csharp
public class FeedbackController : TableController<Feedback>
{
    protected override void Initialize(HttpControllerContext controllerContext)
    {
        base.Initialize(controllerContext);
        MyEventsContext context = new MyEventsContext();
        DomainManager = new EntityDomainManager<Feedback>(context, Request);
    }

    // GET tables/Feedback
    public IQueryable<Feedback> GetAllFeedback()
    {
        var claimsPrincipal = this.User as ClaimsPrincipal;
        string sid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

        // Only return data rows that belong to the current user.
        return Query().Where(t => t.UserId == sid);
    }

    [Authorize]
    // GET tables/Feedback/48D68C86-6EA6-4C25-AA33-223FC9A27959
    public SingleResult<Feedback> GetFeedback(string id)
    {
        return Lookup(id);
    }

    [Authorize]
    // PATCH tables/Feedback/48D68C86-6EA6-4C25-AA33-223FC9A27959
    public Task<Feedback> PatchFeedback(string id, Delta<Feedback> patch)
    {
            return UpdateAsync(id, patch);
    }

    [Authorize]
    // POST tables/Feedback
    public async Task<IHttpActionResult> PostFeedback(Feedback item)
    {
        Feedback current = await InsertAsync(item);
        return CreatedAtRoute("Tables", new { id = current.Id }, current);
    }

    [Authorize]
    // DELETE tables/Feedback/48D68C86-6EA6-4C25-AA33-223FC9A27959
    public Task DeleteFeedback(string id)
    {
            return DeleteAsync(id);
    }
}
```
Notice that the `GetAllFeedback()` method only returns all the feedbacks specific to the user.

### Authenticating Users

App Service Authentication / Authorization is a feature that provides a way for your application to sign in users so that you don't have to change code on the app backend. It provides an easy way to protect your application and work with per-user data. For details refer the [documentation](https://azure.microsoft.com/en-us/documentation/articles/app-service-authentication-overview/). We have already created our `FeedbackController` with necessary `[Authorize]` attribute. 

In this client apps, let's keep it simple and add some Facebook Authentication to our app. If you wish to use another service, please feel free to do so. Azure Mobile Apps have documentation on how to do it.

#### Registering your application with Facebook

* Open a Web Browser & navigate to the [Facebook Developers](https://developers.facebook.com/) website and sign-in with your Facebook account credentials.
* (Optional) If you have not already registered, click **Apps > Register as a Developer**, then accept the policy and follow the registration steps.
* On the top right corner, Under **My Apps** choose **Add a New App**
* Chosse **Website** as the platform

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/Facebook-1.png?token=AC9rtvI7wlCcvCzRyFRW2HZ4WFBBEG4eks5X2HPcwA%3D%3D)

* Skip and Create App ID

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/Facebook-2.png?token=AC9rtkSgTH-sXCtX1bhhsI9Cb8mFNzdYks5X2HQEwA%3D%3D)

* In Display Name, type a unique name for your app, type your Contact Email, choose a Category for your app, then click Create App ID and complete the security check. This takes you to the developer dashboard for your new Facebook app.
* Under "Facebook Login," click Get Started. Add your application's Redirect URI to Valid OAuth redirect URIs, then click Save Changes.
```
Your redirect URI is the URL of your application appended with the path, /.auth/login/facebook/callback. For example, https://<yourappservicename>.azurewebsites.net/.auth/login/facebook/callback. Make sure that you are using the HTTPS scheme.
```
* In the left-hand navigation, click Settings. On the App Secret field, click Show, provide your password if requested, then make a note of the values of App ID and App Secret. You use these in the next step to configure your application in Azure.

#### Configure your Portal with Facebook Authentication

Head to your dashboard, Select **Authentication/Authorization** and enable **App Service Authentication** and select **Allow request(no action)** - This setting will apply to all Controllers. This will help `Session` and `Speaker` data to flow in without Authentication.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Auth-1.png?token=AC9rtqTx0PcPT-gjYyQRyANcp7eDc2JMks5X2HRtwA%3D%3D)

Choose **Facebook** as the **Authentication Provider** and provide the **App ID** and **App Secret** which you copied from the previous step.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/02%20Cloud-Labs/screenshots/NetBackend-Auth-2.png?token=AC9rtjQAK0hz4adDIz78AxE1IV7TORilks5X2HRxwA%3D%3D)

### Create UI to enter Session Feedback

Finally, enable the UI to authenticate the users

Open the **MyEvents/Views/SessionDetailPage.xaml** and uncomment this code.

```xml
<local:FeedbackButton x:Name="FeedBackButton"
    Margin="0,10,0,0" 
    Text="Give Feedback"
Clicked="FeedBackButton_Clicked"/>
```

#### How does it work?

If you want to know the details on how the `Authentication` is implemented in the Client code, refer the `LoginCommand` in `LoginViewModel.cs` which invokes the `IAuthentication` implementation of the platform using `DependencyService`.

Here's the Android code for your reference:

```csharp
public class Authentication : IAuthentication
{
    public async Task<bool> Authenticate()
    {
        var user = await App.DataManager.CurrentClient.LoginAsync(Forms.Context,
            MobileServiceAuthenticationProvider.Facebook);
        if (user != null)
        {
            Settings.IsLoggedIn = true;
            Settings.UserId = user.UserId;
            Settings.AuthToken = user.MobileServiceAuthenticationToken;
        }
        return true;
    }
}
```

Note: If you prefer to use a different AuthenticationProvider, make sure you pass the appropriate `MobileServiceAuthenticationProvider` in the `LoginAsync` method.

## Run the App!
Run the app on all available platforms. On Sessions tab, click on any session, tap **Give Feedback** button, follow instructions and provide feedback.

## Wrapping Up!
We did great again! We looked at creating a scalable backend using .NET APIs and authenticating users. In the next lab, we will look at Mobile DevOps.