using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Family;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyListViewModel
    {
        public ICommand AddFamilyCommand { private set; get; }
        public FamilyListViewModel()
        {
            AddFamilyCommand = new Command(AddFamily);
        }

        async void AddFamily()
        {
            var route = $"{nameof(AddFamilyPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
