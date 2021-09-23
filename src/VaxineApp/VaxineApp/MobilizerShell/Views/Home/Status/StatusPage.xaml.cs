using System;
using VaxineApp.MobilizerShell.ViewModels.Home.Status;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Status
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPage : ContentPage
    {
        public delegate void OnAppearingEventHandler(object sender, EventArgs e);

        public event OnAppearingEventHandler PageAppearing;

        public StatusPage()
        {
            InitializeComponent();

            var svm = ((StatusViewModel)this.BindingContext);
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