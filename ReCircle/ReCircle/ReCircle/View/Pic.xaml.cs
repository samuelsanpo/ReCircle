using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;
using Acr.UserDialogs;
using Newtonsoft.Json;
using System.Linq;

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
        public const string ServiceApiUrl = "https://recircle.cognitiveservices.azure.com/";
        public const string ApiKey = "fbf24fb85def416abc476fdb4ba50e70";

       

        private async void Button_Choose(object sender, EventArgs e)
        {
            await Plugin.Media.CrossMedia.Current.Initialize();

            _foto = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions());
            Img.Source = FileImageSource.FromFile(_foto.Path);
        }

        private async void Button_Take(object sender, EventArgs e)
        {

             await Plugin.Media.CrossMedia.Current.Initialize();

            _foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Directory = "Vision",
                Name = "Target.jpg"
            });
            Img.Source = FileImageSource.FromFile(_foto.Path);

        

        }




        private async void Button_Analyze(object sender, EventArgs e)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading("Clasificando..."))
            {
                if (_foto == null) return;

                var stream = _foto.GetStream();

                var httpClient = new HttpClient();
                var url = ServiceApiUrl;
                httpClient.DefaultRequestHeaders.Add("Prediction-Key", ApiKey);

                var content = new StreamContent(stream);

                var response = await httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Hubo un error en la deteccion...");
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();

                var c = JsonConvert.DeserializeObject<ClasificationResponse>(json);

                var p = c.Predictions.FirstOrDefault();
                if (p == null)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Image no reconocida.");
                    return;
                }
                Resultado.Text = $"{p.Tag} - {p.Probability:p0}";
                Precision.Progress = p.Probability;
            }

        Acr.UserDialogs.UserDialogs.Instance.Toast("Clasificacion terminada...");
        }
}


    public class ClasificationResponse
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
        public DateTime Created { get; set; }
        public Prediction[] Predictions { get; set; }
    }

    public class Prediction
    {
        public string TagId { get; set; }
        public string Tag { get; set; }
        public float Probability { get; set; }
    }

}
