﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Area
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AreaTabs : TabbedPage
    {
        public AreaTabs()
        {
            InitializeComponent();
        }
    }
}