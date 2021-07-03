using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using VaxineApp.ViewModels.Home.Status.Anonymous;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Status.Anonymous
{
    [QueryProperty(nameof(AnonymousChild), nameof(AnonymousChild))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAnonymousChildPage : ContentPage
    {
        public string AnonymousChild { get; set; }
        public EditAnonymousChildPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<AnonymousChildModel>(AnonymousChild);
            BindingContext = new EditAnonymousChildViewModel(result);
        }
    }
}