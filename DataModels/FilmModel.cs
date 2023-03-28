using System.Text.Json.Serialization;


class FilmModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("filmName")]
    public string filmName { get; set; }

    [JsonPropertyName("filmDate")]
    public string filmDate { get; set; }

    [JsonPropertyName("filmTime")]
    public string filmTime { get; set; }

    public FilmModel(int id, string filmname, string filmdate, string filmtime)
    {
        Id = id;
        filmName = filmname;
        filmDate = filmdate;
        filmTime = filmtime;
    }
}


