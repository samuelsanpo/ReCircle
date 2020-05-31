﻿using System;
using System.Threading.Tasks;
using ReCircle.View;
using ReCircle.ViewModel;
using Xamarin.Forms;

namespace ReCircle.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "Registro":
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.register = new RegisterViewModel();
                    await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
                    break;
                case "Principal":
                    Application.Current.MainPage = new MainShell();
                    break;
                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}

