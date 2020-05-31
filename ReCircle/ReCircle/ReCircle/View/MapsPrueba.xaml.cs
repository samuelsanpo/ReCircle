using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace ReCircle.View
{
    [DesignTimeVisible(false)]
    public partial class MapsPrueba : ContentPage
    {
        public MapsPrueba()
        {
          InitializeComponent();
        }

        void map_CameraChanged(System.Object sender, Xamarin.Forms.GoogleMaps.CameraChangedEventArgs e)
        {
            CameraPosition a = e.Position;
            
            latitutedText.Text = a.Target.Latitude.ToString() + "," + a.Target.Latitude.ToString();
        }

        async void map_MapLongClicked(System.Object sender, Xamarin.Forms.GoogleMaps.MapLongClickedEventArgs e)
        {
            
            double lat = Math.Round(e.Point.Latitude, 6);
            double longi = Math.Round(e.Point.Longitude, 6);
            await DisplayAlert("Error", lat.ToString(), "OK");
        }
    }
}
