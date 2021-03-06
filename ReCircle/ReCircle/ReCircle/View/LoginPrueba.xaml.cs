﻿using System;
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
            string uid = await auth.CreateNewUser(EmailInput.Text,PasswordInput.Text);
            InsertFireBase service = new InsertFireBase();
            if(uid != "")
            {
                //service.AddPerson(uid,)
            }
            else
            {
                await DisplayAlert("Error", "E-mail or password are incorrect. Try again!", "OK");
            }
            
        }

    }
}
