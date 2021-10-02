
using Newtonsoft.Json;
using VaxineApp.AdminShell.ViewModels.Home.Period;
using VaxineApp.Core.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.AdminShell.Views.Home.Period
{
    [QueryProperty(nameof(Period), nameof(Period))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPeriodPage : ContentPage
    {
        public string? Period { get; set; }
        public EditPeriodPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Period != null)
            {
                var result = JsonConvert.DeserializeObject<PeriodModel>(Period);
                if (result != null) BindingContext = new EditPeriodViewModel(result);
            }
        }
    }
}