using System;

using ReCircle.Model;
using ReCircle.Services;
using ReCircle.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReCircle
{
    
    public partial class App : Application
    {
       

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPrueba());

        }



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
