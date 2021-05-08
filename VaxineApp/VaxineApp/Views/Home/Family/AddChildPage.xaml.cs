using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChildPage : ContentPage
    {
        int HouseNo;
        public AddChildPage()
        {

        }
        public AddChildPage(int houseNo)
        {
            InitializeComponent();
            HouseNo = houseNo;
        }
    }
}