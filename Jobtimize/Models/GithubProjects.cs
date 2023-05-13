using System.Collections.Generic;

namespace Jobtimize.Models
{
    public class GithubProject
    {
        public int ProjectKey { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectLanguages { get; set; }

        public GithubProject(){}
        public GithubProject(int projectKey, string projectTitle, string projectUrl, string projectDescription, string projectLanguages)
        {
            ProjectKey = projectKey;
            ProjectTitle = projectTitle;
            ProjectUrl = projectUrl;
            ProjectDescription = projectDescription;
            ProjectLanguages = projectLanguages;
        }
    }
}


