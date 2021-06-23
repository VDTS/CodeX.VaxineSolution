﻿using Octokit;
using DataAccessLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;
using Octokit.Internal;
using System.Collections.ObjectModel;
using VaxineApp.Models.Metadata;
using VaxineApp.MVVMHelper;

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
                UserMetaData umd = new UserMetaData();
                var client = new GitHubClient(new ProductHeaderValue("RoadMap"));

                var tokenAuth = new Credentials(Constants.GithubApiKeyForCreatingIssues);
                client.Credentials = tokenAuth;

                var i = new NewIssue(IssueTitle);


                i.Body = $"Issue: {IssueDetails} {Environment.NewLine} Device Manufacturer: {umd.DeviceManufacturer} {Environment.NewLine} Device Model: {umd.DeviceModel}  {Environment.NewLine} Device Name: {umd.DeviceName} {Environment.NewLine} Device Platform: {umd.DevicePlatform} {Environment.NewLine} Device Version: {umd.DeviceVersion} {Environment.NewLine} User Email: {SharedData.Email}";

                if(SuggestionRadioButton == false && ProblemRadioButton == true)
                {
                    i.Labels.Add("Problem");
                }
                else
                {
                    i.Labels.Add("Suggestion");
                }

                var issue = await client.Issue.Create("VDTS", "RoadMap", i);
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
