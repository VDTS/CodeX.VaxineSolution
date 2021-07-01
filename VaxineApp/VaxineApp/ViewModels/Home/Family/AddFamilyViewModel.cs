using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Validations;
using VaxineApp.Views.Home.Family;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class AddFamilyViewModel : ViewModelBase, IDataCrud, IVMUtils
    {
        // Property

        private GetFamilyModel family;
        public GetFamilyModel Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
                OnPropertyChanged();
            }
        }


        // Commands

        public ICommand PostCommand { private set; get; }
        // Constructor
        public AddFamilyViewModel()
        {
            // Property
            Family = new GetFamilyModel();

            // Command
            PostCommand = new Command(Post);
        }


        public void Get()
        {
            throw new NotImplementedException();
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public async void Post()
        {
            if (Family.HouseNo != 0)
            {
                if (PhoneNumberValidator.IsPhoneNumberValid(Family.PhoneNumber))
                {
                    if (!StaticDataStore.FamilyNumbers.Contains(Family.HouseNo))
                    {
                        Family.Id = Guid.NewGuid();
                        Family.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));

                        var data = JsonConvert.SerializeObject(Family);
                        string a = await DataService.Post(data, $"Family/{Preferences.Get("TeamId", "")}");
                        if (a == "OK")
                        {
                            StandardMessagesDisplay.AddDisplayMessage($"{Family.ParentName}'s Family ");

                            var route = $"//{nameof(FamilyListPage)}";
                            await Shell.Current.GoToAsync(route);
                        }
                        else
                        {
                            StandardMessagesDisplay.CanceledDisplayMessage();
                        }
                    }
                    else
                    {
                        StandardMessagesDisplay.FamilyDuplicateValidator(Family.HouseNo);
                    }
                }
                else
                {
                    StandardMessagesDisplay.InvalidPhoneNumber();
                }
            }
            else
            {
                StandardMessagesDisplay.InvalidDataDisplayMessage();
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public void SaveAsPDF()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void GoToPostPage()
        {
            throw new NotImplementedException();
        }

        public void GoToPutPage()
        {
            throw new NotImplementedException();
        }

        public void GoToDetailsPage()
        {
            throw new NotImplementedException();
        }

        public void GoToMapPage()
        {
            throw new NotImplementedException();
        }
    }
}
