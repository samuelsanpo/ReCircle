using System;
using System.ComponentModel;
using System.Windows.Input;
using FireAuth;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using ReCircle.Classes;
using ReCircle.Services;
using Xamarin.Forms;

namespace ReCircle.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private string name;
        private string lastName;
        private string mobile;
        private int role;
        private string address;
        private DateTime birth;
        private string email;
        private string document;
        private string gender;
        private string verification;
        private string password;
        private string repeatPassword;
        private NavigationService navigationService;
        private DialogService dialogService;
        private EncryptService encryptService;
        private IAuth auth;
        #endregion

        #region Properties
        public string Name
        {
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
            get
            {
                return name;
            }
        }

        public string LastName
        {
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
                }
            }
            get
            {
                return lastName;
            }
        }

        public string Mobile
        {
            set
            {
                if (mobile != value)
                {
                    mobile = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mobile"));
                }
            }
            get
            {
                return mobile;
            }
        }

        public int Role
        {
            set
            {
                if (role != value)
                {
                    role = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Role"));
                }
            }
            get
            {
                return role;
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

        public DateTime Birth
        {
            set
            {
                if (birth != value)
                {
                    birth = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Birth"));
                }
            }
            get
            {
                return birth;
            }
        }

        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Document
        {
            set
            {
                if (document != value)
                {
                    document = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Document"));
                }
            }
            get
            {
                return document;
            }
        }

        public string Gender
        {
            set
            {
                if (gender != value)
                {
                    gender = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gender"));
                }
            }
            get
            {
                return gender;
            }
        }

        public string Verification
        {
            set
            {
                if (verification != value)
                {
                    verification = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Verification"));
                }
            }
            get
            {
                return verification;
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

        public string RepeatPassword
        {
            set
            {
                if (repeatPassword != value)
                {
                    repeatPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RepeatPassword"));
                }
            }
            get
            {
                return repeatPassword;
            }
        }
        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            auth = DependencyService.Get<IAuth>();
            encryptService = new EncryptService();
            instance = this;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get { return new RelayCommand(Save); } }

        private async void Save()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Gender) || string.IsNullOrEmpty(Document))
            {
                await dialogService.ShowMessage("Error", "Completa todos los espacios antes de comenzar");
                return;
            }

            if (Password.Equals(RepeatPassword))
            {
                await dialogService.ShowMessage("Ups", "Las contaseñas no coinciden");
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Ups", "Parece que no tienes conexión a internet");
            }

            var passwordEncrypt = encryptService.Encrypt(Password);

            string uid = await auth.CreateNewUser(Email, passwordEncrypt);
            InsertFireBase service = new InsertFireBase();
            if (uid != "")
            {
                await service.AddClient(uid, Name, Role, LastName, Mobile, Address, Birth, Email, Document, Gender, Verification, true);
            }
            else
            {
                await dialogService.ShowMessage("Error", "Hubo un error en el camino, intentalo nuevamente");
            }
        }
        #endregion

        #region Singleton
        private static RegisterViewModel instance;

        public static RegisterViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new RegisterViewModel();
            }

            return instance;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
