using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReCircle.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace ReCircle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Maps : ContentPage
    {
        public Maps()
        {
            InitializeComponent();
            var locationViewModel = MapsViewModel.GetInstance();
            double latitude = locationViewModel.Latitude;
            double longitude = locationViewModel.Longitude;
            var ubicationPin = new Pin { Type = PinType.Place, Label = "Ubicación Actual", Address = "Aqui", Position = new Position(latitude, longitude) };

            map.Pins.Add(ubicationPin);
            Position target = new Position(latitude, longitude);
            map.InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(new CameraPosition(target, 18));
        }
    }
}