using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Xamarin.Forms;

namespace ReCircle.Services
{
    public class ShowPasswordTriggerAction : TriggerAction<Button>
    {
        public string IconImageName { get; set; }

        public string ContraseñaName { get; set; }

        protected override void Invoke(Button sender)
        {
            Image imageIconView = ((Grid)sender.Parent).FindByName<Image>(IconImageName);
            StandardEntry Contraseña = ((Grid)((Grid)sender.Parent).Parent).FindByName<StandardEntry>(ContraseñaName);
            string imageFile = string.Empty;

            if (Contraseña.IsPassword)
            {
                imageFile = (Device.RuntimePlatform == Device.UWP) ? "Assets/hideicon.png" : "hideicon.png";
            }
            else
            {
                imageFile = (Device.RuntimePlatform == Device.UWP) ? "Assets/showicon.png" : "showicon.png";
            }
            imageIconView.Source = ImageSource.FromFile(imageFile);
            Contraseña.IsPassword = !Contraseña.IsPassword;
            Contraseña.CursorPosition = (Contraseña.Text != null) ? Contraseña.Text.Length : 0;
        }
    }
}
