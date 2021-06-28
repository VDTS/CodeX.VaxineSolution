﻿using Octokit;
using System;
using System.Windows.Input;
using VaxineApp.Models.Metadata;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Status;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings.Feedback
{
    public class FeedbackViewModel : ViewModelBase
    {
        // Property
        private string issueTitle;
        public string IssueTitle
        {
            get
            {
                return issueTitle;
            }
            set
            {
                issueTitle = value;
                OnPropertyChanged();
            }
        }

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

        private string issueDetails;
        public string IssueDetails
        {
            get
            {
                return issueDetails;
            }
            set
            {
                issueDetails = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand SubmitIssueOnGithubCommand { private set; get; }
        public ICommand AttachScreenshotOnGithubIssueCommand { private set; get; }

        // ctor
        public FeedbackViewModel()
        {
            SubmitIssueOnGithubCommand = new Command(SubmitIssueOnGithub);
            AttachScreenshotOnGithubIssueCommand = new Command(AttachScreenshotOnGithubIssue);
        }

        private async void AttachScreenshotOnGithubIssue(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
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
                var client = new GitHubClient(new ProductHeaderValue("VaxineSolution"));

                var tokenAuth = new Credentials(Constants.GithubApiKeyForCreatingIssues);
                client.Credentials = tokenAuth;

                var i = new NewIssue(IssueTitle);


                i.Body = $"Issue: {IssueDetails}";

                if(SuggestionRadioButton == false && ProblemRadioButton == true)
                {
                    i.Labels.Add("Problem");
                }
                else
                {
                    i.Labels.Add("Suggestion");
                }

                var issue = await client.Issue.Create("VDTS", "CodeX.VaxineSolution", i);
                if(issue.State.Value.ToString() == "Open")
                {
                    await App.Current.MainPage.DisplayAlert("Issue", "Issue submited", "OK");
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Issue not submitted", "try again!", "OK");
            }
        }
    }
}
