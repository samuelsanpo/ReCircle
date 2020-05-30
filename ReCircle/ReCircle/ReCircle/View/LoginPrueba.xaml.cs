using System;
using System.Collections.Generic;
using System.ComponentModel;
using FireAuth;
using Xamarin.Forms;

namespace ReCircle.View
{
    [DesignTimeVisible(true)]
    public partial class LoginPrueba : ContentPage
    {
        IAuth auth;

        public LoginPrueba()
        {
            auth = DependencyService.Get<IAuth>();
            InitializeComponent();
        }

        async private void LoginClicked()
        {
            string Token = await auth.LoginWithEmailPassword("Jordanlosa97@gmail.com", "123");            
        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string a = auth.pru("hola", "hola");
            await DisplayAlert(a, "E-mail or password are incorrect. Try again!", "OK");
            //string Token = await auth.LoginWithEmailPassword("Jordanlosa97@gmail.com", "123");
        }
    }
}
