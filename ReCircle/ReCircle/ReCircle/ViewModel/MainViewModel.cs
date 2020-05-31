using System;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ReCircle.Services;

namespace ReCircle.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private NavigationService navigationService;
        private DialogService dialogService;
        #endregion

        #region Properties
        public LoginViewModel login { get; set; }
        public RegisterViewModel register { get; set; }
        public MapsViewModel maps { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            //Services
            navigationService = new NavigationService();
            dialogService = new DialogService();
            //ViewModels
            login = new LoginViewModel();
            //Singleton
            instance = this;
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand MapsCommand { get { return new RelayCommand(Maps); } }

        public async void Maps()
        {
            await navigationService.Navigate("Maps");
            await dialogService.ShowMessage("Hola", "");
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
