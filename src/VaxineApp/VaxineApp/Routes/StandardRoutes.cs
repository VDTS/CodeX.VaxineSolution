using Xamarin.Forms;

namespace VaxineApp.Routes
{
    public static class StandardRoutes
    {
        public async static void GoToAddPage(string pageName)
        {
            var route = pageName;
            await Shell.Current.GoToAsync(route);
        }
    }
}
