using System.Text.Json.Serialization;

class BookingModel
{
    [JsonPropertyName("account_id")]
    public int AccountId { get; set; }

    [JsonPropertyName("film_id")]
    public int FilmId { get; set; }

    [JsonPropertyName("seats")]
    public List<SeatModel> Seats { get; set; }

    public BookingModel() { }
    public BookingModel(int account_id, int film_id, List<SeatModel> seats)
    {
        AccountId = account_id;
        FilmId = film_id;
        Seats = seats;
    }
}