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


class JobItem
{
    public string Keyword { get; set; }
    public string Location { get; set; }
    public string Job_title { get; set; }
    public string Job_link { get; set; }
    public string Company { get; set; }
    public string Company_link { get; set; }
    public string Job_location { get; set; }
    public string Job_description { get ; set; }
    public string Seniority_level { get; set; }
    public string Employment_level { get; set; }
    public string Job_function { get; set; }
    public string Industries { get; set; }
    public string Person_hiring { get; set; }
}

class Program 
{


    static async Task Main(string[] args)
    {
        //this is the api thingy
        HttpClient httpClient = new HttpClient();
        string apiUrl = "https://api.openai.com/v1/completions";

        //this is the secret to using functions and stuff from other files:
        string apiKey = EnvironmentalVariables.ApiKey;


        
        string modelName = "text-davinci-003";


        string filePath = "ScrapedData/ExistingSkills.csv";
        List<string> existingSkills = new List<string>();
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                existingSkills.Add(line);
                Console.WriteLine(line);

            }
        }

        foreach(string item in existingSkills)
        {
            Console.WriteLine(item);
        }

        string jsonFilePath = "modified_aggregate_data.json";
        string jsonData = File.ReadAllText(jsonFilePath);
        List<JobItem> jobItems = JsonConvert.DeserializeObject<List<JobItem>>(jsonData);



        // Iterate through each item in the list and print the 'Job_description' property to the console, check if any of the skills in the ExistingSkills.csv file exist in the job description, and if they do, write that to the console

        foreach (JobItem item in jobItems)
        {
            int numberOfSkills = 0;
            foreach (string skill in existingSkills)
                if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                {
                    numberOfSkills += 1;
                }
                if (numberOfSkills >=5 && item.Seniority_level.Contains("entry level", StringComparison.OrdinalIgnoreCase))
                {
                string folderPath = ($"CreatedFiles/{item.Company} - {item.Location} - {item.Job_title}");
                string modifiedJson = JsonConvert.SerializeObject(jobItems, Formatting.Indented);
                string fileName = ($"{item.Company} - {item.Location} - {item.Job_title}");  
                string fileNameGPTResponse = ($"GPT RESPONSE for {item.Company} - {item.Location} - {item.Job_title}");
                string outputPath = Path.Combine(folderPath, fileName + ".docx");
                string gptOutputPath = Path.Combine(folderPath, fileNameGPTResponse + "docx");
                var document = DocX.Create(outputPath);  
                var documentGPTResponse = DocX.Create(gptOutputPath);





                string prompt = $"write me a cover letter for the following job description: {item.Job_description}.  Only return to me the text for the cover letter";
                // string responseContent = await response.Content.ReadAsStringAsync();

                var requestBodyDict = new Dictionary<string, object>()
                {
                    // { "messages", new List<object>()
                    //     {
                    //         new Dictionary<string, string>()
                    //     {
                    //         { "role", role },
                    //         { "content", prompt }
                    //     }
                    //     }
                    // },
                    {"prompt", prompt},
                    {"max_tokens", 50 },
                    {"model", modelName },
                    {"temperature", 0.5},
                    {"top_p", 1},
                    {"frequency_penalty", 0},
                    {"presence_penalty", 0},
                    {"stop", null}
                };

                // serialize the dictionary to a JSON string

                string requestBodyJson = JsonConvert.SerializeObject(requestBodyDict);
                
                // create the HTTP request message
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
                request.Content = new StringContent(requestBodyJson, System.Text.Encoding.UTF8, "application/json");
            
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
                foreach (string skill in existingSkills)
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
                    item.Job_description);
                document.Save();  


                documentGPTResponse.InsertParagraph("this is chatGPT's Response" + responseText);
                documentGPTResponse.Save();


                Console.WriteLine($"Company: {item.Company}   Title: {item.Job_title}    Location: {item.Location}    Job Description: {item.Job_description}");
                foreach (string skill in existingSkills)
                {
                    if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Skill found: {skill}");
                    }
                }
                Console.WriteLine(item.Seniority_level);
                
                Thread.Sleep(1000);
                Console.WriteLine(); 
                // Add an empty line for better readability
            }
        }
    }
}