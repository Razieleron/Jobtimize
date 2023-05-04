public class ChatGptResponse
{
    public List<Choice> choices { get; set; }
}

public class Choice
{
    public string text { get; set; }
    // public double? logprobs { get; set; }
    // public double? finish_reason { get; set; }
}
