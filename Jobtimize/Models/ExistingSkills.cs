using System.Collections.Generic;

namespace Jobtimize.Models
{
    public class ExistingSkills
    {
        public string filePath = "ScrapedData/ExistingSkills.csv";
        public static List<string> List = new List<string> 
        { 
          "JavaScript", 
          "C#",
          "HTML",
          "CSS",
          ".NET",
          "Entity Framework",
          "EF Core",
          "SQL",
          "APIs",
          "API",
          "React",
          "Firebase",
          "MVC",
          "Model View Controller",
          "Razor",
          "Razor Framework",
          "Git",
          "Github",
          "Node.js",
          "Object Oriented Programming",
          "Test-Driven Development",
          "Authentication",
          "Authorization",
          "Redux",
          "NoSQL",
          "Bootstrap",
          "Markdown",
          "ES6",
          "ECMAscript"
        };


        // this is the logic to extract a string from a csv
    // List<string> existingSkills = new List<string>();
    // using (StreamReader reader = new StreamReader(filePath))
    // {
    //     string line;
    //     while ((line = reader.ReadLine()) != null)
    //     {
    //         existingSkills.Add(line);
    //     }
    // }
        
    }





}