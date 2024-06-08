using Newtonsoft.Json;
using onthebeach.Models;

namespace onthebeach;

public class HolidaySearch
{
    public List<string> DepartingFrom;
    public string TravellingTo;
    public DateTime DepartureDate;
    public int Duration;
    public List<HolidayModel> Results = new();
    public List<FlightModel> AvailableFlights;
    public List<HotelModel> AvailableHotels;

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

    public void Initialise()
    {
        try
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string flightsFilePath = Path.Combine(basePath, "Data", "flights.json");
            string jsonFlightData = File.ReadAllText(flightsFilePath);
            AvailableFlights = JsonConvert.DeserializeObject<List<FlightModel>>(jsonFlightData);

            string hotelsFilePath = Path.Combine(basePath, "Data", "hotels.json");
            string jsonHotelData = File.ReadAllText(hotelsFilePath);
            AvailableHotels = JsonConvert.DeserializeObject<List<HotelModel>>(jsonHotelData);
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

    public void Search()
    {
        var selectedFlights = AvailableFlights
            .Where(x => (!DepartingFrom.Any() || DepartingFrom.Contains(x.From))
                        && x.To.Equals(TravellingTo)
                        && x.DepartureDate.Equals(DepartureDate)
            )
            .OrderBy(x => x.Price)
            .ToList();
        
        var selectedHotels = AvailableHotels
            .Where(x => x.Nights == Duration && x.LocalAirports.Any(airport => TravellingTo.Contains(airport)) &&
                        x.ArrivalDate.Equals(DepartureDate)).OrderBy(x => x.PricePerNight).ToList();

        foreach (FlightModel flight in selectedFlights)
        {
            foreach (HotelModel hotel in selectedHotels)
            {
                Results.Add(new HolidayModel
                {
                    Flight = flight,
                    Hotel = hotel,
                    TotalPrice = hotel.PricePerNight * hotel.Nights + flight.Price
                });
            }
        }
    }
}