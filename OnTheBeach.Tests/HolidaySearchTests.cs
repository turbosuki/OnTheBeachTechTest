using Newtonsoft.Json;
using onthebeach;

namespace OnTheBeach.Tests;

public class Tests
{
    private const string FlightFileName = "flights.json";
    private const string HotelFileName = "hotels.json";
    
    [Test]
    public void CustomerSearchingForSingleDepartingFromGetsCorrectResult()
    {
        var departingFrom = new List<string> { "MAN" };
        var holidaySearch = new HolidaySearch(FlightFileName, HotelFileName)
            .WhereDepartingFrom(departingFrom)
            .WhereTravellingTo("AGP")
            .WithDuration(7)
            .WithDepartureDate(new DateTime(2023, 7, 1));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Not.Empty, "Holidays list should not be empty");
        Assert.That(holidaySearch.Results[0].Flight.Id, Is.EqualTo(2), "First flight should have ID 2");
        Assert.That(holidaySearch.Results[0].Hotel.Id, Is.EqualTo(9), "First hotel should have Id 9");
    }
    
    [Test]
    public void CustomerDepartingFromAnyLondonAirportGetsCorrectResult()
    {
        var departingFrom = new List<string> { "LHR", "LGW", "STN", "LTN", "SEN", "LCY" };
        var holidaySearch = new HolidaySearch(FlightFileName, HotelFileName)
            .WhereDepartingFrom(departingFrom)
            .WhereTravellingTo("PMI")
            .WithDuration(10)
            .WithDepartureDate(new DateTime(2023, 6, 15));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Not.Empty, "Holidays list should not be empty");
        Assert.That(holidaySearch.Results[0].Flight.Id, Is.EqualTo(6), "First flight should have ID 6");
        Assert.That(holidaySearch.Results[0].Hotel.Id, Is.EqualTo(5), "First hotel should have Id 5");
    }
    
    [Test]
    public void CustomerDepartingFromAnyAirportGetsCorrectResult()
    {
        var departingFrom = new List<string>();
        var holidaySearch = new HolidaySearch(FlightFileName, HotelFileName)
            .WhereDepartingFrom(departingFrom)
            .WhereTravellingTo("LPA")
            .WithDuration(14)
            .WithDepartureDate(new DateTime(2022, 11, 10));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Not.Empty, "Holidays list should not be empty");
        Assert.That(holidaySearch.Results[0].Flight.Id, Is.EqualTo(7), "First flight should have ID 7");
        Assert.That(holidaySearch.Results[0].Hotel.Id, Is.EqualTo(6), "First hotel should have Id 6");
    }
    
    [Test]
    public void CustomerSearchingForInvalidHolidayGetsNoResult()
    {
        var departingFrom = new List<string>();
        var holidaySearch = new HolidaySearch(FlightFileName, HotelFileName)
            .WhereDepartingFrom(departingFrom)
            .WhereTravellingTo("LPA")
            .WithDuration(21)
            .WithDepartureDate(new DateTime(2022, 10, 1));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Empty, "Holidays list should be empty");
    }
    
    [Test]
    public void FileNotFoundExceptionThrownWhenIncorrectFilePathProvided()
    {
        Assert.Throws<FileNotFoundException>(() =>
        {
            new HolidaySearch("doesnotexist.json", HotelFileName);
        });
    }
    
    [Test]
    public void JsonReaderExceptionThrownWhenFileConversionFailsDueToJsonParsingError()
    {
        Assert.Throws<JsonReaderException>(() =>
        {
            new HolidaySearch("invalidjson.json", HotelFileName);
        });
    }
}