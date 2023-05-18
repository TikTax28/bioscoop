using System.Text.Json;

static class BookingsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/seats.json"));

    public static List<BookingModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        List<BookingModel> bookings = JsonSerializer.Deserialize<List<BookingModel>>(json);

        return bookings;
    }

    public static void WriteAll(List<BookingModel> bookings)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(bookings, options);
        File.WriteAllText(path, json);
    }
}
