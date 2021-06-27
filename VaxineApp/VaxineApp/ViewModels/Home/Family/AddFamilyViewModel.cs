using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
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
                if (!StaticDataStore.FamilyNumbers.Contains(Family.HouseNo))
                {
                    Family.Id = Guid.NewGuid();
                    Family.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));

                    var data = JsonConvert.SerializeObject(Family);
                    string a = await DataService.Post(data, $"Family/{Preferences.Get("TeamId", "")}");
                    if (a == "OK")
                    {
                        await App.Current.MainPage.DisplayAlert("Data submited", "Successfully posted", "OK");

                        var route = $"//{nameof(FamilyListPage)}";
                        await Shell.Current.GoToAsync(route);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(a, "Try again", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Duplicate data", $"{Family.HouseNo} Family already exist.", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Empty Field", "Add data to required fields", "OK");
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
