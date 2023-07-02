//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Service;
using Service.Entities;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class userController:ControllerBase
    {
        private readonly IGitHubService _githubService;
        public userController(IGitHubService githubService)
        {
                _githubService= githubService;  
        }
        //[HttpGet("{userName}/followers")]
        //public async Task<ActionResult<int>> GetFollowers(string userName)
        //{
        //    return await _githubService.GetUserFollowersAsync(userName);
        //}
        //[HttpGet("{userName}/publicrepos")]
        //public async Task<ActionResult<int>> GetPublicRepos(string userName)
        //{
        //    return await _githubService.GetUserPublicRepositories(userName);
        //}
        [HttpGet("/portFolio")]
        public async Task<List<PortFolio>> GetPortFolio()
        {
            return await _githubService.GetPortfolio();
        }
        [HttpGet("/Search")]
        public async Task<List<Repository>> SearchRepositories(string? language,string? RepositoryName, string? userName)
        {
            return await _githubService.SearchRepositories(RepositoryName,language, userName );
        }
    }


}
