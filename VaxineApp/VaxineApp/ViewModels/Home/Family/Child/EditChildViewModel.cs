using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using Newtonsoft.Json;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;

namespace VaxineApp.ViewModels.Home.Family.Child
{
    public class EditChildViewModel : ViewModelBase
    {
        // Property
        private ChildModel child;
        public ChildModel Child
        {
            get
            {
                return child;
            }
            set
            {
                child = value;
                OnPropertyChanged();
            }
        }

        Guid FamilyId;
        // Command
        public ICommand PutCommand { private set; get; }

        public EditChildViewModel(ChildModel child, Guid familyId)
        {
            FamilyId = familyId;
            Child = child;
            PutCommand = new Command(Put);
        }

        private async void Put()
        {
            var jsonData = JsonConvert.SerializeObject(Child);
            var data = await DataService.Put(jsonData, $"Child/{FamilyId}/{Child.FId}");
            if (data == "Submit")
            {
                StandardMessagesDisplay.EditDisplaymessage(child.FullName);
                var route = $"//{nameof(StatusPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                StandardMessagesDisplay.CanceledDisplayMessage();
            }
        }
    }
}
