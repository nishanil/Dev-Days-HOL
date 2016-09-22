## Dev Days HOL - DevOps

 In the previous module, we looked at creating a .NET APIs, creating database in the cloud and authenticating users. Continuous Integration (CI) is one of the key practices of DevOps. Visual Studio Team Services simplifies continuous integration for your applications for all platforms and all languages. VSTS let's you create and manage build processes that automatically compile and test your applications in the cloud or on premises, either on demand or as part of an automated continuous integration strategy. The CI build can also package the code so that it is ready for continuous deployment to test, QA or even Production environments. This module covers how to do that for the cross platform mobile applications. We will pick up the same code that we completed in the previous module, create repository, commit code, create build definitions & deploy to beta testers using HockeyApp.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/AnyADeveloperAnyLanguage1.png) 

## Overview

This lab will cover

* Create repo & commit code
* Create CI build definitions for Android & iOS 
* Trigger a build by committing code to the repo
* Deploy Apps to Beta testers to HockeyApp

We will create an account on [VisualStudio.com](http://VisualStudio.com), create a repo and commit codes. The **Code** folder within this module has the complete source that needs to be committed to VSTS. **Please download this source code and keep it ready to copy into another location.**

## Create repo & commit code

### Sign up for a VSTS Account

Before continuing, you must sign up for a free VSTS account.

1. Sign into [VisualStudio.com](http://VisualStudio.com). Enter your Microsoft Account credentials.

1. Create a new VSTS account. Enter the following information.
    * Give valid name
    * Choose `Git` for managing code
    * Host your project close to your location.
 
    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/CreateAccount.png)

1. Once the account is created, you will see the VSTS landing page. Choose **Colloborate on Code** to add the earlier downloaded code to your repo.

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Dashboard.png)

1. Now click on **Clone in Visual Studio** . This step will open up your Visual Studio locally

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Clone-in-VS-1.png)

1. Enter your Microsoft credentials when prompted

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Clone-in-VS.png)

1. Provide the local **path** for your repository and hit **Clone** in the **Team Explorer** window inside Visual Studio

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Clone-in-VS-2.png)

1. Open Windows Explorer and copy all the content of the **Codes** folder that your downloaded to your local repository

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Copy-Code-From-Git.png)

1. Go back to Visual Studio, in the **Team Explorer**, write your commit message and commit all the changes. Be sure to select `Commit All and Sync`

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Commit-And-Push-1.png)

1. Once committed, you should see this screen

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Commit-And-Push.png)

1. Head back to your VisualStudio.com dashboard and you will see your code has been pushed successfully.

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Dashboard-Code-Pushed.png)


## Create CI build definition for Android

Now, let's create some build definitions to compile the solution and package the Android application.

1. Head over to **Build** tab under your VisualStudio.com dashboard an hit `New`

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Create-Build-Definition.png) 

1. Choose `Xamarin.Android` 
    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Definition-1.png)

1. In the next screen, 
    * Select the `DevDaysLabs` repository that you created earlier
    * Make sure to check the option **Continous Integration **
    * Select the Defaut Agent as **Hosted**

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Definition-2.png)

1. The resulting screen will have all the `Build Steps` automatically generated for you. Let's `Disable` a few steps that we are not doing it as part of this lab. `Disable` the following steps (all the `red` ones):

    * Activate Xamarin License
    * Test
    * Deactivate Xamarin License

    > To Disable, **Right Click** on the build step and select **Disable Selected Task**
 
    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Definition-3-Deavtivate.png)

1. Let's `Configure the Nuget restore Task`
    Provide the solution path in the field below

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Definition--Nuget.png)

1.  Next, click on the `Build Xamarin.Android Project Task` and provide the path to the Android project file.

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Definition-4-SelectDroid-1.png)
    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Definition-4-SelectDroid.png)

1. `Save` the build definition & give an approprite name. For e.g. **Xamarin.Android Release**

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Definition-5-Save.png)

1. Hit the `Queue a New Build` option and watch the build complete

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Queue-Build.png)

1. When the build completes, you should see everything `Green`

    ![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Build-Complete.png)

## Trigger a build by committing code to the repo

> This is a task for you! Change some code in your repo. Open Visual Studio and change a few lines of code and `Commit and Sync`. Get back to VSTS dashboard and see if a build is automatically triggered.

## Deploy Apps to Beta testers through HockeyApp

 HockeyApp is a service that enables mobile app developers to easily distribute apps to testers and get feedback from them. 
 
 First things First, head over to [HockeyApp](https://www.hockeyapp.net/) and [sign up for free](https://rink.hockeyapp.net/users/sign_up).  You can use your Microsoft Account there.

 We will get back to this dashboard to grab the **API Token** later.
 
Let's install the HockeyApp VSTS extension, which will add new build steps to easily deploy to HockeyApp. [Navigate to this link](https://marketplace.visualstudio.com/items?itemName=ms.hockeyapp) and install the HockeyApp extension to your account. 

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-Install.png)

