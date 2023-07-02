using Microsoft.Extensions.Caching.Memory;
using Octokit;
using Service;
using Service.Entities;

namespace WebAPI.CachedServises
{
    public class CachedGitHubService : IGitHubService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IGitHubService _GitHubService;
        private const string UserPortFolioKey ="UserPortFolioKey";
        public CachedGitHubService(IGitHubService GitHubService,IMemoryCache memoryCache)
        {
            _GitHubService = GitHubService;
            _memoryCache = memoryCache;
        }

        public async Task<List<PortFolio>> GetPortfolio()
        {
            if(_memoryCache.TryGetValue(UserPortFolioKey,out List<PortFolio> portFolio))
                return portFolio;

            var cacheOptions=new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30)).SetSlidingExpiration(TimeSpan.FromSeconds(10));

            portFolio = await _GitHubService.GetPortfolio();
            _memoryCache.Set(UserPortFolioKey,portFolio,cacheOptions);
            return portFolio;
        }

        public Task<List<Repository>> SearchRepositories(string? repositoryName, string? language, string? userName)
        {
            return _GitHubService.SearchRepositories(repositoryName, language, userName);
        }
    }
}