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
        static async Task Main(string[] args)
        {
            //this is the api thingy
            HttpClient httpClient = new HttpClient();

            // Iterate through each item in the list and print the 'Job_description' property to the console, check if any of the skills in the ExistingSkills.csv file exist in the job description, and if they do, write that to the console

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
                    string folderPath = ($"CreatedFiles/{item.Company} - {item.Location} - {item.Job_title}");
                    string modifiedJson = JsonConvert.SerializeObject(JobItemParameters.JobItems, Formatting.Indented);


                    string fileName = ($"Resume - {item.Company} - {item.Location} - {item.Job_title}");  
                    string outputPath = Path.Combine(folderPath, fileName + ".docx");
                    var document = DocX.Create(outputPath);  
                    
                    string fileNameGPTResponse = ($"Cover Letter - {item.Company} - {item.Location} - {item.Job_title}");
                    string gptOutputPath = Path.Combine(folderPath, fileNameGPTResponse + ".docx");
                    var documentGPTResponse = DocX.Create(gptOutputPath);





                    string CombinedPrompt = ApiInformation.prompt + $"{item.Job_description}";

                    var requestBodyDict = new Dictionary<string, object>()
                    {
                        {"prompt", prompt}, 
                        {"max_tokens", 50 },
                        {"model", modelName },
                        {"temperature", 0.5},
                        {"top_p", 1},
                        {"frequency_penalty", 0},
                        {"presence_penalty", 0},
                        {"stop", null}
                    };


                    // create the HTTP request message
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ApiInformation.ApiUrl);

                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                    request.Content = new StringContent(JsonConvert.SerializeObject(ApiInformation.RequestBodyDict), System.Text.Encoding.UTF8, "application/json");
                
                    // send the HTTP request
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                
                    // read the response content as a string
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent);
                    string responseText = responseObject.choices[0].text;
                    // display the response content

                    Console.WriteLine($"ChatGPT response for {prompt}: {responseContent}");



                    Directory.CreateDirectory(folderPath);
                    
                    document.InsertParagraph("Here are the Applicant's Skills:");
                    foreach (string skill in ExistingSkills.List)
                    {
                        if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                        {
                        document.InsertParagraph(skill);
                        }
                    }
                    document.InsertParagraph("This is the Job Description");
                    document.InsertParagraph(
                        item.Company + " " + 
                        item.Location + " " + 
                        item.Job_title + " " + 
                        item.Company_link + " " +
                        item.Job_link + " " +
                        item.Job_description);
                    document.Save();  


                    documentGPTResponse.InsertParagraph("this is chatGPT's Response" + responseText);
                    documentGPTResponse.Save();


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