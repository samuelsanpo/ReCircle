using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReCircle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Registrarse(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Verificacion());
        }
    }
}