using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using Octokit.Internal;
using System.Collections.ObjectModel;

namespace VaxineApp.ViewModels.Feedback
{
    public class FeedbackViewModel : BaseViewModel
    {
        public ICommand SubmitIssueOnGithubCommand { private set; get; }
        private string _issueTitle;
        public string IssueTitle
        {
            get { return _issueTitle; }
            set
            {
                _issueTitle = value;
                RaisedPropertyChanged(nameof(IssueTitle));
            }
        }
        private bool _suggestionRadioButton;
        public bool SuggestionRadioButton
        {
            get { return _suggestionRadioButton; }
            set
            {
                _suggestionRadioButton = value;
                RaisedPropertyChanged(nameof(SuggestionRadioButton));
            }
        }

        private bool _problemRadioButton;
        public bool ProblemRadioButton
        {
            get { return _problemRadioButton; }
            set
            {
                _problemRadioButton = value;
                RaisedPropertyChanged(nameof(ProblemRadioButton));
            }
        }

        private string _issueDetails;
        public string IssueDetails
        {
            get { return _issueDetails; }
            set
            {
                _issueDetails = value;
                RaisedPropertyChanged(nameof(IssueDetails));
            }
        }

        public FeedbackViewModel()
        {
            SubmitIssueOnGithubCommand = new Command(SubmitIssueOnGithub);
        }

        private async void SubmitIssueOnGithub()
        {
            SubmitIssue();
            var route = $"//{nameof(StatusPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void SubmitIssue()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("VDTSApps"));

                var tokenAuth = new Credentials(SecretsVault.GithubApiKeyForCreatingIssues);
                client.Credentials = tokenAuth;

                var i = new NewIssue(IssueTitle);
                i.Body = IssueDetails;
                if(SuggestionRadioButton == false && ProblemRadioButton == true)
                {
                    i.Labels.Add("Problem");
                }
                else
                {
                    i.Labels.Add("Suggestion");
                }

                var issue = await client.Issue.Create("VDTS", "VDTSApps", i);
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Issue not submitted", "try again!", "OK");
            }
        }
    }
}
