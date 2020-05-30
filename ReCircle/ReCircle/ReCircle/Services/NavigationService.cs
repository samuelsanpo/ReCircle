using System;
using System.Threading.Tasks;

namespace ReCircle.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
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

