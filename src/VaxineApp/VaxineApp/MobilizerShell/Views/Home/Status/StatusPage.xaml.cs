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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (FilterOverlay.IsVisible)
            {
                // If overlay is visible, slide it to the right, and set as invisible.
                await FilterOverlay.TranslateTo(this.Width - 100, FilterOverlay.Y);
                FilterOverlay.IsVisible = false;
            }
            else
            {
                // If overlay is invisible, make it visible and slide to the left.
                FilterOverlay.IsVisible = true;
                await FilterOverlay.TranslateTo(0, FilterOverlay.Y);
            }
        }
    }
}