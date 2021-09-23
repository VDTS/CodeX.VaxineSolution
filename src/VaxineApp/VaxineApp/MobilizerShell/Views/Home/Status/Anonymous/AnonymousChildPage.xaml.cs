
using System;
using VaxineApp.MobilizerShell.ViewModels.Home.Status.Anonymous;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Status.Anonymous
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnonymousChildPage : ContentPage
    {
        public delegate void OnAppearingEventHandler(object sender, EventArgs e);

        public event OnAppearingEventHandler PageAppearing;
        public AnonymousChildPage()
        {
            InitializeComponent();
            var svm = ((AnonymousChildViewModel)this.BindingContext);
            this.PageAppearing += svm.FirstLoad;
        }

        protected override void OnAppearing()
        {
            OnPageAppearing();
        }
        protected virtual void OnPageAppearing()
        {
            if (PageAppearing != null)
            {
                PageAppearing(this, EventArgs.Empty);
            }
        }

    }
}