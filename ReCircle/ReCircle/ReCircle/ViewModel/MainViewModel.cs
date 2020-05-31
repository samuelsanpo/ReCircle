using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Firebase.Database;
using GalaSoft.MvvmLight.Command;
using ReCircle.Model.Adapter;
using ReCircle.Services;

namespace ReCircle.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private NavigationService navigationService;
        private DialogService dialogService;
        private ObservableCollection<Product> productsType;
        #endregion

        #region Properties
        public LoginViewModel login { get; set; }
        public RegisterViewModel register { get; set; }
        public MapsViewModel maps { get; set; }

        public ObservableCollection<Product> ProductsType
        {
            set
            {
                if (productsType != value)
                {
                    productsType = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProductsType"));
                }
            }
            get
            {
                return productsType;
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            //Services
            navigationService = new NavigationService();
            dialogService = new DialogService();
            productsType = new ObservableCollection<Product>();
            //ViewModels
            login = new LoginViewModel();
            //Singleton
            instance = this;
        }
        #endregion

        #region Methods
        private readonly string ChildName = "Products";

        readonly FirebaseClient firebase = new FirebaseClient("https://recircle-d8492.firebaseio.com/");

        public async Task<List<Product>> GetAllProducts()
        {
            ProductsType.Clear();
            return (await firebase
                .Child(ChildName)
                .OnceAsync<Product>()).Select(item => new Product
                {
                    Name = item.Object.Name,
                    ProductId = item.Object.ProductId,
                    Description = item.Object.Description,
                    Price = item.Object.Price,
                    Stock = item.Object.Stock,
                    UrlImage = item.Object.UrlImage
                }).ToList();
        }

        public async void LoadProducts()
        {
            var productsList = await GetAllProducts();
            ProductsType.Clear();
            for (int number = 0; number < productsList.Count; number++)
            {
                    ProductsType.Add(new Product
                    {
                        ProductId = productsList[number].ProductId,
                        Name = productsList[number].Name,
                        Description = productsList[number].Description,
                        Price = productsList[number].Price,
                        Stock = productsList[number].Stock,
                        Type = productsList[number].Type,
                        UrlImage = productsList[number].UrlImage,
                    });
            }

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
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }
        public ICommand ReciclaCommand { get { return new RelayCommand(Recicla); } }
        public ICommand ContactoCommand { get { return new RelayCommand(Contacto); } }
        public ICommand SemillaCommand { get { return new RelayCommand(Semilla); } }
        public ICommand ComprarCommand { get { return new RelayCommand(Comprar); } }
        public ICommand ConfirmCommand { get { return new RelayCommand(Confirm); } }

        public void Refresh()
        {
            LoadProducts();
        }

        public async void Recicla()
        {
            await navigationService.Navigate("Reciclar");
        }

        public async void Contacto()
        {
            await navigationService.Navigate("Arbol");
        }

        public async void Semilla()
        {
            await navigationService.Navigate("Semilla");
        }

        public async void Comprar()
        {
            await navigationService.Navigate("Compra");
        }

        public async void Confirm()
        {
            await navigationService.Navigate("Confirm");
        }

        #region Commands

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
