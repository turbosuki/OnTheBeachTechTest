using onthebeach;

namespace OnTheBeach.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        //See if test setup needed
    }

    [Test]
    public void CorrectHolidayReturnedForCustomer1()
    {
        var departingFrom = new List<string> { "MAN" };
        var holidaySearch = new HolidaySearch()
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
    public void CorrectHolidayReturnedForCustomer2()
    {
        var departingFrom = new List<string> { "LHR", "LGW", "STN", "LTN", "SEN", "LCY" };
        var holidaySearch = new HolidaySearch()
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
    public void CorrectHolidayReturnedForCustomer3()
    {
        var departingFrom = new List<string>();
        var holidaySearch = new HolidaySearch()
            .WhereDepartingFrom(departingFrom)
            .WhereTravellingTo("LPA")
            .WithDuration(14)
            .WithDepartureDate(new DateTime(2022, 11, 10));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Not.Empty, "Holidays list should not be empty");
        Assert.That(holidaySearch.Results[0].Flight.Id, Is.EqualTo(7), "First flight should have ID 7");
        Assert.That(holidaySearch.Results[0].Hotel.Id, Is.EqualTo(6), "First hotel should have Id 6");
    }
}