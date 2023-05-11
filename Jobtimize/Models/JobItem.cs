using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jobtimize.Models
{

  class JobItemParameters
  {
    public static string JsonFilePath = "modified_PortlandData.json";
    public static List<JobItem> JobItems = 
              JsonConvert.
              DeserializeObject<List<JobItem>>
              (File.ReadAllText(JsonFilePath));
  }

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

}