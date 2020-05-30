using System;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ReCircle.Services;

namespace ReCircle.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Attributes
        private string userName;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        private bool isVisible;
        private string message;
        private string colorEntryPhone;
        private string colorEntryPassword;
        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;
        private EncryptService encryptService;
        #endregion

        #region Properties
        public string UserName
        {
            set
            {
                if (userName != value)
                {
                    userName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserName"));
                }
            }
            get
            {
                return userName;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
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

        public string Message
        {
            set
            {
                if (message != value)
                {
                    message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
                }
            }
            get
            {
                return message;
            }
        }

        public string ColorEntryPhone
        {
            set
            {
                if (colorEntryPhone != value)
                {
                    colorEntryPhone = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorEntryPhone"));
                }
            }
            get
            {
                return colorEntryPhone;
            }
        }

        public string ColorEntryPassword
        {
            set
            {
                if (colorEntryPassword != value)
                {
                    colorEntryPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorEntryPassword"));
                }
            }
            get
            {
                return colorEntryPassword;
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();
            encryptService = new EncryptService();
            IsEnabled = true;
            ColorEntryPhone = "Black";
            ColorEntryPassword = "Black";
            IsVisible = false;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }
        public ICommand RegisterTypeCommand { get { return new RelayCommand(Registro); } }

        private async void Login()
        {

            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
            {
                IsVisible = true;
                Message = "Debes ingresar numero de celular y contraseña";
                ColorEntryPhone = "Yellow";
                ColorEntryPassword = "Yellow";
                return;
            }

            if (string.IsNullOrEmpty(UserName))
            {
                ColorEntryPassword = "Black";
                IsVisible = true;
                Message = "Debes ingresar el numero de celular";
                ColorEntryPhone = "Yellow";
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                ColorEntryPhone = "Black";
                IsVisible = true;
                Message = "Debes ingresar la contraseña";
                ColorEntryPassword = "Yellow";
                return;
            }

            ColorEntryPhone = "Black";
            ColorEntryPassword = "Black";
            IsVisible = false;
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            //var passwordEncrypt = encryptService.Encrypt(Password);


            await navigationService.Navigate("Principal");
            IsRunning = false;
            IsEnabled = true;

        }

        private async void Registro()
        {
            await navigationService.Navigate("Registro");
        }
        #endregion

        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
