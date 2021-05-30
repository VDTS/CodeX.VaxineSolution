using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;

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
        private string _suggestionRadioButton;
        public string SuggestionRadioButton
        {
            get { return _suggestionRadioButton; }
            set
            {
                _suggestionRadioButton = value;
                RaisedPropertyChanged(nameof(SuggestionRadioButton));
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
            var route = $"//{nameof(StatusPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
