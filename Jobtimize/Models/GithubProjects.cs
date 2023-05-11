using System.Collections.Generic;

namespace Jobtimize.Models
{

    public class GithubProject
    {
        public string ProjectTitle { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectLanguage { get; set; }

        public GithubProject(string projectTitle, string projectUrl, string projectDescription, string projectLanguage)
        {
            ProjectTitle = projectTitle;
            ProjectUrl = projectUrl;
            ProjectDescription = projectDescription;
            ProjectLanguage = projectLanguage;
        }
        
        GithubProject PigDice = new GithubProject("Pig Dice", "https://github.com/Razieleron/pig-dice", "Two Player Dice Game", "JavaScript");
        
    }


}


