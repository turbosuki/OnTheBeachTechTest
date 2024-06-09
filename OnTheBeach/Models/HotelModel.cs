namespace onthebeach.Models;

using Newtonsoft.Json;
using System;

public class HotelModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("arrival_date")]
    public DateTime ArrivalDate { get; set; }
    
    [JsonProperty("price_per_night")]
    public int PricePerNight { get; set; }
    
    [JsonProperty("local_airports")]
    public List<string> LocalAirports { get; set; }
    
    [JsonProperty("nights")]
    public int Nights { get; set; }
    
    public HotelModel(int id, string name, DateTime arrivalDate, int pricePerNight, List<string> localAirports, int nights)
    {
        Id = id;
        Name = name;
        ArrivalDate = arrivalDate;
        PricePerNight = pricePerNight;
        LocalAirports = localAirports;
        Nights = nights;
    }
}