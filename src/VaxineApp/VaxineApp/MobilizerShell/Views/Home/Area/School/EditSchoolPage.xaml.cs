using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Area.School;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Area.School
{
    [QueryProperty(nameof(School), nameof(School))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSchoolPage : ContentPage
    {
        public string School { get; set; }
        public EditSchoolPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var result = JsonConvert.DeserializeObject<SchoolModel>(School);
            BindingContext = new EditSchoolViewModel(result);
        }
    }
}