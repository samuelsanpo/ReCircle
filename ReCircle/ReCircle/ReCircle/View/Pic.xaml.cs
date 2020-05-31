using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;
using Acr.UserDialogs;

namespace ReCircle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pic : ContentPage
    {
        private MediaFile _foto;

        public Pic()
        {
            InitializeComponent();
        }



        private async void Button_Choose(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            var foto = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions());
            _foto = foto;
            ImgSource.Source = FileImageSource.FromFile(foto.Path);

        }
        private async void Button_Take(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            var foto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Directory = "clasificator",
                Name= "source.jpg"
            });
            _foto = foto;
            ImgSource.Source = FileImageSource.FromFile(foto.Path);
        }

        private async void Button_Analyze(object sender, EventArgs e)
        {
            const string endpoint = "https://recircle.cognitiveservices.azure.com/";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Prediction-key", "fbf24fb85def416abc476fdb4ba50e70");

            var ContentStream = new StreamContent(_foto.GetStream());

            var response = await httpClient.PostAsync(endpoint, ContentStream);

            if (!response.IsSuccessStatusCode) {
                UserDialogs.Instance.Toast("Error");
                return;
            }

            var json = await response.Content.ReadAsStringAsync();



        }
    }
}