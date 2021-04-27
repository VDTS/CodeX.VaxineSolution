using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Status
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildDetails : ContentPage
    {
        ChildrenModel Child { get; set; }
        public ChildDetails(ChildrenModel child)
        {
            InitializeComponent();
            Child = child;
            this.BindingContext = Child;
        }
    }
}