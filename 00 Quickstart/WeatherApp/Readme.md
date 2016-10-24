# Weather App Xamarin.iOS

In this module we will build a native iOS and Android app using Visual Studio, Xamarin.iOS and Xamarin.Android
The app we are building is called Weather App that gets the weather information from a web services
when you tap a location on the Maps.

## Overview

What we will learn in this lab?

- Calling web method to get weather data
- Code sharing between iOS and Android app

### iOS
- Creating a user interface using iOS Designer
- Using Maps control in an app 

### Android
- Adding Android permission
- Accessing device location

In the _Start_ folder you will find the partially completed Weather App solution.
That we will use to complete this exercise

## Pre-requisites 
Before executing the labs be sure to setup your macOS or Windows machines as mentioned in the Lab Setup guidelines. 
The steps & screenshots in this guide focuses on Visual Studio development in Windows. 
However, the same labs can be executed in Xamarin Studio on a macOS too. 

When you rung the app, the iOS simulator should start and load the app with blank screen. Now you are all set up to write some code!


### Android 

The project contains layout files for designing UI. Open `Resources\layout\Main.axml`, it should open in the Android designer.
Select the button, in the property panel change the text to _Get Weather_ and Save.

You need add locate [permissions](https://developer.android.com/guide/topics/security/permissions.html), before we access the device location.

1. Create a new Xamarin.Android application named GetLocation.
2. Edit **AssemblyInfo.cs**, and declare the permissions necessary to use the LocationServices:

```    
[assembly: UsesPermission(Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Manifest.Permission.AccessCoarseLocation)]
```

3. Declare the permissions necessary to use the Geocoder class. This is not strictly necessary for obtaining the GPS coordinates of the device, but this example will attempt to provide a street address for the current location:
```
[assembly: UsesPermission(Manifest.Permission.Internet)]
```

4. Edit **Main.axml** so that it contains two TextViews and a Button:

```
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="fill_parent"
    android:layout_height="fill_parent"
    android:paddingTop="20dp"
    android:paddingLeft="8dp"
    android:paddingRight="8dp">
    <TextView
        android:layout_width="fill_parent"
        android:textAppearance="?android:attr/textAppearanceMedium"
        android:layout_height="wrap_content"
        android:text="Location (when available)"
        android:id="@+id/location_text"
        android:layout_marginBottom="15dp" />
    <Button
        android:layout_width="fill_parent"
        android:textAppearance="?android:attr/textAppearanceMedium"
        android:layout_height="wrap_content"
        android:id="@+id/get_weather"
        android:text="Get Weather" />
    <TextView
        android:layout_width="fill_parent"
        android:textAppearance="?android:attr/textAppearanceMedium"
        android:layout_height="wrap_content"
        android:text="Address (when available)"
        android:id="@+id/address_text"
        android:layout_marginTop="10dp" />
</LinearLayout>
```

5. Add some instance variables to **MainActivity.cs**:
```
static readonly string TAG = "X:" + typeof (Activity1).Name;
TextView _addressText;
Location _currentLocation;
LocationManager _locationManager;

string _locationProvider;
TextView _locationText;
```
6. Change `OnCreate` method: 

```
protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);
    SetContentView(Resource.Layout.Main);

    _addressText = FindViewById<TextView>(Resource.Id.address_text);
    _locationText = FindViewById<TextView>(Resource.Id.location_text);
    FindViewById<TextView>(Resource.Id.get_address_button).Click += WeatherButton_OnClick;

    InitializeLocationManager();
}
```
The handler for button click will be covered below. The logic for initializing the LocationManager is placed in its own method for clarity.

7. Add a method called InitializeLocationManager to **MainActivity.cs**:
```
void InitializeLocationManager()
{
    _locationManager = (LocationManager) GetSystemService(LocationService);
    Criteria criteriaForLocationService = new Criteria
                                          {
                                              Accuracy = Accuracy.Fine
                                          };
    IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

    if (acceptableLocationProviders.Any())
    {
        _locationProvider = acceptableLocationProviders.First();
    }
    else
    {
        _locationProvider = string.Empty;
    }
    Log.Debug(TAG, "Using " + _locationProvider + ".");
}
```

The `LocationManager` class will listen for GPS updates from the device and notify the application by way of events. In this example we ask Android for the best location provider that matches a given set of `Criteria` and provide that provider to `LocationManager`.

8. Edit **MyActivity.cs** and have it implement the interface `ILocationListener` and add in the methods required by that interface:
```
[Activity(Label = "Get Location", MainLauncher = true, Icon = "@drawable/icon")]
public class Activity1 : Activity, ILocationListener
{
    // removed code for clarity

    public void OnLocationChanged(Location location) {}

    public void OnProviderDisabled(string provider) {}

    public void OnProviderEnabled(string provider) {}

    public void OnStatusChanged(string provider, Availability status, Bundle extras) {}
}
```

9. Override `OnResume` so that `MyActivity` will begin listening to the `LocationManager` when the activity comes into the foreground:
```
protected override void OnResume()
{
    base.OnResume();
    _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
}
```

10. Override `OnPause` and unsubscribe `MainActivity` from the `LocationManager` when the activity goes into the background:
```
protected override void OnPause()
{
    base.OnPause();
    _locationManager.RemoveUpdates(this);
}
```
We reduce the demands on the battery by unsubscribing from the LocationManager when the activity goes into the background.

11. Add an event handler called `WeatherButton_OnClick` to MainActivity. This button allows the user to try and get the address from the latitude and longitude. 
The snippet below contains the code for `WeatherButton_OnClick`:
```
async void WeatherButton_OnClick(object sender, EventArgs eventArgs)
{
    if (_currentLocation == null)
    {
        _addressText.Text = "Can't determine the current address. Try again in a few minutes.";
        return;
    }

    Address address = await ReverseGeocodeCurrentLocation();
    DisplayAddress(address);
}

async Task<Address> ReverseGeocodeCurrentLocation()
{
    Geocoder geocoder = new Geocoder(this);
    IList<Address> addressList =
        await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

    Address address = addressList.FirstOrDefault();
    return address;
}

void DisplayAddress(Address address)
{
    if (address != null)
    {
        StringBuilder deviceAddress = new StringBuilder();
        for (int i = 0; i < address.MaxAddressLineIndex; i++)
        {
            deviceAddress.AppendLine(address.GetAddressLine(i));
        }
        // Remove the last comma from the end of the address.
        _addressText.Text = deviceAddress.ToString();
    }
    else
    {
        _addressText.Text = "Unable to determine the address. Try again in a few minutes.";
    }
}
```

The `ReverseGeocodeCurrentLocation` method will asynchronously lookup a collection of `Address` objects for the currrent location. Depending on factors such as the location and network availability, none, one, or multiple addresses will be returned. The first address (if possible) will be passed to the method `DisplayAddress` which will display the address in the Activity. 

12. Update the method `OnLocationChanged` to display the latitude and longitude when GPS updates are received and update the address:
```
public async void OnLocationChanged(Location location)
{
    _currentLocation = location;
    if (_currentLocation == null)
    {
        _locationText.Text = "Unable to determine your location. Try again in a short while.";
    }
    else
    {
        _locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
        Address address = await ReverseGeocodeCurrentLocation();
        DisplayAddress(address);
    }
}
```
You will need to add the `System.Xml.Linq` assembly.

13. Add the **WeatherPCL** reference to the project

14. Call `GetWeather` method in `WeatherButton_OnClick`, add the below code after `DisplayAddress` method

```           
string tempdata = await _weatherApi.GetWeather(_currentLocation.Longitude, _currentLocation.Latitude);
var alert = new AlertDialog.Builder(this);
alert.SetTitle("Weather").SetMessage(tempdata).Show();
```            

13. Run the application. After a short while, the location of the GPS should be displayed. Be patient it can take a little while to display the co-ordinates

14. Click the button Get Weather to show the current tempreture for the address

### iOS

The projects contains a `Main.storyboard` file a interface designer surface where we will 
add UI controls for Maps, labels and button. A `ViewController.cs` a controller file where
we will write the business logic, in this exercise this will be touch handler for `Map` controls
and code to get the weather data.

And finally `AppDelegate.cs` this where the bootstrapping code for app

You can take a look at completed project in Finish folder, if you get stuck any where during this exercise

## Let's design the interface

Please follow the instructions to add a controls to the design surface

1. Double click on the `Main.storyboard` file, once designer is loaded you should some iOS `View` rendered on the device
2. Drag and drop a `Label` control from the Toolbox to the designer surface
3. Let's give it a meaningful name that we can identify in the `ViewController` code
4. Now repeat the steps 2 and 3, but add the `Map View` instead
5. When you're done the Designer should looks something like this: ![Designer](https://github.com/prashantvc/HOL-WeatherApp/blob/master/Screenshots/Designer.PNG)
6. Finally, lets run it in the simulator to verify that map loads

## Get location co-ordinates from the Map

1. Open the `ViewController.cs`, and locate the `ViewDidLoad` method. <br/> `ViewDidLoad` method is called when the screen loads on the screen
2. Create an event handler for `RegionChanged` event for the Map, lets call it `MyMap_RegionChanged`
3. We want to access the Geo coordinates for center of the displayed region, `MyMap.Region.Center` gives the value
4. Add the **WeatherPCL** project reference to your project
5. Pass the _longitude_ and _latitude_ values from `MyMap.Region.Center` value to the `GetWeather` method from `WeatherAPI` classs, where we get weather information from the web service
6. Update the `MyMap_RegionChanged` code to:
```
async void MyMap_RegionChanged(object sender, MKMapViewChangeEventArgs e)
{
    var weatherApi = new WeatherAPI();

    string tempData = await _weatherApi.GetWeather(MyMap.CenterCoordinate.Latitude, MyMap.CenterCoordinate.Latitude);
    Description.Text = tempData;
}
```


## Parse the weather data

We are using the OpenWeatherMap service to get the weather data, please take a look at the `GetWeather` method in `ViewController.cs` file.
Once we parsed the data returned from the web service we assign it to the `Description` label

`Description.Text = $"City {city}, {temperature}°C";`

## What's next?

Once you competed this exercise, you can try extend the app with:

- Display current weather icon
- Add forecast details
