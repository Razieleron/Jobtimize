using System.Collections.Generic;
using ConsoleApiCall.Keys;
using Jobtimize.Models;

namespace Jobtimize.Models
{
    public class ApiInformation
    {
        public static string ApiUrl = "https://api.openai.com/v1/completions";
        public static string ApiKey = EnvironmentalVariables.ApiKey;
        public static string CoverLetterPrompt = $"write me a cover letter for the following job description as though you were a recent coding bootcamp graduate.  Do not claim that I have more than 1 year of experience.  Assume you haveChoose from the following list of skills when you write the cover letter and only include the ones most applicable to the job description:  HTML, CSS, JavaScript, SQL, C#, .NET, MVC, React, Razor, Razor Framework, EF Core, Entity Framework Core, Git, Github, Node.js, Object Oriented Programming, Test-Driven Development, Asynchrony, calling APIs, creating APIs, Authentication with Identity, Authorization, Canvas Methods in JavaScript, Redux, NoSQL, Functional Programming, Bootstrap, Markdown, ES6, ECMAscript.  Be sure to only include skills that are contained within the job description, and do not claim to have any skills that aren't listed above. Only return to me the text for the cover letter.  Here is the job description: ";
        public static string GithubProjectOrderPrompt = $"write me a cover letter for the following job description as though you were a recent coding bootcamp graduate.  Do not claim that I have more than 1 year of experience.  Assume you haveChoose from the following list of skills when you write the cover letter and only include the ones most applicable to the job description:  HTML, CSS, JavaScript, SQL, C#, .NET, MVC, React, Razor, Razor Framework, EF Core, Entity Framework Core, Git, Github, Node.js, Object Oriented Programming, Test-Driven Development, Asynchrony, calling APIs, creating APIs, Authentication with Identity, Authorization, Canvas Methods in JavaScript, Redux, NoSQL, Functional Programming, Bootstrap, Markdown, ES6, ECMAscript.  Be sure to only include skills that are contained within the job description, and do not claim to have any skills that aren't listed above. Only return to me the text for the cover letter.  Here is the job description: ";
        public static string ModelName = "text-davinci-003";

        public static Dictionary<string, object> CoverLetterRequestBodyDict = new Dictionary<string, object>  
                    {
                        {"prompt", CoverLetterPrompt}, 
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

// private static async Task<string> GetGptResponseAsync(HttpClient httpClient, Dictionary<string, object> requestBodyDict)
// {
//     HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ApiInformation.ApiUrl);
//     request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiInformation.ApiKey);
//     request.Content = new StringContent(JsonConvert.SerializeObject(requestBodyDict), System.Text.Encoding.UTF8, "application/json");

//     HttpResponseMessage response = await httpClient.SendAsync(request);
//     var responseContent = await response.Content.ReadAsStringAsync();
//     string responseText = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent).choices[0].text;

//     return responseText;
// }

// ApiInformation.RequestBodyDict["prompt"] = $"{item.Job_description}";
// string firstResponse = await GetGptResponseAsync(httpClient, ApiInformation.RequestBodyDict);

// ApiInformation.RequestBodyDict["prompt"] = "Another prompt";
// string secondResponse = await GetGptResponseAsync(httpClient, ApiInformation.RequestBodyDict);

// // ...and so on for additional prompts



// // ...and so on for additional prompts
