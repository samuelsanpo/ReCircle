using System;
namespace ReCircle.ViewModel
{
    public class MainViewModel
    {
        #region Attributes
        #endregion

        #region Properties
        public LoginViewModel login { get; set; }
        public RegisterViewModel register { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            //ViewModels
            login = new LoginViewModel();
            register = new RegisterViewModel();
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
        #endregion
    }
}
