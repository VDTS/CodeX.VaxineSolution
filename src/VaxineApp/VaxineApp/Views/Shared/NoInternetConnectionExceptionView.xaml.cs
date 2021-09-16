
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoInternetConnectionExceptionView : ContentView
    {
        public NoInternetConnectionExceptionView()
        {
            InitializeComponent();
        }
    }
}