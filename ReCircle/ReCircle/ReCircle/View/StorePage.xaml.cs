using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReCircle.Model.Adapter;
using ReCircle.ViewModel;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReCircle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {
        public StorePage()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();
            base.Appearing += (object sender, EventArgs e) =>
            {
                mainViewModel.RefreshCommand.Execute(this);
            };
        }

        private async void MyListView_ItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var choose = e.Item as Product;
            var pr = new PopupCompra();
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Right,
                PositionOut = MoveAnimationOptions.Left
            };

            pr.Animation = scaleAnimation;
            await PopupNavigation.PushAsync(pr);
        }
    }
}