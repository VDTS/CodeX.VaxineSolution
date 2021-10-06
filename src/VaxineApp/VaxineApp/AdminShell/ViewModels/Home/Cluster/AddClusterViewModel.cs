using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.Cluster
{
    public class AddClusterViewModel : ViewModelBase
    {
        ClusterValidator? ValidationRules { get; set; }
        // Property
        private ClusterModel? cluster;
        public ClusterModel? Cluster
        {
            get
            {
                return cluster;
            }
            set
            {
                cluster = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PostCommand { private set; get; }

        public AddClusterViewModel()
        {
            Cluster = new ClusterModel();
            ValidationRules = new ClusterValidator();

            // Command
            PostCommand = new Command(Post);
        }
        private async void Post()
        {
            if (Cluster != null)
            {
                Cluster.Id = Guid.NewGuid();

                var result = ValidationRules?.Validate(Cluster);
                if (result != null && result.IsValid)
                {
                    var jData = JsonConvert.SerializeObject(Cluster);

                    string postResponse = await DataService.Post(jData, $"Cluster");
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
                        StandardMessagesDisplay.AddDisplayMessage(Cluster.ClusterName);

                        var route = "..";
                        await Shell.Current.GoToAsync(route);
                    }
                }
                else
                {
                    StandardMessagesDisplay.ValidationRulesViolation(result?.Errors[0].PropertyName, result?.Errors[0].ErrorMessage);
                }
            }
        }
    }
}
