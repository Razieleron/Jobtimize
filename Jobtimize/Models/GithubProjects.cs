using System.Collections.Generic;

namespace Jobtimize.Models
{
    public class GithubProject
    {
        public string ProjectTitle { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectLanguages { get; set; }

        public GithubProject(){}
        public GithubProject(string projectTitle, string projectUrl, string projectDescription, string projectLanguages)
        {
            ProjectTitle = projectTitle;
            ProjectUrl = projectUrl;
            ProjectDescription = projectDescription;
            ProjectLanguages = projectLanguages;
        }
    }
}


