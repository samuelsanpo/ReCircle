using System;
using System.Threading.Tasks;
using ReCircle.View;
using ReCircle.ViewModel;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
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
                    var mainViewModel2 = MainViewModel.GetInstance();
                    mainViewModel2.maps = new MapsViewModel();
                    Application.Current.MainPage = new MainShell();
                    break;
                case "Maps":
                    await App.Current.MainPage.Navigation.PushAsync(new Maps());
                    break;
                case "Semilla":
                    await App.Current.MainPage.Navigation.PushAsync(new SemillaPage());
                    break;
                case "Arbol":
                    await App.Current.MainPage.Navigation.PushAsync(new ArbolPage());
                    break;
                case "Compra":
                    var pr = new PopupCompra();
                    var scaleAnimation = new ScaleAnimation
                    {
                        PositionIn = MoveAnimationOptions.Right,
                        PositionOut = MoveAnimationOptions.Left
                    };
                    pr.Animation = scaleAnimation;
                    await PopupNavigation.PushAsync(pr);
                    break;
                case "Confirm":
                    await PopupNavigation.PopAsync();
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

