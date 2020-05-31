using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace ReCircle.View
{
    public partial class Camara : ContentPage
    {
        public Camara()
        {
            InitializeComponent();
        }

        async Task<Stream> PickImage()
        {
            await CrossMedia.Current.Initialize();

            var pickOrCamera = await DisplayActionSheet("Do you want to pick a photo from the gallery or take a picture with the camera?", "Cancel", null, "Pick from gallery", "Take with camera");

            MediaFile pickedImage = null;

            if (!CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Camera not available", "Sorry, the camera does not seem available. Maybe you are running this app on an emulator or Simulator where the camera view is not supported?", "OK");


            }
            else
            {
                pickedImage = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    SaveToAlbum = false
                });
            }

            if (pickedImage == null)
                return null;

            CapturedImage.Source = ImageSource.FromStream(() => pickedImage.GetStreamWithImageRotatedForExternalStorage());

            return pickedImage.GetStreamWithImageRotatedForExternalStorage();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var imageStream = await PickImage();

            if (imageStream == null)
                return;

            /*var client = new CustomVisionPredictionClient
            {
                ApiKey = KeysAndUrls.CustomVisionPredictionApiKey,
                Endpoint = KeysAndUrls.PredictionUrl
            };*/

            //var result = await client.ClassifyImageAsync(KeysAndUrls.ProjectId, KeysAndUrls.IterationName, imageStream);
            //var bestResult = result.Predictions.OrderByDescending(p => p.Probability).FirstOrDefault();
            var result = await DependencyService.Resolve<IPlatformPredictionService>().Classify(imageStream);

            // If you go with the OnDeviceCustomVision plugin you can simply do this as well
            // var tags = await CrossImageClassifier.Current.ClassifyImage(imageStream);
            // var result = tags.OrderByDescending(t => t.Probability).First().Tag;
            // More info here: https://github.com/jimbobbennett/Xam.Plugins.OnDeviceCustomVision

            //DescriptionLabel.IsVisible = true;
            //SetLabel($"{result.Tag} ({result.Confidence}%)");
        }
    }
}
