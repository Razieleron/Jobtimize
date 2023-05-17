using System.Runtime.CompilerServices;
using System.Collections.Generic;
using ConsoleApiCall.Keys;
using Jobtimize.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jobtimize.Models
{
    public class ApiInformation
    {
        public static string ApiUrl = "https://api.openai.com/v1/completions";
        public static string ApiKey = EnvironmentalVariables.ApiKey;
        public static string CoverLetterPrompt = @$"You an aspiring software developer.  You have diverse experience and have always been fascinated with the process of process optimization and automation.  You have a bachelors degree in social science from portland state university.  You discovered your love for programming when repetitive tasks from your current job were too boring for you to handle.  You started automating those tasks and discovered a love for computer programming.  As a result of that newfound passion, you decided to enroll in Epicodus, an intensive 5 month coding bootcamp where you were going to be learning JavaScript, C# and React.  You gained exposure to a lot of other technologies along the way, such as HTML, CSS, SQL, and .Net.  You also gained experience with MVC, Razor/Razor Framework, EF Core (entity framework core), Git and Github.  In your studies, you gained experience with Node.js, Object oriented programming, and Test driven development.  You were even exposed to calling and creating APIs, Authentication/Authorization, functional programming, and bootstrap. 

        Generate a cover letter for the following job description, with the following format.  Initially, address the letter to the Hiring Manager.  In the first paragraph, communicate how you match with the company and that it is a good culture fit by parsing the job description for relevant company values and expressing how they align with your own.
        
        In the second paragraph, highlight how the skills you learned while attending epicodus are relevant to the job description, making sure to only include skills and talents from the provided list below. DO NOT MENTION ANY SKILLS OR TALENTS THAT ARE NOT IN THE FOLLOWING LIST.

        List of Skills and Talents:
        1. JavaScript 
        2. C#
        3. HTML
        4. CSS
        5. .NET
        6. Entity Framework
        7. EF Core
        8. SQL
        9. APIs
        10. API
        11. React
        12. Firebase
        13. MVC
        14. Model View Controller
        15. Razor
        16. Razor Framework
        17. Git
        18. Github
        19. Node.js
        20. Object Oriented Programming
        21. Test-Driven Development
        22. Authentication
        23. Authorization
        24. Redux
        25. NoSQL
        26. Bootstrap
        27. Markdown
        28. ES6
        29. ECMAscript

        In the third paragraph, summarize the culture fit and skill fit between yourself and the job description and express a desire to work there in a clear and unique way.  End the cover letter with the name Jannon Sielaff, the phone number (503) 756 0150, the email jannon.sielaff@gmail.com, a linked in url to https://www.linkedin.com/in/jannon-sielaff/ and a github repository url to https://github.com/Razieleron/


        here is the Job Description:

";
        public static string GithubProjectOrderPrompt = @$"Here is a list of my GithubProjects - ";
        public static string JobDescriptionDistillationPrompt = $"Can you summarize this job description for me?  Be sure to include all the technologies and programming languages listed in your summary- ";
        public static string ModelName = "text-davinci-003";


        public static Dictionary<string, object> CoverLetterRequestBodyDict = new Dictionary<string, object>
                    {
                        { "prompt", CoverLetterPrompt },
                        { "max_tokens", 50 },
                        { "model", ModelName },
                        { "temperature", 0.5 },
                        { "top_p", 1 },
                        { "frequency_penalty", 0 },
                        { "presence_penalty", 0 },
                        { "stop", null }
                    };

        public static Dictionary<string, object> JobDescriptionDistillationRequestBodyDict = new Dictionary<string, object>  
                    {
                        {"prompt", JobDescriptionDistillationPrompt}, 
                        {"max_tokens", 50 },
                        {"model", ModelName },
                        {"temperature", 0.5},
                        {"top_p", 1},
                        {"frequency_penalty", 0},
                        {"presence_penalty", 0},
                        {"stop", null}
                    };

        public static Dictionary<string, object> GithubProjectRequestBodyDict = new Dictionary<string, object>  
                    {
                        {"prompt", GithubProjectOrderPrompt}, 
                        {"max_tokens", 100 },
                        {"model", ModelName },
                        {"temperature", 0.5},
                        {"top_p", 1},
                        {"frequency_penalty", 0},
                        {"presence_penalty", 0},
                        {"stop", null}
                    };
                    
                
    }
}








