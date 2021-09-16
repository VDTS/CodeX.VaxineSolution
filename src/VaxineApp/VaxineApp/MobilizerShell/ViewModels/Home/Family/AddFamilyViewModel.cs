using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Family
{
    public class AddFamilyViewModel : ViewModelBase, IDataCrud, IVMUtils
    {
        // Validator
        FamilyValidator ValidationRules { get; set; }
        // Property

        private FamilyModel family;
        public FamilyModel Family
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
            ValidationRules = new FamilyValidator();
            Family = new FamilyModel();

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
            Family.Id = Guid.NewGuid();
            Family.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));

            var result = ValidationRules.Validate(Family);
            if (result.IsValid)
            {
                if (!StaticDataStore.FamilyNumbers.Contains(Family.HouseNo))
                {
                    var jData = JsonConvert.SerializeObject(Family);
                    string postResponse = await DataService.Post(jData, $"Family/{Preferences.Get("TeamId", "")}");
                    if (postResponse == "ConnectionError")
                    {
                        StandardMessagesDisplay.NoConnectionToast();
                    }
                    else if (postResponse == "Error")
                    {
                        StandardMessagesDisplay.Error();
                    }
                    else if (postResponse == "ErrorTracked")
                    {
                        StandardMessagesDisplay.ErrorTracked();
                    }
                    else
                    {
                        _ = await DataService.Put((++StaticDataStore.TeamStats.TotalHouseholds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalHouseholds");
                        StandardMessagesDisplay.AddDisplayMessage($"{Family.ParentName}'s Family ");

                        var route = "..";
                        await Shell.Current.GoToAsync(route);
                    }
                }
                else
                {
                    StandardMessagesDisplay.FamilyDuplicateValidator(Family.HouseNo);
                }
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
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
