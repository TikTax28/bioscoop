using System.Text.Json.Serialization;


public class FilmModel
{
    private static int _nextId;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("filmName")]
    public string filmName { get; set; }

    [JsonPropertyName("filmDate")]
    public string filmDate { get; set; }

    [JsonPropertyName("filmTime")]
    public string filmTime { get; set; }

    public FilmModel()
    {
        Id = NextId();
    }
    
    public FilmModel(string filmname, string filmdate, string filmtime)
    {
        Id = CurrentId;
        filmName = filmname;
        filmDate = filmdate;
        filmTime = filmtime;

        _nextId = Id + 1;
    }

    private static int NextId()
    {
        return ++_nextId;
    }

    public static int CurrentId
    {
        get { return _nextId; }
        set { _nextId = value; }
    }
}
