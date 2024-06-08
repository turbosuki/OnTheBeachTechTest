using Newtonsoft.Json;

namespace onthebeach.Models;

public class FlightModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("airline")]
    public string Airline { get; set; }
    
    [JsonProperty("from")]
    public string From { get; set; }
    
    [JsonProperty("to")]
    public string To { get; set; }
    
    [JsonProperty("price")]
    public int Price { get; set; }
    
    [JsonProperty("departure_date")]
    public DateTime DepartureDate { get; set; }

    public FlightModel(int id, string airline, string from, string to, int price,
        DateTime departureDate)
    {
        Id = id;
        Airline = airline;
        From = from ;
        To = to;
        Price = price;
        DepartureDate = departureDate;
    }
}