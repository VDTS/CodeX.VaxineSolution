using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VaxineApp.AdminShell.Views.Home.User.UserClaims;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.User.UserClaims
{
    public class AddUserClaimsViewModel : ViewModelBase
    {
        //string Uid;
        private string role;
        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                OnPropertyChanged();
            }
        }

        private string fullName;

        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }

        private string email;

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private List<string> clustersList;
        public List<string> ClustersList
        {
            get
            {
                return clustersList;
            }
            set
            {
                clustersList = value;
                OnPropertyChanged();
            }
        }

        private string selectedCluster;
        public string SelectedCluster
        {
            get
            {
                return SelectedCluster;
            }
            set
            {
                SelectedCluster = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddClaimsAndNextCommand { private set; get; }

        public AddUserClaimsViewModel(string fullName, string email, string phoneNumber, string role)
        {
            this.FullName = fullName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Role = role;

            ClustersList = new List<string>();
            //LoadClusters();

            AddClaimsAndNextCommand = new Command(AddClaimsAndNext);
        }

        private async void AddClaimsAndNext(object obj)
        {
            string route = $"{nameof(CreateUserPage)}?FullName={FullName}&Email={Email}&PhoneNumber={PhoneNumber}&Role={Role}";

            await Shell.Current.GoToAsync(route);
        }

        public async void LoadTeams()
        {
            // Loading teams based on selected Cluster
        }

        public async void LoadClusters()
        {
            var jData = await DataService.Get("Cluster");

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
                    var data = JsonConvert.DeserializeObject<Dictionary<string, ClusterModel>>(jData);

                    if (data != null)
                        foreach (KeyValuePair<string, ClusterModel> item in data)
                        {
                            ClustersList?.Add(
                                    item.Value.ClusterName
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
    }
}