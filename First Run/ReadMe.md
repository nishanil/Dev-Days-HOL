##First Run
------------------------------

This troubleshooting guide will help you in making your Xamarin First-Run experience wonderful.

The following steps will help you verify your Xamarin installation and troubleshoot for errors, if any.

**Pre-requisites:**
- Visual Studio 2015 with Xamarin
- Android SDK
- Java SDK (JDK)
- Microsoft Visual Studio Emulator

Once Visual Studio with Xamarin is installed, there are few other things that are required to run Android App smoothly.
These are done behine the scenes automatically given that the required URLs are accessible in the network. 


**Check Installation**:
- Open Visual Studio
- Click on File > New > Project...

![File > New Project...](https://dl.dropboxusercontent.com/u/30949500/Dev-Days-Images/VS-New-Project.JPG)

- From the left pane, select Cross-Platform and then select *Blank Xaml App (Xamarin.Forms Portable)*

![New Blank Xaml App (Xamarin.Forms Portable)](https://dl.dropboxusercontent.com/u/30949500/Dev-Days-Images/VS-New-Project-CP-BlankXamlApp.JPG)

- Enter the Name, Location and Solution name & click on OK
  - Given App name as **App1**

- Once done, set the **App1.Droid** as Start-up project
  - Right click *App1.Droid*
  - Click on **Set as StartUp-Project**

- Right click *App1.Droid* and click on **Build**


**Alternatively, you can open any of the [Finished Project](https://github.com/nishanil/Dev-Days-HOL) and Test it.**

*After this, you should see sometihng like Restoring Packages etc., and then the Build should get over successfully.*

In case, it fails, then you will have to troubleshoot based on the information given below. 



#Troubleshooting
----------------

**Whitelist URLs:**
Generally, you would not be seeing the errors described in this document if your network/firewall allows you to connect to the following URLs:
- [https://dl-ssl.google.com/](https://dl-ssl.google.com/)
- [https://dl.google.com/android](https://dl.google.com/android ) 
- [https://api.nuget.org/v3/](https://api.nuget.org/v3/index.json)

**M2Repository:**
You may see m2repository errors when referencing a NuGet package of the Android Support Libraries or Google Play services. 
The error message resembles the following:

*Download failed. Please download https://dl-ssl.google.com/android/repository/android_m2repository_r22.zip and extract it to the C:\Users\{UserName}\AppData\Local\Xamarin\Android.Support.v4\23.0.0\content directory.*

`Quick workaround`: Clear all the folders in the location _C:\Users\<username>\AppData\Local\Xamarin_ and then **Clean** and **Rebuild** the Solution. This build might take some time (serveral minutes) as all the packages will be downloaded again by the Visual Studio.

If you're not an stable network, you may want to try the offline fix listed below.

The Offline Fix:

1. Close any open instances of Visual Studio

2. Download the file [https://dl-ssl.google.com/android/repository/android_m2repository_r22.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r22.zip) 

3. Rename the file android_m2repository_r22.zip to 96659D653BDE0FAEDB818170891F2BB0.zip (MD5 hash of URL)

4. Open Run (Press Win + R Keys)

5. Type **%LocalAppData%**

    ![Run LocalAppData](https://dl.dropboxusercontent.com/u/30949500/Dev-Days-Images/Run-LocalAppData.png)

6. Navigate to the folder **Xamarin**

    ![Open AppData/Local/Xamarin](https://dl.dropboxusercontent.com/u/30949500/Dev-Days-Images/AppData-Local-Xamarin.png)

7. Navigate to the folder zips (Create if doesn’t exist)

8. Paste the **96659D653BDE0FAEDB818170891F2BB0.zip** in this folder

    ![Navigate to the Folder zips](https://dl.dropboxusercontent.com/u/30949500/Dev-Days-Images/AppData-Local-Xamarin-Zips.png)

9. Restart Visual Studio and try building the project.

In case the M2Repository is different than the one specified, variations can be found in here. 
The steps remain the same, however, Step 2 and 3 changes based on the Repository archive:

| **Repository archive**                                                                                    | **MD5 hash of URL**              |
|-----------------------------------------------------------------------------------------------------------|----------------------------------|
| [android_m2repository_r33.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r33.zip) | 5FB756A25962361D17BBE99C3B3FCC44 |
| [android_m2repository_r32.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r32.zip) | F16A3455987DBAE5783F058F19F7FCDF |
| [android_m2repository_r31.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r31.zip) | 99A8907CE2324316E754A95E4C2D786E |
| [android_m2repository_r30.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r30.zip) | 05AD180B8BDC7C21D6BCB94DDE7F2C8F |
| [android_m2repository_r29.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r29.zip) | 2A3A8A6D6826EF6CC653030E7D695C41 |
| [android_m2repository_r28.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r28.zip) | 17BE247580748F1EDB72E9F374AA0223 |
| [android_m2repository_r27.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r27.zip) | C9FD4FCD69D7D12B1D9DF076B7BE4E1C |
| [android_m2repository_r26.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r26.zip) | 8157FC1C311BB36420C1D8992AF54A4D |
| [android_m2repository_r25.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r25.zip) | 0B3F1796C97C707339FB13AE8507AF50 |
| [android_m2repository_r24.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r24.zip) | 8E3C9EC713781EDFE1EFBC5974136BEA |
| [android_m2repository_r23.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r23.zip) | D5BB66B3640FD9B9C6362C9DB5AB0FE7 |
| [android_m2repository_r22.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r22.zip) | 96659D653BDE0FAEDB818170891F2BB0 |
| [android_m2repository_r21.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r21.zip) | CD3223F2EFE068A26682B9E9C4B6FBB5 |
| [android_m2repository_r20.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r20.zip) | 650E58DF02DB1A832386FA4A2DE46B1A |
| [android_m2repository_r19.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r19.zip) | 263B062D6EFAA8AEE39E9460B8A5851A |
| [android_m2repository_r18.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r18.zip) | 25947AD38DCB4865ABEB61522FAFDA0E |
| [android_m2repository_r17.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r17.zip) | 49054774F44AE5F35A6BA9D3C117EFD8 |
| [android_m2repository_r16.zip](https://dl-ssl.google.com/android/repository/android_m2repository_r16.zip) | 0595E577D19D31708195A83087881EE6 |


**NuGet Packages:** Once you have downloaded the project from Github, it will still require to make use of the NuGet Packages. 
The packages won’t get downloaded in case, the URL [https://nuget.org/](https://nuget.org/) is not allowed through your firewall. 
To fix this issue, making use of the packages in some offline ways, please follow these steps:
1.	Close any open instances of Visual Studio.
2.	Download the packages.zip folder
3.	Right-click the packages.zip and click on Properties.
4.	If available, Check the Unblock checkbox and click on OK.

![unblock Packages.zip](https://dl.dropboxusercontent.com/u/30949500/Dev-Days-Images/packages-zip-unblock.png)

5.	Extract the contents to C:\DevDaysHols\packages
6.	Open Visual Studio and click on build.


**major.minor version 52.0:** Depending on the Android API installed during installation, the projects might either be targeted to Android N (API 24) or Android M (API 23). 
By default, during the installation, Visual Studio installs JDK1.7. 
However, if the project is targeted to API 24, then JDK1.8 is required.

In case, the project with target API 24 is compiled with JDK1.7 in the system, you will see an error like this:

*C:\Program Files (x86)\MSBuild\Xamarin\Android\Xamarin.Android.Common.targets(3,3): Error: java.lang.UnsupportedClassVersionError: com/android/dx/command/Main : Unsupported major.minor version 52.0*


The Version 52.0 here is referring to the specific release of the JDK, which in this case relates to JDK 8. Xamarin.Android 7.0 requires [JDK 1.8](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) to use the Android Nougat (API 24) APIs. 
You can continue to use earlier versions of the JDK if targeting earlier Android API levels:
- [JDK 1.8](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) - up to API 24+
- [JDK 1.7](http://www.oracle.com/technetwork/java/javase/downloads/jdk7-downloads-1880260.html) - up to API 23
- [JDK 1.6](http://www.oracle.com/technetwork/java/javase/downloads/java-archive-downloads-javase6-419409.html) - up to API 20

Additionally, a 64-bit version of the JDK is required to use [custom controls in the Android designer](https://developer.xamarin.com/releases/vs/xamarin.vs_4/xamarin.vs_4.2/#androiddesignercustomcontrols).
The best option is to install the 64-bit version of [JDK 1.8](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html) since it is backwards compatible with all the previous API levels and supports the new Android designer features.
