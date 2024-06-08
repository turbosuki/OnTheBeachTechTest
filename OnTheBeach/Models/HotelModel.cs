namespace onthebeach.Models;

public class HotelModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ArrivalDate { get; set; }
    public int PricePerNight { get; set; }
    public List<string> LocalAirports { get; set; }
    public int Nights { get; set; }

    public HotelModel(int id, string name, DateTime arrivalDate, int pricePerNight, List<string> localAirports,
        int nights)
    {
        Id = id;
        Name = name;
        ArrivalDate = arrivalDate;
        PricePerNight = pricePerNight;
        LocalAirports = localAirports;
        Nights = nights;
    }
}