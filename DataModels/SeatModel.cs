using System.Text.Json.Serialization;

class SeatModel
{
    [JsonPropertyName("row")]
    public string Row { get; set; }

    [JsonPropertyName("seat")]
    public int Seat { get; set; }

    public SeatModel(string row, int seat)
    {
        Row = row;
        Seat = seat;
    }
}