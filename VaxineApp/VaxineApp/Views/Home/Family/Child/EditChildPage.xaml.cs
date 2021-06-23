using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Family.Child;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family.Child
{
    [QueryProperty(nameof(Child), nameof(Child))]
    [QueryProperty(nameof(FamilyId), nameof(FamilyId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditChildPage : ContentPage
    {
        public string FamilyId { get; set; }
        public string Child { get; set; }
        public EditChildPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<ChildModel>(Child);
            BindingContext = new EditChildViewModel(result, Guid.Parse(FamilyId));
        }
    }
}