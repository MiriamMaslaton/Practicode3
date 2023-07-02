using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    public class PortFolio
    {
        public string? language { get; set; }
        public DateTime Commit { get; set; }
        public int Stars { get; set; }
        public int PullRequest { get; set; }
        public string Link { get; set; }
        public PortFolio(string? language, DateTime commit, int stars, int pullRequest, string link)
        {
            this.language = language;
            Commit = commit;
            Stars = stars;
            PullRequest = pullRequest;
            Link = link;
        }
    }
}
