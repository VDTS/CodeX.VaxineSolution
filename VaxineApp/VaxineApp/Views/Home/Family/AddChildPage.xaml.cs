using DataAccessLib.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChildPage : ContentPage
    {
        protected DbService DataService = new DbService();
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisedPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private string _fullName;
        public string FullName {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisedPropertyChanged(nameof(FullName));
            }
        }
        private DateTime _dOB;
        public DateTime DOB {
            get { return _dOB; }
            set
            {
                _dOB = value;
                RaisedPropertyChanged(nameof(DOB));
            }
        }
        private string _gender;
        public string Gender {
            get { return _gender; }
            set
            {
                _gender = value;
                RaisedPropertyChanged(nameof(Gender));
            }
        }
        private bool _oPV0;
        public bool OPV0 {
            get { return _oPV0; }
            set
            {
                _oPV0 = value;
                RaisedPropertyChanged(nameof(OPV0));
            }
        }
        private int _rINo;
        public int RINo {
            get { return _rINo; }
            set
            {
                _rINo = value;
                RaisedPropertyChanged(nameof(RINo));
            }
        }
        Guid FamilyId;
        //public ICommand SaveDataCommand { private set; get; }
        public AddChildPage()
        {

        }
        public AddChildPage(Guid familyId)
        {
            InitializeComponent();
            FamilyId = familyId;
            PageContent.BindingContext = this;
            //SaveDataCommand = new Command(SaveChild);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            ChildModel clinic = new ChildModel()
            {
                Id = Guid.NewGuid(),
                FullName = FullName,
                DOB = DOB,
                Gender = Gender,
                OPV0 = OPV0,
                RINo = RINo
            };

            var data = JsonConvert.SerializeObject(clinic);

            string a = DataService.Post(data, $"Child/{FamilyId}");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            //var route = $"//{nameof(ClinicPage)}";
            //await Shell.Current.GoToAsync(route);
            await Navigation.PushAsync(new FamilyDetailsPage(new GetFamilyModel { Id = FamilyId}));
            //var route = $"../";
            //await Shell.Current.GoToAsync(route);
        }
    }
}