using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Models.Home.Area;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VaxineApp.Views.Home.Family
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyDetailsPage : ContentPage
    {
        protected DbContext Data = new DbContext();
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisedPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public GetFamilyModel Family { get; set; }
        private List<ChildModel> _childs;
        public List<ChildModel> Childs
        {
            get { return _childs; }
            set
            {
                _childs = value;
                RaisedPropertyChanged(nameof(Childs));
            }
        }
        public FamilyDetailsPage(GetFamilyModel family)
        {
            Childs = new List<ChildModel>();
            Family = family;
            LoadData();
            AddChildCommand = new Command(AddChild);
            InitializeComponent();
            PageContent.BindingContext = this;
        }

        private async void LoadData()
        {
            var data = await Data.GetChild("T", Family.HouseNo);
            foreach (var item in data)
            {
                Childs.Add(
                    new ChildModel
                    {
                        FullName = item.FullName,
                        //DOB = item.DOB,
                        //Gender = item.Gender,
                        //OPV0 = item.OPV0,
                        //RINo = item.RINo
                    });
            }
        }

        public FamilyDetailsPage()
        { }
        public ICommand AddChildCommand { private set; get; }

        public async void AddChild()
        {
            var route = $"//{new AddChildPage(Family.HouseNo)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddChildPage(Family.HouseNo));
            //var route = $"{new AddChildPage(Family.HouseNo)}";
            //await Shell.Current.GoToAsync(route);
        }
    }
}