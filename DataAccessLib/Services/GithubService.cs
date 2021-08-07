﻿using DataAccessLib.Models;
using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaxineApp.DataAccessLib;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DataAccessLib.GithubService
{
    public class GithubService
    {
        // Property

        // Ctor
        public GithubService()
        {

        }

        public async Task<string> SubmitIssue(string feedback)
        {
            var data = JsonConvert.DeserializeObject<FeedbackModel>(feedback);

            var client = new GitHubClient(new ProductHeaderValue(Constants.GithubProductHeaderValue));

            var tokenAuth = new Credentials(Constants.GithubApiKeyForCreatingIssues);
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
                    var issue = await client.Issue.Create(Constants.GtihubRepoOwner, Constants.GithubRepoName, i);
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