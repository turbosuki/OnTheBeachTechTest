using Newtonsoft.Json;
using onthebeach.Models;

namespace onthebeach;

public class HolidaySearch
{
    private List<string> DepartingFrom { get; set; }
    private string TravellingTo { get; set; }
    private DateTime DepartureDate { get; set; }
    private int Duration { get; set; }
    public List<HolidayModel> Results { get; set; } = [];
    private List<FlightModel> AvailableFlights { get; set; }
    private List<HotelModel> AvailableHotels { get; set; }

    public HolidaySearch()
    {
        UpdateFlightAndHotelDataFromFiles();
    }
    
    public HolidaySearch WhereDepartingFrom(List<string> departingFrom)
    {
        DepartingFrom = departingFrom;
        return this;
    }

    public HolidaySearch WhereTravellingTo(string travellingTo)
    {
        TravellingTo = travellingTo;
        return this;
    }

    public HolidaySearch WithDepartureDate(DateTime departureDate)
    {
        DepartureDate = departureDate;
        return this;
    }

    public HolidaySearch WithDuration(int duration)
    {
        Duration = duration;
        return this;
    }

    public void Search()
    {
        var selectedFlights = AvailableFlights
            .Where(flight => (!DepartingFrom.Any() || DepartingFrom.Contains(flight.From))
                             && flight.To.Equals(TravellingTo)
                             && flight.DepartureDate.Equals(DepartureDate)
            )
            .ToList();
        
        var selectedHotels = AvailableHotels
            .Where(hotel => hotel.Nights.Equals(Duration) && hotel.LocalAirports.Any(airport => TravellingTo.Contains(airport)) &&
                            hotel.ArrivalDate.Equals(DepartureDate))
            .ToList();

        foreach (var flight in selectedFlights)
        {
            foreach (var hotel in selectedHotels)
            {
                Results.Add(CreateHolidayModel(flight, hotel));
            }
        }

        Results = Results.OrderBy(r => r.TotalPrice).ToList();
        
    }

    public void UpdateFlightAndHotelDataFromFiles()
    {
        try
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var flightsFilePath = Path.Combine(basePath, "Data/flights.json");
            var jsonFlightData = File.ReadAllText(flightsFilePath);
            AvailableFlights = JsonConvert.DeserializeObject<List<FlightModel>>(jsonFlightData) ?? [];

            var hotelsFilePath = Path.Combine(basePath, "Data/hotels.json");
            var jsonHotelData = File.ReadAllText(hotelsFilePath);
            AvailableHotels = JsonConvert.DeserializeObject<List<HotelModel>>(jsonHotelData) ?? [];
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"A required file was not found. Check your directory structure. Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during the search: {ex.Message}");
        }
    }

    private HolidayModel CreateHolidayModel(FlightModel flight, HotelModel hotel)
    {
        return new HolidayModel(flight, hotel);
    }
}