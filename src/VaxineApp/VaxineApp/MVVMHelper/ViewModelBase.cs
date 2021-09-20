using DataAccess.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VaxineApp.MVVMHelper
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // This will be removed after moving codebase to new logic
        protected DbService DataService = new DbService(Constants.FirebaseBaseUrl, Constants.FirebaseApiKey);


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
