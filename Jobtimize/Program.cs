using System.Data;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Jobtimize.Models;

namespace Jobtimize
{
    class Program 
    {
        static async Task Main(string[] args)
        {
        GithubProject PigDice = new GithubProject("Pig Dice", "https://github.com/Razieleron/pig-dice", "Two Player Dice Game", "JavaScript, HTML, CSS");
        GithubProject Dilpr = new GithubProject("Dilpr", "https://github.com/Razieleron/Dilpr", "Social media app for dogs", "C#, Razor Markup, SQL");


        HttpClient httpClient = new HttpClient();
        string initialPrompt = (string)ApiInformation.RequestBodyDict["prompt"];

        foreach (JobItem item in JobItemParameters.JobItems)
        {     
            int numberOfSkills = 0;
            ApiInformation.RequestBodyDict["prompt"] = initialPrompt;

            foreach (string skill in ExistingSkills.List)

                if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                {
                    numberOfSkills += 1;
                }
                if (numberOfSkills >=5)
                {
                    
                    ApiInformation.RequestBodyDict["prompt"] = ApiInformation.RequestBodyDict["prompt"] += $"{item.Job_description}";
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ApiInformation.ApiUrl);
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiInformation.ApiKey);
                    request.Content = new StringContent(JsonConvert.SerializeObject(ApiInformation.RequestBodyDict),System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    string responseText = JsonConvert.DeserializeObject<ChatGptResponse>(responseContent).choices[0].text;


                    // Console.WriteLine($"Cover Letter Content - {responseContent}");

                    string inputFilePath = "./../Jobtimize/ScrapedData/templatedResume.docx";
                    string proj1Table = "Proj1Table";

                    string resumeFilePath = AssembleFolderPathString(item) + "\\" + CreateResumeName(item) + ".docx";
                    string coverLetterFilePath = AssembleFolderPathString(item) + "\\" + CreateCoverLetterName(item) + ".docx";

                    CreateDirectory(AssembleFolderPathString(item));
                    DuplicateDocumentAndThenReplaceTextInWordDocument(inputFilePath, resumeFilePath, proj1Table, PigDice.ProjectTitle);
                    CreateWordDocument(coverLetterFilePath, responseText);
                    WriteJobInformationToTheConsole(item);


                    List<string> wordsToFind = ExistingSkills.List;

                    Dictionary<string, int> wordCounts = CountWordOccurrences(item.Job_description, wordsToFind);

                    var filteredWordCounts = wordCounts.Where(entry => entry.Value >= 1);

                    foreach (var fish in filteredWordCounts)
                    {
                        Console.WriteLine($"Word: {fish.Key} - Count: {fish.Value}");
                    }


                    Thread.Sleep(3000);
                    Console.WriteLine(); 
                    // Add an empty line for better readability 
                }
                
            }
        }


        static Dictionary<string, int> CountWordOccurrences(string inputText, List<string> wordsToFind)
        {
            string[] words = inputText.ToLower().Split(new[] { ' ', ',', '.', ';', ':', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (string wordToFind in wordsToFind)
            {
                int count = 0;
                foreach (string word in words)
                {
                    if (word.Equals(wordToFind, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                    }
                }
                wordCounts[wordToFind] = count;
            }

            return wordCounts;
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


    
    
    }

}
