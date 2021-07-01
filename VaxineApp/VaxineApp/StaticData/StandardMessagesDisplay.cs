using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VaxineApp.StaticData
{
    public static class StandardMessagesDisplay
    {
        public async static void AddDisplayMessage(string input)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.AddTitle, StandardMessagesText.AddBody(input), "OK");
        }
        public async static void EditDisplaymessage(string input)
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.EditTitle, StandardMessagesText.EditBody(input), "OK");
        }
        public async static void NoDataDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.NoDataTitle, StandardMessagesText.NoDataBody, "OK");
        }
        public async static void CanceledDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.CanceledTitle, StandardMessagesText.CanceledBody, "OK");
        }
        public async static void InvalidDataDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.InvalidDataTitle, StandardMessagesText.InvalidDataBody, "OK");
        }
        public async static void FeatureUnderConstructionTitleDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.FeatureUnderConstructionTitle, StandardMessagesText.FeatureUnderConstructionBody, "OK");
        }
        public async static Task<bool> DeleteDisplayMessage(string input)
        {
            return await App.Current.MainPage.DisplayAlert(StandardMessagesText.DeleteTitle, StandardMessagesText.DeleteBody(input), "Yes", "No");
        }
        public async static void NoItemSelectedDisplayMessage()
        {
            await App.Current.MainPage.DisplayAlert(StandardMessagesText.NoItemSelectedTitle, StandardMessagesText.NoItemSelectedBody, "OK");
        }
    }
}
