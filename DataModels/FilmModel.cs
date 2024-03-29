using System.Text.Json.Serialization;


public class FilmModel
{
    private static int _nextId;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("filmName")]
    public string filmName { get; set; }

    [JsonPropertyName("filmDescription")]
    public string filmDescription { get; set; }

    [JsonPropertyName("filmDate")]
    public string filmDate { get; set; }

    [JsonPropertyName("filmTime")]
    public string filmTime { get; set; }

    [JsonPropertyName("filmRoom")]
    public string filmRoom { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    public FilmModel()
    {
        Id = NextId();
    }
    
    public FilmModel(string filmname, string filmdescription, string filmdate, string filmtime, string filmroom)
    {
        Id = CurrentId;
        filmName = filmname;
        filmDescription = filmdescription;
        filmDate = filmdate;
        filmTime = filmtime;
        filmRoom = filmroom;
        Active = true;

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
