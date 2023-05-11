using System;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        // Laad de gegevens uit JSON-bestand
        var jsonString = File.ReadAllText("films.json");
        var movies = JsonConvert.DeserializeObject<Movie[]>(jsonString);

        // Vraag om  ID van de film die moet worden bijgewerkt
        Console.Write("Voer het ID in van de film die moet worden bijgewerkt: ");
        var id = int.Parse(Console.ReadLine());

        // Zoek film met het opgegeven ID
        var movieToUpdate = movies.FirstOrDefault(m => m.Id == id);
        if (movieToUpdate == null)
        {
            Console.WriteLine($"Film met ID {id} niet gevonden.");
            return;
        }

        // Vraag om nieuwe beschrijving
        Console.Write("Voer de nieuwe beschrijving in: ");
        var newDescription = Console.ReadLine();

        // Werk beschrijving van de film bij
        movieToUpdate.FilmDescription = newDescription;

        // Sla bijgewerkte gegevens opnieuw op in JSON-bestand
        var updatedJsonString = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText("films.json", updatedJsonString);

        Console.WriteLine("Film succesvol bijgewerkt.");
    }
}

class Movie
{
    public int Id { get; set; }
    public string FilmName { get; set; }
    public string FilmDescription { get; set; }
    public string FilmDate { get; set; }
    public string FilmTime { get; set; }
    public string FilmRoom { get; set; }
}


