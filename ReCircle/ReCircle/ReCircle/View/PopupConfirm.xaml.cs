﻿using Rg.Plugins.Popup.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReCircle.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupConfirm //: PopupPage
    {
        public PopupConfirm()
        {
            InitializeComponent();
        }


        public ScaleAnimation Animation { get; internal set; }
    }
}