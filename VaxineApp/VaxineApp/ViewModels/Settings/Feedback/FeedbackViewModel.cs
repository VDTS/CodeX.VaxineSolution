using DataAccessLib.GithubService;
using Newtonsoft.Json;
using Octokit;
using System;
using System.Windows.Input;
using VaxineApp.AndroidNativeApi;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ParentShellDir.Views.Home;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Home.Status;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings.Feedback
{
    public class FeedbackViewModel : ViewModelBase
    {
        GithubService GithubRestService { get; set; }
        private FeedbackModel feedback;
        public FeedbackModel Feedback
        {
            get
            {
                return feedback;
            }
            set
            {
                feedback = value;
                OnPropertyChanged();
            }
        }

        // Property
        public string AppPackageName;
        public string AppVersionName;

        private bool suggestionRadioButton;
        public bool SuggestionRadioButton
        {
            get
            {
                return suggestionRadioButton;
            }
            set
            {
                suggestionRadioButton = value;
                OnPropertyChanged();
            }
        }

        private bool problemRadioButton;
        public bool ProblemRadioButton
        {
            get
            {
                return problemRadioButton;
            }
            set
            {
                problemRadioButton = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand SubmitIssueOnGithubCommand { private set; get; }
        public ICommand BackButtonBehaviorCommand { private set; get; }

        // ctor
        public FeedbackViewModel()
        {
            // Property
            GithubRestService = new GithubService(Constants.GithubProductHeaderValue, Constants.GithubApiKeyForCreatingIssues, Constants.GtihubRepoOwner, Constants.GithubRepoName);

            Feedback = new FeedbackModel();
            AppPackageName = DependencyService.Get<IPackageName>().PackageName;
            AppVersionName = DependencyService.Get<IAppVersion>().GetVersion();

            // Command
            BackButtonBehaviorCommand = new Command(Backbutton);
            SubmitIssueOnGithubCommand = new Command(SubmitIssue);
        }

        private async void Backbutton()
        {
            var alert = DependencyService.Get<IAlert>();
            var result = await alert.Display("", "Do you want to save as draft?", "Save", "Delete", "Cancel");
            if (result == "Save")
            {
                StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
                var route = "..";
                await Shell.Current.GoToAsync(route);
            }
            else if (result == "Delete")
            {
                StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
                var route = "..";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                return;
            }
        }

        private async void SubmitIssue()
        {
            Feedback.Labels.Add(AppVersionName);

            if (SuggestionRadioButton == false && ProblemRadioButton == true)
            {
                Feedback.Labels.Add("Problem");
            }
            else
            {
                Feedback.Labels.Add("Suggestion");
            }

            var role = await Xamarin.Essentials.SecureStorage.GetAsync("Role");

            if (role == "Mobilizer")
            {
                Feedback.Labels.Add("mobilizer app");
            }
            else if (role == "Supervisor")
            {
                Feedback.Labels.Add("supervisor app");
            }
            else if (role == "Parent")
            {
                Feedback.Labels.Add("parent app");
            }

            if (AppPackageName == "com.codex.vaxineappbeta")
            {
                Feedback.Labels.Add("beta-version");
            }

            // Serialize feedback
            var data = JsonConvert.SerializeObject(Feedback);
            var result = await GithubRestService.SubmitIssue(data);

            if(result == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }else if(result == "OK")
            {
                Feedback = new FeedbackModel();
                StandardMessagesDisplay.IssueSubmitToast();
            }
            else
            {
                StandardMessagesDisplay.CanceledDisplayMessage();
            }
        }
    }
}
