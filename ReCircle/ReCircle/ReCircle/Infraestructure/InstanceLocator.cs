using System;
using ReCircle.ViewModel;

namespace ReCircle.Infraestructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            var mainViewModel = MainViewModel.GetInstance();
            Main = mainViewModel;

        }
    }
}
