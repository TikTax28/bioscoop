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

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        SeatModel otherSeat = (SeatModel)obj;

        return Row == otherSeat.Row && Seat == otherSeat.Seat;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Seat);
    }
}