using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ReCircle.Services;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

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
        private string city;
        private string address;
        private string locality;
        private bool isRunning;
        private bool isEnabled;
        private bool isVisible;
        private bool isVisibleTwo;
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

        public string City
        {
            set
            {
                if (city != value)
                {
                    city = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("City"));
                }
            }
            get
            {
                return city;
            }
        }

        public string Address
        {
            set
            {
                if (address != value)
                {
                    address = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Address"));
                }
            }
            get
            {
                return address;
            }
        }

        public string Locality
        {
            set
            {
                if (locality != value)
                {
                    locality = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Locality"));
                }
            }
            get
            {
                return locality;
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

        public bool IsVisible
        {
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisible"));
                }
            }
            get
            {
                return isVisible;
            }
        }

        public bool IsVisibleTwo
        {
            set
            {
                if (isVisibleTwo != value)
                {
                    isVisibleTwo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisibleTwo"));
                }
            }
            get
            {
                return isVisibleTwo;
            }
        }
        #endregion

        #region Constructor
        public MapsViewModel()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
            GeolocationMethod();
            IsVisible = false;
            IsVisibleTwo = true;
            IsRunning = true;
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
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                    await dialogService.ShowMessage(Latitude.ToString(), Longitude.ToString());

                    AddressMethod();

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

        public async void AddressMethod()
        {

            try
            {
                //var placemarks = await Geocoding.GetPlacemarksAsync(Latitude, Longitude);
                Geocoder geoCoder = new Geocoder();

                Position position = new Position(Latitude, Longitude);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                //string address = possibleAddresses.FirstOrDefault();
                Address = possibleAddresses.FirstOrDefault();
                //var placemark = placemarks?.FirstOrDefault();
                //if (possibleAddresses != null)
                //{

                //City = placemark.AdminArea;
                //Address = placemark.Thoroughfare + " " + placemark.SubThoroughfare;
                //Locality = placemark.Locality;

                //}

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                await dialogService.ShowMessage("Error", "Mal Feature");
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
                await dialogService.ShowMessage("Error", "Mal Exception");
            }
            IsEnabled = true;
            IsRunning = false;
        }
        #endregion

        #region Commands
        public ICommand MapsCommand { get { return new RelayCommand(Maps); } }
        public ICommand SaveCommand { get { return new RelayCommand(Save); } }
        public ICommand AceptLocationCommand { get { return new RelayCommand(AceptLocation); } }

        
        public async void Maps()
        {
            await navigationService.Navigate("Maps");
        }

        private async void AceptLocation()
        {
            IsVisible = true;
            IsVisibleTwo = false;
        }

        private  async void Save()
        {
            await dialogService.ShowMessage("Muy bien", "La dirección se a guardado correctamente");
            await navigationService.Navigate("Principal");
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
