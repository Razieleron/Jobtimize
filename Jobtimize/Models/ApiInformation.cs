using System.Collections.Generic;
using ConsoleApiCall.Keys;
using Jobtimize.Models;

namespace Jobtimize.Models
{

    public class ApiInformation
    {
        public static string ApiUrl = "https://api.openai.com/v1/completions";
        public static string ApiKey = EnvironmentalVariables.ApiKey;
        public static string IncompletePrompt = $"write me a cover letter for the following job description as though you were a recent coding bootcamp graduate.  Do not claim that I have more than 1 year of experience.  Assume you haveChoose from the following list of skills when you write the cover letter and only include the ones most applicable to the job description:  HTML, CSS, JavaScript, SQL, C#, .NET, MVC, React, Razor, Razor Framework, EF Core, Entity Framework Core, Git, Github, Node.js, Object Oriented Programming, Test-Driven Development, Asynchrony, calling APIs, creating APIs, Authentication with Identity, Authorization, Canvas Methods in JavaScript, Redux, NoSQL, Functional Programming, Bootstrap, Markdown, ES6, ECMAscript.  Be sure to only include skills that are contained within the job description, and do not claim to have any skills that aren't listed above. Only return to me the text for the cover letter.  Here is the job description: ";
        public static string ModelName = "text-davinci-003";

        public static Dictionary<string, object> RequestBodyDict = new Dictionary<string, object>  
                            {
                        {"prompt", IncompletePrompt}, 
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