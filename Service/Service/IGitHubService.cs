using Octokit;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IGitHubService
    {
        public Task<List<PortFolio>> GetPortfolio();
        public Task<List<Repository>> SearchRepositories(string? repositoryName, string? language, string? userName);
    }
}
