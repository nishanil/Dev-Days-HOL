## Dev Days HOL
Dev Days provide attendees with an intense, hands-on learning experience. Spend the time exploring mobile development with sessions from Xamarin & Azure Experts, our technology partners, and members of the local developer community, then roll up your sleeves for an afternoon dedicated to diving into code.

Inside this repo you will find all of the hands on labs that you will experience at Dev Days!

![MyEvents](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/01%20Dev-Labs/screenshots/DevDaysHol.png?token=AC9rtpBmFfQKj9epUgPmYpJ8IBqxXsgkks5X0l_BwA%3D%3D)

##Overview
The Hands On Labs are broken down into 3 modules:
* [00 Quickstart - Samples to get you started on Traditional Approach](https://github.com/nishanil/Dev-Days-HOL/tree/master/00%20Quickstart)
* [01 Dev-Labs - Learn to create a Xamarin.Forms app from scratch](https://github.com/nishanil/Dev-Days-HOL/tree/master/01%20Dev-Labs)
* [02 Cloud-Labs - Building Cloud connected & Offline Sync Apps](https://github.com/nishanil/Dev-Days-HOL/tree/master/02%20Cloud-Labs)
  * [Create a no code backend with Easy Tables](https://github.com/nishanil/Dev-Days-HOL/tree/master/02%20Cloud-Labs/01%20EasyTables)
  * [Create a Scalable .NET Backend & Authenticate Users](https://github.com/nishanil/Dev-Days-HOL/tree/master/02%20Cloud-Labs/02%20NetBackend)
* [02 DevOps-Labs - VSTS + HockeyApp](https://github.com/nishanil/Dev-Days-HOL/tree/master/03%20DevOps-Labs)

##Pre-Requisites
* Visual Studio 2015 Community Edition with latest Updates (or higher)
* Xamarin
* Visual Studio Android Emulator or Google Emulators
* Azure subscription (for cloud labs)
* Download the repo

##Lab Setup Guidelines
Here is the full guide for both Mac OS X and Windows to help get you started:
* [Mac OS X](https://docs.google.com/document/d/1moCCFj_QkNA7RSO-hvvPZr9R6eVW-ENSCkEmwZ8wGxc/edit?usp=sharing)
* [Windows](https://docs.google.com/document/d/1wkG36pVcqo3enL5RSSv-haa-2_jSazsruatzPGbPqAM/edit?usp=sharing)

In order to test the part of iOS, for those who do not have a MAC, you can use one of the weekly plans [MacInCloud](https://www.macincloud.com/checkout/managed.html) (Europe Server, OS El Capitain) with the option Enable Remote Build Port enabled.

###Visual Studio Emulator for Android

Instructions to Launch **Visual Studio Emulator for Android** and run an Android Virtual Emulator.

 Click on the _Tools_ menu and click on _Visual Studio Emulator for Android._

![launch](https://cloud.githubusercontent.com/assets/10905781/18432739/6275e418-7901-11e6-828d-aa0e7fb23c59.png)


If Visual Studio Emulator for Android is not installed, you can follow theses instructions to install:

1.  Either launch Visual Studio Installer or launch from Control Panel > Programs & Features.

2. Click on Modify.

![modify](https://cloud.githubusercontent.com/assets/10905781/18432456/ad985cc0-78ff-11e6-8134-679a93c9a65a.png)

3. In the set of components, check the box beside Microsoft Visual Studio Emulator for Android.

![install](https://cloud.githubusercontent.com/assets/10905781/18432518/0c5a5358-7900-11e6-91ef-4bcfb148143b.png)

Install and Re-Launch Visual Studio.

##Troubleshooting

If you get this error:
> Unzipping failed. Please download https://dl-ssl.google.com/android/repository/support_r19.0.1.zip and extract it to the C:\Users<username>\AppData\Local\Xamarin\Android.Support.v4\19.0.1\content directory. 

`Quick workaround`: Clear all the folders in the location _C:\Users\<username>\AppData\Local\Xamarin_ and then **Clean** and **Rebuild** the Solution. This build might take some time (serveral minutes) as all the packages will be downloaded again by the Visual Studio.

##License
Copyright (c) 2016 Nish Anil

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
