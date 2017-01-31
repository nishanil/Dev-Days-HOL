using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UIKit;
using CoreLocation;
using MapKit;
using System.Xml.Linq;
using WeatherPCL;

namespace WeatherApp
{
    public partial class ViewController : UIViewController
    {
        WeatherAPI _weatherApi = new WeatherAPI();
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            MyMap.RegionChanged += MyMap_RegionChanged;
        }

        async void MyMap_RegionChanged(object sender, MKMapViewChangeEventArgs e)
        {
            string tempData = await _weatherApi.GetWeather(MyMap.CenterCoordinate.Latitude, MyMap.CenterCoordinate.Latitude);
            Description.Text = tempData;
        }
    }
}