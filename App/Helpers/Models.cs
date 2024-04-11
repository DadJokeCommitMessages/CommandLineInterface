using System.Text.Json;
using System.Text.Json.Serialization;

public class JokePostRequest
{
    public string story { get; private set; }

    public string jokeType { get; private set; }

    public JokePostRequest(string story, string jokeType)
    {
        this.story = story;
        this.jokeType = jokeType;
    }

    public string ToJsonString()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class JokeResponse
{
    public int jokeID { get; set; }
    public string story { get; set; }
    public string jokeType { get; set; }

    public JokeResponse(int jokeID, string story, string jokeType)
    {
        this.jokeID = jokeID;
        this.story = story;
        this.jokeType = jokeType;
    }

}

public class JokeType
{
    public int jokeID { get; set; }
    public string jokeType { get; set; }

    public JokeType(int jokeID,string jokeType)
    {
        this.jokeID = jokeID;
        this.jokeType = jokeType;
    }
}