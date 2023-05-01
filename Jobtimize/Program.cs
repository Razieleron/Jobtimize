using System.Xml.XPath;
using System.Net.Http.Headers;
using System.Data;
using System.Runtime.CompilerServices;
using System;
using System.IO;
using Newtonsoft.Json;

class JobItem
{
    public string Keyword { get; set; }
    public string Job_description { get ; set; }
    public string Job_title { get; set; }
    public string Job_link { get; set; }
    public string Location { get; set; }
    public string Company { get; set; }
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

        string jsonFilePath = "ScrapedData/JobData.json";
        string jsonData = File.ReadAllText(jsonFilePath);
        List<JobItem> jobItems = JsonConvert.DeserializeObject<List<JobItem>>(jsonData);


        // Iterate through each item in the list and print the 'Job_description' property to the console, check if any of the skills in the ExistingSkills.csv file exist in the job description, and if they do, write that to the console
        foreach (JobItem item in jobItems)
        {
            // Console.WriteLine($"Job description: {item.Job_description}");
            Console.WriteLine($"Company: {item.Company}   Title: {item.Job_title}    Location: {item.Location}    Job Description: {item.Job_description}");
            foreach (string skill in existingSkills)
            {
                if (item.Job_description.Contains(skill, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Skill found: {skill}");
                }
            }

            Console.WriteLine(); // Add an empty line for better readability
        }
    }
}