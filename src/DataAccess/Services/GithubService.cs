using Newtonsoft.Json;
using Octokit;
using System;
using System.Threading.Tasks;
using VaxineApp.Core.Models.AccountModels;
using Xamarin.Essentials;

namespace DataAccess.GithubService
{
    public class GithubService
    {
        // Property

        // Ctor
        public GithubService(string githubProductHeaderValue, string githubApiKeyForCreatingIssues, string githubRepoOwner, string githubRepoName)
        {
            GithubProductHeaderValue = githubProductHeaderValue;
            GithubApiKeyForCreatingIssues = githubApiKeyForCreatingIssues;
            GithubRepoOwner = githubRepoOwner;
            GithubRepoName = githubRepoName;
        }

        public string GithubProductHeaderValue { get; }
        public string GithubApiKeyForCreatingIssues { get; }
        public string GithubRepoOwner { get; }
        public string GithubRepoName { get; }

        public async Task<string> SubmitIssue(string feedback)
        {
            var data = JsonConvert.DeserializeObject<FeedbackModel>(feedback);

            var client = new GitHubClient(new ProductHeaderValue(GithubProductHeaderValue));

            var tokenAuth = new Credentials(GithubApiKeyForCreatingIssues);
            client.Credentials = tokenAuth;

            var i = new NewIssue(data.Title)
            {
                Body = data.Body
            };

            foreach (var item in data.Labels)
            {
                i.Labels.Add(item);
            }


            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return "ConnectionError";
            }
            else
            {
                try
                {
                    var issue = await client.Issue.Create(GithubRepoOwner, GithubRepoName, i);
                    if (issue.State.Value.ToString() == "Open")
                    {
                        return "OK";
                    }
                    else
                    {
                        return "Error";
                    }
                }
                catch (Exception)
                {
                    return "Error";
                }
            }
        }
    }
}