Under our projects services in **settings**, we can also connect to HockeyApp to specify our API Token. Click on Settings 

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-Service-1.png)

Under **Services** choose, create a **New Service Endpoint** and choose **HockeyApp**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-Service-2.png)

Now get back to your **HockeyApp Dashboard** anc create a new API Token from **Account Settings**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-Service-3.png)

Copy the **API Token** and add it the **Add new HockeyApp Connection** screen

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-Service-4.png)


### Deploy Android Apps

In this lab, we will focus on delivering Android apps to beta testers so let's Edit build definition for Android.

Back in our build steps we can add the HockeyApp step under the Deploy category:

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Add-HockeyApp.png)

Finally, provide the hockey app connection and the path of the **.apk**.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Deploy-HockeyApp.png)

#### Queue a new build

When the build completes you should see the deploy step **succeed**.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Deploy-HockeyApp-Build-Sucess.png)

Go to HockeyApp and you should see your app being listed there.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/HockeyApp-FinalDash.png)

#### Versioning Assemblies

Android **.apks** are versioned based on the values set inside the **AndroidManifest.xml** file. 

Two settings are available, and you should always define values for both of them:

* `versionCode` — An integer used as an internal version number. 
* `versionName` — A string used as the version number shown to users.

 When releasing packages, it is best practice to version the package to match the build number so that it is easy to track. Since VSTS takes care of builds, its best to add a **Build Step** to update the manifest file with the running build number. To do do this, you need to install an extension from the VSTS marketplace 
 first.

##### Format Build number

Since we rely on the build number to be the verion number for our apps, let's edit them to an appropriate format. We will later use a regex pattern to strip out the last digits to append to our version number.

Go to **General** tab and edit the build number format to `1.0.0$(rev:.r)` and **Save**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Version-Assemblies-4.png)

##### Installing an Extension from the VSTS Marketplace

>Note: You can always do this using a PowereShell script or a bat script. However, it's always a good practice to search for one in the marketplace.

In the upper right corner of your VSTS dashboard, click the Basket icon and select **Browse Marketplace**.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Version-Assemblies-0.png)

Search for `Version Assemblies` and install the `Colin's ALM Corner Build Tasks`

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Version-Assemblies.png)

**Add VersionName Step**

Now, click **Add Build Step** and add the `Version Assemblies` task and place it right before the build task.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Version-Assemblies-2.png)

* Source Path: (browse to the `properties` folder right inside the droid project)
* File Pattern: `AndroidManifest.xml`
* Build Regex Pattern: `(?:\d+\.\d+\.\d+\.)(\d+)`
* In the Advanced Section
    * Build Regex Group Index: `0`
    * Regex Replace Pattern: `versionName="\d+\.\d+\.\d+`
    * Prefix for Replacements: `versionName="`
* Edit build step name as `Update Version Name` for understanding

**Add VersionCode Step**

Click **Add Build Step** again and add the same `Version Assemblies` task and place it right after the `Update Version Name` task. 

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Version-Assemblies-3.png)

* Source Path: (browse to the `properties` folder right inside the droid project)
* File Pattern: `AndroidManifest.xml`
* Build Regex Pattern: `(?:\d+\.\d+\.\d+\.)(\d+)`
* In the Advanced Section
    * Build Regex Group Index: `1`
    * Regex Replace Pattern: `versionCode="\d+`
    * Prefix for Replacements: `versionCode="`
* Edit build step name as `Update Version Code` for understanding

Save the steps & Queue a new build.

That's it. When the build completes, head over to HockeyApp and check if the `versionName` and `versionCode` are updated accordingly.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Version-Assemblies-5.png)


#### Test them on Device

To test them on real devices, you need to install HockeyApp onto them. The steps here list how to install hockey app on an emulator, however, we recommend that you use your Android phone.

**Register Device**

In the Hockey App dashboard, go to Account Settings and Click on the **Devices** and click on **Guided Process**

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-RegisterDevice-1.png)

Copy the URL and paste it on your Device

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-RegisterDevice-2.png)

Once you login, Download the HockeyApp for Android.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Hockey-App-RegisterDevice-3.png)


That's it. Now install the HockeyApp that was downloaded and run the app. Login with your credentials and you will see the MyEvents app being listed there. You can send this link to your beta testers and they will be notified whenever a new build happens.



### Great Reads

* [Continuous Integration for Android with Visual Studio Team Services](https://blog.xamarin.com/continuous-integration-for-android-with-visual-studio-team-services/)
* [Continuous Delivery to Google Play with Team Services](https://blog.xamarin.com/continuous-delivery-to-google-play-with-team-services/)
* [Continuous Integration for iOS Apps with Visual Studio Team Services](https://blog.xamarin.com/continuous-integration-for-ios-apps-with-visual-studio-team-services/)


