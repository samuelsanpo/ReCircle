using System;
using System.Collections.Generic;
using FireAuth;
using Xamarin.Forms;

namespace ReCircle.View
{
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
            string Token = await auth.LoginWithEmailPassword("Jordanlosa97@gmail.com", "123");
        }
    }
}
