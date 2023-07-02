using Microsoft.Extensions.Options;
using Octokit;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace Service
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly GitHubIntegrationOptions _options;
        public GitHubService(IOptions<GitHubIntegrationOptions> options)
        {
            _client = new GitHubClient(new ProductHeaderValue("my-github-app"));
            _options = options.Value;
        }
        public async Task<List<PortFolio>> GetPortfolio()
        {
            List<PortFolio> result = new List<PortFolio>();
            var repositories = (await _client.Repository.GetAllForUser(_options.UserName)).ToList();
            foreach (var Repository in repositories)
            {
                var language = Repository.Language;
                var commit = (await _client.Repository.Commit.GetAll(Repository.Id))[0].Commit.Author.Date.LocalDateTime;
                var Stars = Repository.StargazersCount;
                var PullRequest = (await _client.Repository.PullRequest.GetAllForRepository(Repository.Id)).Count();
                var Link = Repository.Url;
                result.Add(new PortFolio(language, commit, Stars, PullRequest, Link));
            }
            return result;
        }
        public async Task<List<Repository>> SearchRepositories(string? repositoryName = null, string? language = null, string? userName = null)
        {
            var search = "is:public ";

            if (!string.IsNullOrEmpty(repositoryName))
                search += $"name:{repositoryName} ";
            if (!string.IsNullOrEmpty(language))
                search +=$"language:{language} ";
            if (!string.IsNullOrEmpty(userName))
                search += $"user:{userName} ";
            var request = new SearchRepositoriesRequest(search);
            var result = await _client.Search.SearchRepo(request);
            return result.Items.ToList();
        }
    }
}
