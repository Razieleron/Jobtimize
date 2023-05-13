using System.Collections.Generic;
using ConsoleApiCall.Keys;
using Jobtimize.Models;

namespace Jobtimize.Models
{
    public class ApiInformation
    {
        public static string ApiUrl = "https://api.openai.com/v1/completions";
        public static string ApiKey = EnvironmentalVariables.ApiKey;
        public static string CoverLetterPrompt = $"write me a one page cover letter for the following job description as though you were a recent coding bootcamp graduate.  Do not claim that I have more than 1 year of experience.  Assume you haveChoose from the following list of skills when you write the cover letter and only include the ones most applicable to the job description:  HTML, CSS, JavaScript, SQL, C#, .NET, MVC, React, Razor, Razor Framework, EF Core, Entity Framework Core, Git, Github, Node.js, Object Oriented Programming, Test-Driven Development, Asynchrony, calling APIs, creating APIs, Authentication with Identity, Authorization, Canvas Methods in JavaScript, Redux, NoSQL, Functional Programming, Bootstrap, Markdown, ES6, ECMAscript.  Be sure to only include skills that are contained within the job description, and do not claim to have any skills that aren't listed above. Only return to me the text for the cover letter.  Here is the job description: ";
        public static string GithubProjectOrderPrompt = $"Here is a list of my GithubProjects - ";
        public static string JobDescriptionDistillationPrompt = $"Can you summarize this job description for me?  Be sure to include all the technologies and programming languages listed in your summary- ";
        public static string ModelName = "text-davinci-003";

        public static Dictionary<string, object> CoverLetterRequestBodyDict = new Dictionary<string, object>  
                    {
                        {"prompt", CoverLetterPrompt}, 
                        {"max_tokens", 2000 },
                        {"model", ModelName },
                        {"temperature", 0.5},
                        {"top_p", 1},
                        {"frequency_penalty", 0},
                        {"presence_penalty", 0},
                        {"stop", null}
                    };

        public static Dictionary<string, object> JobDescriptionDistillationRequestBodyDict = new Dictionary<string, object>  
                    {
                        {"prompt", JobDescriptionDistillationPrompt}, 
                        {"max_tokens", 2000 },
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
                        {"max_tokens", 50 },
                        {"model", ModelName },
                        {"temperature", 0.5},
                        {"top_p", 1},
                        {"frequency_penalty", 0},
                        {"presence_penalty", 0},
                        {"stop", null}
                    };
                    
                
    }
}


public static List<object> MessagesList = new List<object>
{
    new Dictionary<string, string> { { "role", "system" }, { "content", "You are a helpful assistant." } },
    new Dictionary<string, string> { { "role", "user" }, { "content", CoverLetterPrompt } }
};

public static Dictionary<string, object> CoverLetterRequestBodyDict = new Dictionary<string, object>
{
    { "messages", MessagesList },
    { "max_tokens", 2000 },
    { "model", ModelName },
    { "temperature", 0.5 },
    { "top_p", 1 },
    { "frequency_penalty", 0 },
    { "presence_penalty", 0 },
    { "stop", null }
};




