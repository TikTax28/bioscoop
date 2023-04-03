using System;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // File path of the JSON file containing movie data
        string filePath = "movietest.json";
        // Read the entire JSON file as a string
        string json = File.ReadAllText(filePath);

        // Deserialize the JSON string into an array of Movie objects using Newtonsoft.Json package
        Movie[] movies = JsonConvert.DeserializeObject<Movie[]>(json);

        // Loop until the user presses the "b" key
        while (true)
        {
            // Ask the user to input a command
            Console.WriteLine("Enter 'a' to display all movies, 's' to search for a movie, or 'b' to exit:");
            ConsoleKeyInfo key = Console.ReadKey();

            // If the user pressed the "a" key, display all movies
            if (key.Key == ConsoleKey.A)
            {
                Console.WriteLine("\nAll available movies:");
                foreach (var movie in movies)
                {
                    Console.WriteLine($"{movie.Title} ({movie.Year})");
                }
                Console.WriteLine();
            }
            // If the user pressed the "s" key, enter search function
            else if (key.Key == ConsoleKey.S)
            {
                // Ask user to input a search query
                Console.Write("\nEnter movie title to search: ");
                string searchQuery = Console.ReadLine();

                // Use LINQ to search for movies whose title contains the search query, ignoring case sensitivity
                var results = movies.Where(m => m.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));

                // Output the number of results found
                Console.WriteLine($"\nFound {results.Count()} results:");

                // Output each movie title and year for each result found
                foreach (var movie in results)
                {
                    Console.WriteLine($"{movie.Title} ({movie.Year})");
                }
                Console.WriteLine();
            }
            // If the user pressed the "b" key, exit the loop and end the program
            else if (key.Key == ConsoleKey.B)
            {
                break;
            }
        }
    }
}

// Define a class to represent a Movie object with Title and Year properties
class Movie
{
    public string Title { get; set; }
    public int Year { get; set; }
}
