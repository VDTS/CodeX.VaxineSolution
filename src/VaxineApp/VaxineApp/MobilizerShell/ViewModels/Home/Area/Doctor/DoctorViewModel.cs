using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility.Extensions;
using VaxineApp.MobilizerShell.Views.Home.Area.Doctor;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Area.Doctor
{
    public class DoctorViewModel : ViewModelBase, IDataCrud, IVMUtils
    {
        // Property
        private DoctorModel? selectedDoctor;
        public DoctorModel? SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DoctorModel>? doctors;
        public ObservableCollection<DoctorModel>? Doctors
        {
            get
            {
                return doctors;
            }
            set
            {
                doctors = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }


        // Command
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }


        // ctor
        public DoctorViewModel()
        {
            // Property
            Doctors = new ObservableCollection<DoctorModel>();
            SelectedDoctor = new DoctorModel();


            // Get
            Get();

            // Command
            PullRefreshCommand = new Command(Refresh);
            DeleteCommand = new Command(Delete);
            SaveAsPDFCommand = new Command(SaveAsPDF);
            GoToPutPageCommand = new Command(GoToPutPage);
            GoToPostPageCommand = new Command(GoToPostPage);
        }

        public async void Get()
        {
            var jData = await DataService.Get($"Doctor/{Preferences.Get("TeamId", "")}");

            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, DoctorModel>>(jData);

                    if (data != null)
                        foreach (KeyValuePair<string, DoctorModel> item in data)
                        {
                            Doctors?.Add(
                                    new DoctorModel
                                    {
                                        FId = item.Key.ToString(),
                                        Id = item.Value.Id,
                                        Name = item.Value.Name,
                                        IsHeProvindingSupportForSIAAndVaccination = item.Value.IsHeProvindingSupportForSIAAndVaccination
                                    }
                                );
                        }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public async void Delete()
        {
            if (!SelectedDoctor.AreEmpty())
            {
                var isDeleteAccepted = await StandardMessagesDisplay.DeleteDisplayMessage(SelectedDoctor?.Name);
                if (isDeleteAccepted)
                {
                    var deleteResponse = await DataService.Delete($"Doctor/{Preferences.Get("TeamId", "")}/{SelectedDoctor?.FId}");
                    if (deleteResponse == "ConnectionError")
                    {
                        StandardMessagesDisplay.NoConnectionToast();
                    }
                    else if (deleteResponse == "Error")
                    {
                        StandardMessagesDisplay.Error();
                    }
                    else if (deleteResponse == "ErrorTracked")
                    {
                        StandardMessagesDisplay.ErrorTracked();
                    }
                    else if (deleteResponse == "null")
                    {
                        _ = await DataService.Put((--StaticDataStore.TeamStats.TotalDoctors).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalDoctors");

                        StandardMessagesDisplay.ItemDeletedToast();

                        if(SelectedDoctor != null)
                        Doctors?.Remove(SelectedDoctor);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void Clear()
        {
            Doctors?.Clear();
        }

        public void CancelSelection()
        {
            if (SelectedDoctor?.FId != null)
            {
                SelectedDoctor = null;
            }
            else
            {
                return;
            }
        }

        public void SaveAsPDF()
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public async void GoToPostPage()
        {
            var route = $"{nameof(AddDoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void GoToPutPage()
        {
            if (SelectedDoctor?.Name != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedDoctor);
                var route = $"{nameof(EditDoctorPage)}?Doctor={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedDoctor = null;
            }
            else
            {
                StandardMessagesDisplay.NoItemSelectedDisplayMessage();
            }
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
