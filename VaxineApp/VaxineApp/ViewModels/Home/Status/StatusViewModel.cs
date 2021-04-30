using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using DataAccess;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Status
{
    public class StatusViewModel : BaseViewModel
    {
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
        public ICommand RegistrationPageCommand { private set; get; }
        //public ICommand CollectionView_SelectionChangedCommand { private set; get; }
        public async void Add()
        {
            var route = $"{nameof(RegistrationPage)}";
            await Shell.Current.GoToAsync(route);
        }
        //public async Task GetChild()
        //{
        //    var child = await Data.GetChilds();
        //    CopyToChilds(child);

        //}

        
        public StatusViewModel()
        {
            RegistrationPageCommand = new Command(Add);
            //CollectionView_SelectionChangedCommand = new Command<>(CollectionView_SelectionChanged);
        }
    }
}
