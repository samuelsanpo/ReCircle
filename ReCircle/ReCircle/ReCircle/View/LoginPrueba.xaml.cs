using System;
using System.Collections.Generic;
using System.ComponentModel;
using FireAuth;
using ReCircle.Services;
using Xamarin.Forms;

namespace ReCircle.View
{
    [DesignTimeVisible(true)]
    public partial class LoginPrueba : ContentPage
    {
        IAuth auth;

        public LoginPrueba()
        {
            
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
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
            string Token = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
            if (Token != "")
            {
                await DisplayAlert("Good", "E-mail or password are incorrect. Try again!", "OK");
            }
            else
            {
                ShowError();
            }
        }

        async void Register_Clicked(System.Object sender, System.EventArgs e)
        {
            
            ProductService service = new ProductService();
            await service.AddProduct("Gotas canabis", "Gotas de canabis", 19500, 45, "https://firebasestorage.googleapis.com/v0/b/recircle-d8492.appspot.com/o/productsImg%2FWhatsApp%20Image%202020-05-31%20at%2011.12.17%20AM%20(1).jpeg?alt=media&token=4fc6dfb0-f6a5-488d-ae9d-ec630dfd8056", 4);
            await DisplayAlert("Good", "bien registrado", "OK");

        }

    }
}
