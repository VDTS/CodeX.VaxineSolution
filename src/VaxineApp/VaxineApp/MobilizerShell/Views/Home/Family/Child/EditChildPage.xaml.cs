using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using VaxineApp.MobilizerShell.ViewModels.Home.Family.Child;
using VaxineApp.Models;
using VaxineApp.StaticData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.MobilizerShell.Views.Home.Family.Child
{
    [QueryProperty(nameof(Child), nameof(Child))]
    [QueryProperty(nameof(FamilyId), nameof(FamilyId))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditChildPage : ContentPage
    {
        public string? FamilyId { get; set; }
        public string? Child { get; set; }
        public EditChildPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                if (Child != null)
                {
                    var result = JsonConvert.DeserializeObject<ChildModel>(Child);
                    if(result != null) BindingContext = new EditChildViewModel(result, Guid.Parse(FamilyId));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                StandardMessagesDisplay.InputToast(ex.Message);
            }
        }
    }
}