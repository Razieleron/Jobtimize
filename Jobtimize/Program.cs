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
        {public static List<GithubProject> githubProjects = new List<GithubProject>
        {
            new GithubProject { 
                ProjectTitle = "Pig Dice", 
                ProjectUrl = "https://github.com/Razieleron/pig-dice", 
                ProjectDescription = "Two Player Dice Game", 
                ProjectLanguages = "JavaScript, HTML, CSS"},
            new GithubProject { 
                ProjectTitle = "Programming Quiz", 
                ProjectUrl = "https://github.com/Razieleron/week-2-coding-review", 
                ProjectDescription = "This is a simple choice tree exercise using the medium of a 'Quiz'. Asking the user for inputs will return a value that, depending on the choices made, will return the 'best' programming language for that user to learn.", 
                ProjectLanguages = "Event Handlers/Listeners, Event Objects, Function Expressions, Accessing HTML Element Attributes and Properties in the DOM, Forms, Branching, and Locally/Functionally Scoped Variables"},
            new GithubProject { 
                ProjectTitle = "Mister Roboger's Neighborhood", 
                ProjectUrl = "https://github.com/Razieleron/week-3-coding-review", 
                ProjectDescription = "An exercise in arrays, branching, and loops in string manipulation.", 
                ProjectLanguages = "JavaScript, HTML, CSS, Test Driven Development"},
            new GithubProject { 
                ProjectTitle = "Super Galactic Age Calculator", 
                ProjectUrl = "https://github.com/Razieleron/super-galactic-age-calculator", 
                ProjectDescription = "This Application Calculates your age in various solar years - (Earth, Mercury, Venus, Mars, Jupiter).", 
                ProjectLanguages = "JavaScript, HTML, CSS"},
            new GithubProject { 
                ProjectTitle = "Gif Machine", 
                ProjectUrl = "https://github.com/Razieleron/gif-machine", 
                ProjectDescription = "A website that makes Api calls to an endpoint based on a user's input and then updates the dom with the returned image.", 
                ProjectLanguages = "JavaScript, HTML, CSS, Babel, Eslint"},
            new GithubProject { 
                ProjectTitle = "The Event Handlers", 
                ProjectUrl = "https://github.com/Razieleron/the-event-handlers", 
                ProjectDescription = "Utilized JavaScript to Create a Side-Scrolling Shoot-em-up through calling methods on the canvas.  Sourced  all assets from free sources and built the game with Plain HTML/CSS.", 
                ProjectLanguages = "JavaScript, HTML, CSS, Markdown, Node.js, Canvas Methods"},
            new GithubProject { 
                ProjectTitle = "Currency Exchange", 
                ProjectUrl = "https://github.com/Razieleron/currency-exchange", 
                ProjectDescription = "An application that converts UDS to other currencies by making an API call in a javascript environment.", 
                ProjectLanguages = "JavaScript, HTML, CSS"},
            new GithubProject { 
                ProjectTitle = "MadLib in C#", 
                ProjectUrl = "https://github.com/Razieleron/MadLib-C-Sharp", 
                ProjectDescription = "A multi page madlib site programmed in C# using the .Net6.0 framework and Model View Controllers and AspNetCore.", 
                ProjectLanguages = "C#, CSHTML, .Net, MVC, AspNetCore"},
            new GithubProject { 
                ProjectTitle = "Vendor and Order Tracker", 
                ProjectUrl = "https://github.com/Razieleron/Vendor-and-Order-Tracker", 
                ProjectDescription = "A web application that allows a user to create vendors and the orders that those vendors place.", 
                ProjectLanguages = "C#, HTML, CSS, TDD, .Net, MVC, AspNetCore"},
            new GithubProject { 
                ProjectTitle = "Eau Claire's Salon", 
                ProjectUrl = "https://github.com/Razieleron/Eau-Claire-s-Salon", 
                ProjectDescription = "A Personnel and Client Management Web Application with a persistent database in C#, using EF core and SQL", 
                ProjectLanguages = "C#, HTML, EF Core, .Net, SQL, AspNetCore, MVC"},
            new GithubProject { 
                ProjectTitle = "Pierre's Sweet and Savory Treats", 
                ProjectUrl = "https://github.com/Razieleron/PierresSweetAndSavoryTreats", 
                ProjectDescription = "An application with user authentication and a many-to-many database relationship written in C#, using EF Core and mySQL", 
                ProjectLanguages = "C#, Entity Framework, SQL, AspNetCore MVC, HTML, CSS"},
            new GithubProject { 
                ProjectTitle = "Dr Sillystrings' Factory", 
                ProjectUrl = "https://github.com/Razieleron/Dr.-Sillystringz-s-Factory", 
                ProjectDescription = "This is a Many-to-Many relationship management Web Application that allows user to create Engineers and Machines and also give Engineers license to work on certain machines ", 
                ProjectLanguages = "JavaScript, HTML, CSS"}
            // new GithubProject { 
            //     ProjectTitle = "Pig Dice", 
            //     ProjectUrl = "https://github.com/Razieleron/pig-dice", 
            //     ProjectDescription = "Two Player Dice Game", 
            //     ProjectLanguages = "JavaScript, HTML, CSS"}
        };
        static async Task Main(string[] args)
        {

        
        // GithubProject PigDice = new GithubProject("Pig Dice", "https://github.com/Razieleron/pig-dice", "Two Player Dice Game", "JavaScript, HTML, CSS");
        GithubProject Dilpr = new GithubProject("Dilpr", "https://github.com/Razieleron/Dilpr", "Social media app for dogs", "C#, Razor Markup, SQL");
        GithubProject Name = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name2 = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name3 = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name4 = new GithubProject("Name", "url", "description", "languages/technologies");
        GithubProject Name5 = new GithubProject("Name", "url", "description", "languages/technologies");


        
        string initialCoverLetterPrompt = (string)ApiInformation.CoverLetterRequestBodyDict["prompt"];
        string initialJobDescriptionDistillationPrompt = (string)ApiInformation.JobDescriptionDistillationRequestBodyDict["prompt"];
        string initialGithubProjectOrderPrompt = (string)ApiInformation.GithubProjectRequestBodyDict["prompt"];

        foreach (JobItem item in JobItemParameters.JobItems)
        {     
            int numberOfSkills = 0;
            ApiInformation.CoverLetterRequestBodyDict["prompt"] = initialCoverLetterPrompt;
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
                    
                        //distills the job description
                    string distilledJobDescription = await GetGptResponseByDictAsync(httpClient, ApiInformation.JobDescriptionDistillationRequestBodyDict);
                    
                        //this appends the cover letter prompt with the job description from the json file
                    ApiInformation.CoverLetterRequestBodyDict["prompt"] = 
                            ApiInformation.CoverLetterRequestBodyDict["prompt"] += 
                            $"{item.Job_description}" + 
                            @"
                    
                            Please create a one-page cover letter focusing on the most relevant skills and talents from the list that match the requirements of the job description.";
                        
                        //this is the variable that contains the text from the chatGptAPI Call
                    string coverLetterResponseText = await GetGptResponseByDictAsync(httpClient, ApiInformation.CoverLetterRequestBodyDict);

                    string githubProjectOrderResponseText = await GetGptResponseByDictAsync(httpClient, ApiInformation.GithubProjectRequestBodyDict);
                    Console.WriteLine("githubProjectOrderResponseText = " + githubProjectOrderResponseText);
















                    Dictionary<string, string> replacements = new Dictionary<string, string>
                    {
                        { "Proj1Name", "John" },
                        { "Proj1Url", "Doe" },
                        { "Proj1Description", "john.doe@example.com" },
                    };


                    string inputFilePath = "./../Jobtimize/ScrapedData/templatedResume.docx";
                    // string proj1Table = "Proj1Name";
                    // string proj1Url = "Proj1Url";
                    // string proj1Description = "Proj1Description";
                    // string proj1Languages = "Proj1Languages";

                    string resumeFilePath = AssembleFolderPathString(item) + "\\" + CreateResumeName(item) + ".docx";
                    string coverLetterFilePath = AssembleFolderPathString(item) + "\\" + CreateCoverLetterName(item) + ".docx";

                    CreateDirectory(AssembleFolderPathString(item));

                    DuplicateDocument(inputFilePath, resumeFilePath);
                    ReplaceTextInWordDocument(resumeFilePath, replacements);

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
    public static void DuplicateDocument(string inputFilePath, string outputFilePath)
    {
        File.Copy(inputFilePath, outputFilePath, true);
    }
    public static void ReplaceTextInWordDocument(string filePath, Dictionary<string, string> replacements)
    {
        using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
        {
            Body body = doc.MainDocumentPart.Document.Body;

            foreach (var replacement in replacements)
            {
                string textToReplace = replacement.Key;
                string newText = replacement.Value;

                var runsWithTextToReplace = body.Descendants<Run>()
                    .Where(run => run.InnerText.Contains(textToReplace))
                    .ToList();

                foreach (var run in runsWithTextToReplace)
                {
                    string replacedText = run.InnerText.Replace(textToReplace, newText);
                    run.RemoveAllChildren<Text>();
                    run.AppendChild(new Text(replacedText));
                }
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
        string responseText = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent).choices[0].text;

        return responseText;
    }

    
    }

}

