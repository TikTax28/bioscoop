using System.Text.Json;

static class FilmsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/films.json"));

    public static List<FilmModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        List<FilmModel> films = JsonSerializer.Deserialize<List<FilmModel>>(json);
        
        if (films.Count > 0)
        {
            FilmModel.CurrentId = films.Max(f => f.Id) + 1;
        }
        else
        {
            FilmModel.CurrentId = 1;
        }

        return films;
    }

    public static void WriteAll(List<FilmModel> films)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(films, options);
        File.WriteAllText(path, json);
    }
}
