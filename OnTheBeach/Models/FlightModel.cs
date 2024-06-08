namespace onthebeach.Models;

public class FlightModel
{
    public int Id { get; set; }
    public string Airline { get; set; }
    public string SourceLocation { get; set; }
    public string DestinationLocation { get; set; }
    public int Price { get; set; }
    public DateTime DepartureDate { get; set; }

    public FlightModel(int id, string airline, string sourceLocation, string destinationLocation, int price,
        DateTime departureDate)
    {
        Id = id;
        Airline = airline;
        SourceLocation = sourceLocation;
        DestinationLocation = destinationLocation;
        Price = price;
        DepartureDate = departureDate;
    }
}