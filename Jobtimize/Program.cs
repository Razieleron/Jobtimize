using System;
using System.IO;
using System.Data;
using System.Text;
using Xceed.Words.NET;  
using Newtonsoft.Json;
using System.Xml.XPath;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Word = Microsoft.Office.Interop.Word;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Diagnostics;
using ConsoleApiCall.Keys;
using Jobtimize.Models;

namespace Jobtimize
{
    class Program 
    {
    private static string CreateFolderPath(JobItem item)
    {
    return $"CreatedFiles/{item.Company} - {item.Location} - {item.Job_title}";
    }
    private static string CreateResumeName(JobItem item)
    {
        return $"Resume - {item.Company} - {item.Location} - {item.Job_title}";
    }
    private static string CreateCoverLetterName(JobItem item)
            {
                return $"Cover Letter - {item.Company} - {item.Location} - {item.Job_title}";
            }
        
        static async Task Main(string[] args)
        {
        HttpClient httpClient = new HttpClient();

        foreach (JobItem item in JobItemParameters.JobItems)
        {
            int numberOfSkills = 0;
            
            foreach (string skill in ExistingSkills.List)
                if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                {
                    numberOfSkills += 1;
                }
                if (numberOfSkills >=5)
                {

                var documentResume = DocX.Create(Path.Combine(CreateFolderPath(item), CreateResumeName(item) + ".docx"));  
                var documentCoverLetter = DocX.Create(Path.Combine(CreateFolderPath(item), CreateCoverLetterName(item) + ".docx"));



                ApiInformation.RequestBodyDict["prompt"] = ApiInformation.RequestBodyDict["prompt"] += $"{item.Job_description}";
                Console.WriteLine(ApiInformation.RequestBodyDict["prompt"]);




                // create the HTTP request message
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ApiInformation.ApiUrl);

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiInformation.ApiKey);

                request.Content = new StringContent(JsonConvert.SerializeObject(ApiInformation.RequestBodyDict), System.Text.Encoding.UTF8, "application/json");
            
                // send the HTTP request
                HttpResponseMessage response = await httpClient.SendAsync(request);
            
                // read the response content as a string
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent);
                string responseText = responseObject.choices[0].text;
                // display the response content

                Console.WriteLine($"ChatGPT response for the prompt: {responseContent}");


                Directory.CreateDirectory(CreateFolderPath(item));
                
                documentResume.InsertParagraph("Here are the Applicant's Skills:");
                foreach (string skill in ExistingSkills.List)
                {
                    if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                    {
                    documentResume.InsertParagraph(skill);
                    }
                }
                documentResume.InsertParagraph("This is the Job Description");
                documentResume.InsertParagraph(
                    item.Company + " " + 
                    item.Location + " " + 
                    item.Job_title + " " + 
                    item.Company_link + " " +
                    item.Job_link + " " +
                    item.Job_description);
                documentResume.Save();  


                documentCoverLetter.InsertParagraph(responseText);
                documentCoverLetter.Save();


                Console.WriteLine($"Company: {item.Company}   Title: {item.Job_title}    Location: {item.Location}    Job Description: {item.Job_description}");
                foreach (string skill in ExistingSkills.List)
                {
                    if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Skill found: {skill}");
                    }
                }
                Console.WriteLine(item.Seniority_level);
                
                Thread.Sleep(3000);
                Console.WriteLine(); 
                // Add an empty line for better readability
                }
                
            }
        }
    }
}
