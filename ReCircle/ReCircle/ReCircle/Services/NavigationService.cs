using System;
using System.Threading.Tasks;
using ReCircle.View;
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
                    await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
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

