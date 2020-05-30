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
            Person person = new Person();
            InsertFireBase instanceF = new InsertFireBase();
            instanceF.AddPerson("Jordan", "3224292730");

            MainPage = new NavigationPage(new LoginPrueba());


            MainPage = new NavigationPage(new MainPage());

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
