using System.Collections.Generic;

namespace Jobtimize.Models
{

    public class GithubProject
    {
        public string ProjectTitle { get; set; }
        public string ProjectUrl { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectLanguages { get; set; }

        public GithubProject(string projectTitle, string projectUrl, string projectDescription, string projectLanguages)
        {
            ProjectTitle = projectTitle;
            ProjectUrl = projectUrl;
            ProjectDescription = projectDescription;
            ProjectLanguages = projectLanguages;
        }
        
        
        
    }


}

// // GithubProject.cs
// public class GithubProject
// {
//     public string ProjectName { get; set; }
//     public string Description { get; set; }

//     public GithubProject(string projectName, string description)
//     {
//         ProjectName = projectName;
//         Description = description;
//     }
// }

// // Program.cs
// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         // Instantiate a GithubProject object named PigDice
//         GithubProject PigDice = new GithubProject("PigDice", "A dice game");

//         // Access the specific property of the PigDice object
//         Console.WriteLine($"Project Name: {PigDice.ProjectName}");
//         Console.WriteLine($"Description: {PigDice.Description}");
//     }
// }

