using Newtonsoft.Json;
using onthebeach.Models;

namespace onthebeach;

public class HolidaySearch
{
    private List<string> _departingFrom;
    private string _travellingTo;
    private DateTime _departureDate;
    private int _duration;
    private List<FlightModel> _availableFlights;
    private List<HotelModel> _availableHotels;
    public List<HolidayModel> Results { get; private set; } = [];

    public HolidaySearch()
    {
        UpdateFlightAndHotelDataFromFiles();
    }
    
    public HolidaySearch WhereDepartingFrom(List<string> departingFrom)
    {
        _departingFrom = departingFrom;
        return this;
    }

    public HolidaySearch WhereTravellingTo(string travellingTo)
    {
        _travellingTo = travellingTo;
        return this;
    }

    public HolidaySearch WithDepartureDate(DateTime departureDate)
    {
        _departureDate = departureDate;
        return this;
    }

    public HolidaySearch WithDuration(int duration)
    {
        _duration = duration;
        return this;
    }

    public void Search()
    {
        var selectedFlights = _availableFlights
            .Where(flight => (!_departingFrom.Any() || _departingFrom.Contains(flight.From))
                             && flight.To.Equals(_travellingTo)
                             && flight.DepartureDate.Equals(_departureDate)
            )
            .ToList();
        
        var selectedHotels = _availableHotels
            .Where(hotel => hotel.Nights.Equals(_duration) 
                            && hotel.LocalAirports.Any(airport => _travellingTo.Contains(airport)) 
                            && hotel.ArrivalDate.Equals(_departureDate))
            .ToList();

        foreach (var flight in selectedFlights)
        {
            foreach (var hotel in selectedHotels)
            {
                Results.Add(new HolidayModel(flight, hotel));
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
            _availableFlights = JsonConvert.DeserializeObject<List<FlightModel>>(jsonFlightData) ?? [];

            var hotelsFilePath = Path.Combine(basePath, "Data/hotels.json");
            var jsonHotelData = File.ReadAllText(hotelsFilePath);
            _availableHotels = JsonConvert.DeserializeObject<List<HotelModel>>(jsonHotelData) ?? [];
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
}