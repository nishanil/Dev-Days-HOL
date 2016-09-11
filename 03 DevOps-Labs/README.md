## Dev Days HOL - DevOps

 In the previous module, we looked at creating a .NET APIs, creating database in the cloud and authenticating users. Continuous Integration (CI) is one of the key practices of DevOps. Visual Studio Team Services simplifies continuous integration for your applications for all platforms and all languages. VSTS let's you create and manage build processes that automatically compile and test your applications in the cloud or on premises, either on demand or as part of an automated continuous integration strategy. The CI build can also package the code so that it is ready for continuous deployment to test, QA or even Production environments. This module covers how to do that for the cross platform mobile applications. We will pick up the same code that we completed in the previous module, create repository, commit code, create build definitions & deploy to beta testers using HockeyApp.

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
2. Create a new VSTS account. Enter the following information.
    * Give valid name
    * Choose `Git` for managing code
    * Host your project close to your location.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/CreateAccount.png)

3. Once the account is created, you will see the VSTS landing page. Choose **Colloborate on Code** to add the earlier downloaded code to your repo.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Dashboard.png)

4. Now click on **Clone in Visual Studio** . This step will open up your Visual Studio locally

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Clone-in-VS-1.png)

5. Enter your Microsoft credentials when prompted

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Clone-in-VS.png)

6. Provide the local **path** for your repository and hit **Clone** in the **Team Explorer** window inside Visual Studio

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Clone-in-VS-2.png)

7. Open Windows Explorer and copy all the content of the **Codes** folder that your downloaded to your local repository

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Copy-Code-From-Git.png)

8. Go back to Visual Studio, in the **Team Explorer**, write your commit message and commit all the changes. Be sure to select `Commit All and Sync`

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Commit-And-Push-1.png)

9. Once committed, you should see this screen

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Commit-And-Push.png)

10. Head back to your VisualStudio.com dashboard and you will see your code has been uploaded successfully.

![](https://raw.githubusercontent.com/nishanil/Dev-Days-HOL/master/03%20DevOps-Labs/screenshots/Dashboard-Code-Pushed.png)


## Create CI build definitions for Android & iOS 

