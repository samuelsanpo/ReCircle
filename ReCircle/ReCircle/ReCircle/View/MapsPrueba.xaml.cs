using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace ReCircle.View
{
    public partial class MapsPrueba : ContentPage
    {
        public MapsPrueba()
        {
          InitializeComponent();
            var defaultPin = new Pin { Type = PinType.Place, Label = "This is my home", Address = "Here"
                ,Position = new Position(-23.68, -46.87) };
            map.Pins.Add(defaultPin);
        }
    }
}
