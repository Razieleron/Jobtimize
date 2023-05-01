using System;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using System.Xml.XPath;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using Xceed.Words.NET;  
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


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
    static void Main(string[] args)
    {
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

        string jsonFilePath = "modified_JobData.json";
        string jsonData = File.ReadAllText(jsonFilePath);
        List<JobItem> jobItems = JsonConvert.DeserializeObject<List<JobItem>>(jsonData);


        // Iterate through each item in the list and print the 'Job_description' property to the console, check if any of the skills in the ExistingSkills.csv file exist in the job description, and if they do, write that to the console
        foreach (JobItem item in jobItems)
        {
            
            string modifiedJson = JsonConvert.SerializeObject(jobItems, Formatting.Indented);
            string folderPath = "CreatedFiles";
            string fileName = ($"{item.Company} - {item.Location} - {item.Job_title}");  
            string outputPath = Path.Combine(folderPath, fileName + ".docx");
            var document = DocX.Create(outputPath);  
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



            Console.WriteLine($"Company: {item.Company}   Title: {item.Job_title}    Location: {item.Location}    Job Description: {item.Job_description}");
            foreach (string skill in existingSkills)
            {
                if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Skill found: {skill}");
                }
            }

            Console.WriteLine(); 
            // Add an empty line for better readability
        }
    }
}