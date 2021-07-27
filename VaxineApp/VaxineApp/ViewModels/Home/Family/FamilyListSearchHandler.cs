using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.Models;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Family;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyListSearchHandler : SearchHandler
    {
        public ObservableCollection<GetFamilyModel> Families { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            Families = StaticDataStore.Families;
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Families
                    .Where(family => family.ParentName.ToLower().Contains(newValue.ToLower()))
                    .ToList<GetFamilyModel>();
            }
        }
        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            await Task.Delay(1000);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            // The following route works because route names are unique in this application.

            var JsonData = JsonConvert.SerializeObject(item);
            var route = $"{nameof(FamilyDetailsPage)}?Family={JsonData}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
