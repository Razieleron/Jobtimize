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
        public static string CoverLetterPrompt = $"You an aspiring software developer.  You have diverse experience and have always been fascinated with the process of process optimization and automation.  You have a bachelors degree in social science from portland state university.  You discovered your love for programming when repetitive tasks from your current job were too boring for you to handle.  You started automating those tasks and discovered a love for computer programming.  As a result of that newfound passion, you decided to enroll in Epicodus, an intensive 5 week coding bootcamp where you were going to be learning JavaScript, C# and React.  You gained exposure to a lot of other technologies along the way, such as HTML, CSS, SQL, and .Net.  You also gained experience with MVC, Razor/Razor Framework, EF Core (entity framework core), Git and Github.  In your studies, you gained experience with Node.js, Object oriented programming, and Test driven development.  You were even exposed to calling and creating APIs, Authentication/Authorization, functional programming, and bootstrap.  Write me a one page cover letter for the following job description and only return to me the text for the cover letter. Be sure to not only repeat what I've given you here as a prompt, but take each skill listed and compare it to the job description - for example, if the job description lists front-end development, you can say something like 'I have experience in a variety of front end programming languages and frameworks, such as: etc. etc. etc.'.  If The skill is not listed here in this prompt, DO NOT INCLUDE IT IN THE COVER LETTER. Here is the job description: ";
        public static string GithubProjectOrderPrompt = $"Here is a list of my GithubProjects - ";
        public static string JobDescriptionDistillationPrompt = $"Can you summarize this job description for me?  Be sure to include all the technologies and programming languages listed in your summary- ";
        public static string ModelName = "text-davinci-003";


        public static Dictionary<string, object> CoverLetterRequestBodyDict = new Dictionary<string, object>
                    {
                        { "prompt", CoverLetterPrompt },
                        { "max_tokens", 2000 },
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








