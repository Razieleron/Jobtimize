namespace Jobtimize.Models;

public class GptMessage
{
    public string role { get; set; }
    public string content { get; set; }
}

public class GptChoice
{
    public GptMessage message { get; set; }
    public string finish_reason { get; set; }
    public int index { get; set; }
}

public class ChatGptResponse
{
    public string id { get; set; }
    public string object_type { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public Dictionary<string, int> usage { get; set; }
    public List<GptChoice> choices { get; set; }
}


