using System;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ReCircle.Services;
using Xamarin.Essentials;

namespace ReCircle.ViewModel
{
    public class MapsViewModel : INotifyPropertyChanged
    {
        #region Attributes
        DialogService dialogService;
        NavigationService navigationService;
        private string firstCamera;
        private double latitude;
        private double longitude;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string FirstCamera
        {
            set
            {
                if (firstCamera != value)
                {
                    firstCamera = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstCamera"));
                }
            }
            get
            {
                return firstCamera;
            }
        }

        public double Latitude
        {
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Latitude"));
                }
            }
            get
            {
                return latitude;
            }
        }

        public double Longitude
        {
            set
            {
                if (longitude != value)
                {
                    longitude = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Longitude"));
                }
            }
            get
            {
                return longitude;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }
        #endregion

        #region Constructor
        public MapsViewModel()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Methods
        public async void GeolocationMethod()
        {
            IsRunning = true;
            IsEnabled = false;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    //FirstCamera = "4.00542,-7.748585, 15";
                    Latitude = 4.00542;
                    Longitude = -7.748585;
                    await dialogService.ShowMessage(Latitude.ToString(), Longitude.ToString());

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await dialogService.ShowMessage("Error", fnsEx.ToString());
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await dialogService.ShowMessage("Error", fneEx.ToString());
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await dialogService.ShowMessage("Ups", "Debes permitirnos acceder a tu ubicación");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await dialogService.ShowMessage("Error", "Algo salio mal, intentalo de nuevo");
            }

        }
        #endregion

        #region Commands
        public ICommand MapsCommand { get { return new RelayCommand(Maps); } }

        public async void Maps()
        {
            await navigationService.Navigate("Maps");
            GeolocationMethod();
            
        }

        #endregion

        #region Singleton
        private static MapsViewModel instance;

        public static MapsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MapsViewModel();
            }

            return instance;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
