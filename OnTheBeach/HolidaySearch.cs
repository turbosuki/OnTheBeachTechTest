using onthebeach.Models;

namespace onthebeach;

public class HolidaySearch
{
    public List<string> DepartingFrom;
    public string TravellingTo;
    public DateTime DepartureDate;
    public int Duration;
    public List<HolidayModel> Results;

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
        // do the do
    }
}