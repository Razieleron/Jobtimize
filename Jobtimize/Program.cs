using System.Data;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Jobtimize.Models;

namespace Jobtimize
{
    class Program 
        {public static List<GithubProject> githubProjects = new List<GithubProject>
        {
            // new GithubProject {
            //     ProjectKey = 5, 
            //     ProjectTitle = "Gif Machine", 
            //     ProjectUrl = "https://github.com/Razieleron/gif-machine", 
            //     ProjectDescription = "A website that makes Api calls to an endpoint based on a user's input and then updates the dom with the returned image.", 
            //     ProjectLanguages = "JavaScript, HTML, CSS, Babel, Eslint"},
            new GithubProject {
                ProjectKey = 6, 
                ProjectTitle = "The Event Handlers", 
                ProjectUrl = "https://github.com/Razieleron/the-event-handlers", 
                ProjectDescription = "Utilized JavaScript to Create a Side-Scrolling Shoot-em-up through calling methods on the canvas.  Sourced  all assets from free sources and built the game with Plain HTML/CSS.", 
                ProjectLanguages = "JavaScript, HTML, CSS, Markdown, Node.js, Canvas Methods"},
            // new GithubProject {
            //     ProjectKey = 7, 
            //     ProjectTitle = "Currency Exchange", 
            //     ProjectUrl = "https://github.com/Razieleron/currency-exchange", 
            //     ProjectDescription = "An application that converts UDS to other currencies by making an API call in a javascript environment.", 
            //     ProjectLanguages = "JavaScript, HTML, CSS"},
            // new GithubProject {
            //     ProjectKey = 8, 
            //     ProjectTitle = "MadLib in C#", 
            //     ProjectUrl = "https://github.com/Razieleron/MadLib-C-Sharp", 
            //     ProjectDescription = "A multi page madlib site programmed in C# using the .Net6.0 framework and Model View Controllers and AspNetCore.", 
            //     ProjectLanguages = "C#, CSHTML, .Net, MVC, AspNetCore"},
            new GithubProject {
                ProjectKey = 9, 
                ProjectTitle = "Vendor/Order Tracker", 
                ProjectUrl = "https://github.com/Razieleron/Vendor-and-Order-Tracker", 
                ProjectDescription = "A web application that allows a user to create vendors and the orders that those vendors place.", 
                ProjectLanguages = "C#, HTML, CSS, TDD, .Net, MVC, AspNetCore"},
            // new GithubProject {
            //     ProjectKey = 10, 
            //     ProjectTitle = "Hair Salon", 
            //     ProjectUrl = "https://github.com/Razieleron/Eau-Claire-s-Salon", 
            //     ProjectDescription = "A Personnel and Client Management Web Application with a persistent database in C#, using EF core and SQL", 
            //     ProjectLanguages = "C#, HTML, EF Core, .Net, SQL, AspNetCore, MVC"},
            new GithubProject {
                ProjectKey = 11, 
                ProjectTitle = "Pierre's Bakery", 
                ProjectUrl = "https://github.com/Razieleron/PierresSweetAndSavoryTreats", 
                ProjectDescription = "An application with user authentication and a many-to-many database relationship written in C#, using EF Core and mySQL", 
                ProjectLanguages = "C#, Entity Framework, SQL, AspNetCore MVC, HTML, CSS"},
            // new GithubProject {
            //     ProjectKey = 12, 
            //     ProjectTitle = "Engineer's Factory", 
            //     ProjectUrl = "https://github.com/Razieleron/Dr.-Sillystringz-s-Factory", 
            //     ProjectDescription = "This is a Many-to-Many relationship management Web Application that allows user to create Engineers and Machines and give Engineers license to work on certain machines ", 
            //     ProjectLanguages = "C#, Entity Framework, SQL, AspNetCore MVC, HTML, CSS"},
            new GithubProject {
                ProjectKey = 13, 
                ProjectTitle = "Animal Shelter API", 
                ProjectUrl = "https://github.com/Razieleron/AnimalShelterApi", 
                ProjectDescription = "A functional API connected to an EF core database.  Has versioning functionality and authentication tokens required for access to the api.", 
                ProjectLanguages = "C#, .Net, EF Core, SQL, Postman"},
            new GithubProject {
                ProjectKey = 14, 
                ProjectTitle = "DiLPr", 
                ProjectUrl = "https://github.com/Razieleron/DiLPr.Solution", 
                ProjectDescription = "A MVC app for Dogs I'd Like To Pet.  A tinder-style application with authentication, registration and login, multiple profile image uploading and storage, a swiping feature populated with existing profiles and a shared interests functionality as well", 
                ProjectLanguages = "EF core, CSS, SQL, MVC, .Net, C#, HTML"},
            new GithubProject {
                ProjectKey = 15, 
                ProjectTitle = "WordGuess!", 
                ProjectUrl = "https://github.com/Razieleron/react-word-game", 
                ProjectDescription = "A multiplayer hangman style guessing game that utilizes an api to call random words.", 
                ProjectLanguages = "React, JavaScript, HTML, CSS"},
            new GithubProject {
                ProjectKey = 16, 
                ProjectTitle = "RecipeBox", 
                ProjectUrl = "https://github.com/Razieleron/RecipeBox", 
                ProjectDescription = "A styled and built out application to track recipes and their components for a fictional hobby website.", 
                ProjectLanguages = "C#, .Net, AspNetCore MVC, SQL, HTML, CSS"},
            new GithubProject {
                ProjectKey = 17, 
                ProjectTitle = "The Coffee Cafe", 
                ProjectUrl = "https://github.com/Razieleron/ReactInventoryTracker", 
                ProjectDescription = "A React based inventory management system with add item name/details/quantity functionality, view item details functionality, and increment/decrement inventory functionality.", 
                ProjectLanguages = "React, JSX, Node.js, JavaScript, HTML, CSS"},
            new GithubProject {
                ProjectKey = 18, 
                ProjectTitle = "Jobtimize", 
                ProjectUrl = "https://github.com/Razieleron/Jobtimize", 
                ProjectDescription = "Resume and Cover letter automation program that takes scraped data and makes multiple api calls to chatGPT with engineered prompts to appropriately populate both the cover letter and resume.", 
                ProjectLanguages = "ChatGPT, APIs, C#, Packages: OpenXML, Newtonsoft.Json, CsvHelper"}
        };
        static async Task Main(string[] args)
        {
        
        string initialCoverLetterPrompt = (string)ApiInformation.CoverLetterRequestBodyDict["prompt"];
        string initialJobDescriptionDistillationPrompt = (string)ApiInformation.JobDescriptionDistillationRequestBodyDict["prompt"];
        string initialGithubProjectOrderPrompt = (string)ApiInformation.GithubProjectRequestBodyDict["prompt"];
        string serializedGithubProjects = JsonConvert.SerializeObject(githubProjects);

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
                    ApiInformation.CoverLetterRequestBodyDict["prompt"] = 
                            ApiInformation.CoverLetterRequestBodyDict["prompt"] += 
                            $"{item.Job_description}" + 
                            @"
                    
                            Please create a one-page cover letter focusing on the most relevant skills and talents from the list that match the requirements of the job description.";
                    
                    //this appends the github project prompt with the job description and the list of github objects
                    ApiInformation.GithubProjectRequestBodyDict["prompt"] = $"Based on the following job description:  {item.Job_description}" + @$"
                    
                    
                    order this list of github projects in order from most relevant to the posting to the least relevant to the posting, referring to each item only by it's 'ProjectKey' field.  As an example, your return should look like this '1, 3, 15, 4, 6' with 1 being the most relevant and 6 the least relevant.  Be sure to balance choosing projects that show the ability to accomplish the needs of the job with choosing complex projects that demonstrate overall competence and acumen.  Here is the list of github Projects:  
                    
                    {serializedGithubProjects}";
                    
                    
                    //this is the variable that contains the text from the chatGptAPI Call
                    string coverLetterResponseText = await GetGptResponseByDictAsync(httpClient, ApiInformation.CoverLetterRequestBodyDict);
                    string githubProjectOrderResponseText = await GetGptResponseByDictAsync(httpClient, ApiInformation.GithubProjectRequestBodyDict);


                    string [] projectsList = githubProjectOrderResponseText.Replace(" ", "").Split(',');
                    int firstNumber = int.Parse(projectsList[0]);
                    int secondNumber = int.Parse(projectsList[1]);
                    int thirdNumber = int.Parse(projectsList[2]);

                    GithubProject firstProjectInList = githubProjects.FirstOrDefault(p => p.ProjectKey == firstNumber);
                    GithubProject secondProjectInList = githubProjects.FirstOrDefault(p => p.ProjectKey == secondNumber);
                    GithubProject thirdProjectInList = githubProjects.FirstOrDefault(p => p.ProjectKey == thirdNumber);

                    Dictionary<string, string> replacements = new Dictionary<string, string>
                    {
                        { "Proj1Name", $"{firstProjectInList.ProjectTitle}" },
                        { "Proj1Url", $"{firstProjectInList.ProjectUrl}" },
                        { "Proj1Description", $"{firstProjectInList.ProjectDescription}" },
                        { "Proj1Languages", $"{firstProjectInList.ProjectLanguages}" },

                        { "Proj2Name", $"{secondProjectInList.ProjectTitle}" },
                        { "Proj2Url", $"{secondProjectInList.ProjectUrl}" },
                        { "Proj2Description", $"{secondProjectInList.ProjectDescription}" },
                        { "Proj2Languages", $"{secondProjectInList.ProjectLanguages}" },

                        { "Proj3Name", $"{thirdProjectInList.ProjectTitle}" },
                        { "Proj3Url", $"{thirdProjectInList.ProjectUrl}" },
                        { "Proj3Description", $"{thirdProjectInList.ProjectDescription}" },
                        { "Proj3Languages", $"{thirdProjectInList.ProjectLanguages}" },
                    };

                    Console.WriteLine("githubProjectOrderResponseText = " + githubProjectOrderResponseText);

                    string inputFilePath = "./../Jobtimize/ScrapedData/templatedResume.docx";
                    string jobDetailsText = $"Company: {item.Company}\r\nLocation: {item.Location}\r\nJob Title: {item.Job_title}\r\nLink to posting: {item.Job_link}\r\nJob Seniority Level (not always accurate): {item.Seniority_level}\r\nJob Description :\r\n{item.Job_description}";

                    string resumeFilePath = AssembleFolderPathString(item) + "\\" + CreateResumeName(item);
                    string coverLetterFilePath = AssembleFolderPathString(item) + "\\" + CreateCoverLetterName(item);
                    string jobDetailsFilePath = AssembleFolderPathString(item) + "\\" + CreateJobDetailsName(item);

                    CreateDirectory(AssembleFolderPathString(item));

                    DuplicateDocument(inputFilePath, resumeFilePath);
                    ReplaceTextInWordDocument(resumeFilePath, replacements);

                    CreateWordDocument(coverLetterFilePath, coverLetterResponseText);
                    CreateWordDocument(jobDetailsFilePath, jobDetailsText);
                    
                    WriteJobInformationToTheConsole(item);
                    Thread.Sleep(5000);
                    Console.WriteLine(); 
                }
                
            }
        }

    private static string AssembleFolderPathString(JobItem item)
    {
    return $"CreatedFiles/{item.Company} - {item.Location} - {item.Job_title}";
    }
    private static string CreateJobDetailsName(JobItem item)
    {
        return $"Job Details for - {item.Company} - {item.Location} - {item.Job_title}.docx";
    }
    private static string CreateResumeName(JobItem item)
    {
        return $"Resume - {item.Company} - {item.Location} - {item.Job_title}.docx";
    }
    private static string CreateCoverLetterName(JobItem item)
    {
        return $"Cover Letter - {item.Company} - {item.Location} - {item.Job_title}.docx";
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

            // Create a new body.
            Body body = new Body();

            // Split the text into paragraphs on newline characters.
            string[] paragraphs = bodyText.Split('\n');
            foreach (string para in paragraphs)
                {
                // Create a new paragraph for each line of text.
                Paragraph paragraph = new Paragraph();            
                Run run = new Run();
                Text text = new Text(para);
            
                // Assemble the document structure.
                run.Append(text);
                paragraph.Append(run);
                body.Append(paragraph);
                }

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

