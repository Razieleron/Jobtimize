using System.Data;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Jobtimize.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace Jobtimize
{
    class Program 
    {
        static async Task Main(string[] args)
        {
        GithubProject PigDice = new GithubProject("Pig Dice", "https://github.com/Razieleron/pig-dice", "Two Player Dice Game", "JavaScript, HTML, CSS");
        GithubProject Dilpr = new GithubProject("Dilpr", "https://github.com/Razieleron/Dilpr", "Social media app for dogs", "C#, Razor Markup, SQL");
        GithubProject Name = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name2 = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name3 = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name4 = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name5 = new GithubProject("Name", "url", "description", "languages/technologies");


        
        // string initialCoverLetterPrompt = (string)ApiInformation.CoverLetterRequestBodyDict["prompt"];
        string initialJobDescriptionDistillationPrompt = (string)ApiInformation.JobDescriptionDistillationRequestBodyDict["prompt"];
        string initialGithubProjectOrderPrompt = (string)ApiInformation.GithubProjectRequestBodyDict["prompt"];

        foreach (JobItem item in JobItemParameters.JobItems)
        {     
            int numberOfSkills = 0;
            // ApiInformation.CoverLetterRequestBodyDict["prompt"] = initialCoverLetterPrompt;
            ApiInformation.JobDescriptionDistillationRequestBodyDict["prompt"] = initialJobDescriptionDistillationPrompt;
            ApiInformation.GithubProjectRequestBodyDict["prompt"] = initialGithubProjectOrderPrompt;

            foreach (string skill in ExistingSkills.List)

                if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                {
                    numberOfSkills += 1;
                }
                if (numberOfSkills >=5)
                {
                    HttpClient httpClient = new HttpClient();

                    string gpt4Response = await GetGptResponseAsync(ApiInformation.CoverLetterRequestBodyDict);
                    Console.WriteLine("gpt4 response = " + gpt4Response);

                        //appends the 
                    ApiInformation.JobDescriptionDistillationRequestBodyDict["prompt"] = ApiInformation.JobDescriptionDistillationRequestBodyDict["prompt"] + $"Company: {item.Job_description}   Title: {item.Job_title}    Location: {item.Location}    Job Description: {item.Job_description}";
                        //distills the job description
                    string distilledJobDescription = await GetGptResponseByDictAsync(httpClient, ApiInformation.JobDescriptionDistillationRequestBodyDict);
                    Console.WriteLine("distilledJobDescription = " + distilledJobDescription);
                        //concats the distilled job description to the end of the predefined cover letter prompt
                    ApiInformation.CoverLetterRequestBodyDict["prompt"] = ApiInformation.CoverLetterRequestBodyDict["prompt"] += $"{item.Job_description}";
                        //this is the variable that contains the text from the chatGptAPI Call
                    string coverLetterResponseText = await GetGptResponseByDictAsync(httpClient, ApiInformation.CoverLetterRequestBodyDict);

                    string githubProjectOrderResponseText = await GetGptResponseByDictAsync(httpClient, ApiInformation.GithubProjectRequestBodyDict);
                    Console.WriteLine("githubProjectOrderResponseText = " + githubProjectOrderResponseText);



















                    string inputFilePath = "./../Jobtimize/ScrapedData/templatedResume.docx";
                    string proj1Table = "Proj1Name";
                    // string proj1Url = "Proj1Url";
                    // string proj1Description = "Proj1Description";
                    // string proj1Languages = "Proj1Languages";

                    string resumeFilePath = AssembleFolderPathString(item) + "\\" + CreateResumeName(item) + ".docx";
                    string coverLetterFilePath = AssembleFolderPathString(item) + "\\" + CreateCoverLetterName(item) + ".docx";

                    CreateDirectory(AssembleFolderPathString(item));
                    DuplicateDocumentAndThenReplaceTextInWordDocument(inputFilePath, resumeFilePath, proj1Table, PigDice.ProjectTitle);
                    CreateWordDocument(coverLetterFilePath, coverLetterResponseText);
                    WriteJobInformationToTheConsole(item);

                    Thread.Sleep(3000);
                    Console.WriteLine(); 
                    // Add an empty line for better readability 
                }
                
            }
        }

    private static string AssembleFolderPathString(JobItem item)
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
    private static void CreateDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine($"Directory '{directoryPath}' created successfully");
        }
    }
    private static void CreateWordDocument(string outputFilePath, string bodyText)
    {
        using (WordprocessingDocument doc = WordprocessingDocument.Create(outputFilePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
        {
            // Add a main document part.
            MainDocumentPart mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document();

            // Create a new body and add content.
            Body body = new Body();
            Paragraph paragraph = new Paragraph();
            Run run = new Run();
            Text text = new Text(bodyText);

            // Assemble the document structure.
            run.Append(text);
            paragraph.Append(run);
            body.Append(paragraph);

            // Add the body to the document.
            mainPart.Document.Append(body);

            // Save the document.
            mainPart.Document.Save();
        }
    }
    private static void WriteJobInformationToTheConsole(JobItem item)
    {
        Console.WriteLine($"Company: {item.Company}   Title: {item.Job_title}    Location: {item.Location}    Job Description: {item.Job_description}");
        foreach (string skill in ExistingSkills.List)
        {
            if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Skill found: {skill}");
            }
        }
        Console.WriteLine(item.Seniority_level);
    }
    private static void DuplicateDocumentAndThenReplaceTextInWordDocument(string inputFilePath, string outputFilePath, string textToReplace, string newText)
    {
        System.IO.File.Copy(inputFilePath, outputFilePath, true);
        using(WordprocessingDocument doc = WordprocessingDocument.Open(outputFilePath, true))
        {
            // get the body of the document
            Body body = doc.MainDocumentPart.Document.Body;

            // find all the run elements containing the text to replace
            var runsWithTextToReplace = body.Descendants<Run>()
                                            .Where(run => run.InnerText.Contains(textToReplace))
                                            .ToList();
            // replace the text in each Run element
            foreach (var run in runsWithTextToReplace)
            {
                string replacedText =   run.InnerText.Replace(textToReplace, newText);
                                        run.RemoveAllChildren<Text>();
                                        run.AppendChild(new Text(replacedText));
            }
            doc.MainDocumentPart.Document.Save();
        }
    }
    private static async Task<string> GetGptResponseByDictAsync(HttpClient httpClient, Dictionary<string, object> requestBodyDict)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ApiInformation.ApiUrl);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiInformation.ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(requestBodyDict), System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        string responseText = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent).choices[0].message.content;

        return responseText;
    }

    public static async Task<string> GetGptResponseAsync(Dictionary<string, object> requestBodyDict)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ApiInformation.ApiUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiInformation.ApiKey);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestBodyDict), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Raw JSON response:" + responseContent);
            ChatGptResponse chatGptResponse = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent);

            // Print the deserialized response for debugging purposes
            Console.WriteLine("Deserialized response: " + JsonConvert.SerializeObject(chatGptResponse, Formatting.Indented));

            if (chatGptResponse.choices != null && chatGptResponse.choices.Count > 0)
            {
                string responseText = chatGptResponse.choices[0].message.content;
                return responseText;
            }
            else
            {
                Console.WriteLine("No choices found in the response.");
                return null;
            }
        }
    }

    
    }

}
