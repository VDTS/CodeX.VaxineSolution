using Newtonsoft.Json;
using VaxineApp.MobilizerShell.ViewModels.Home.Status.Anonymous;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Status.Anonymous
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