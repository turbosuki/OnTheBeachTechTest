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
        var holidaySearch = new HolidaySearch()
            .WhereDepartingFrom("MAN") //TODO represent airport names better, hardcoding icky
            .WhereTravellingTo("AGP")
            .WithDuration(7)
            .WithDepartureDate(new DateTime(2023, 7, 1));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Not.Empty, "Holidays list should not be empty");
        Assert.That(holidaySearch.Results[0].Flight.Id, Is.EqualTo(2), "First flight should have ID 2");
        Assert.That(holidaySearch.Results[0].Hotel.Id, Is.EqualTo(9), "First hotel should have Id 9");
    }
    
    public void CorrectHolidayReturnedForCustomer2()
    {
        var holidaySearch = new HolidaySearch()
            .WhereDepartingFrom("") //TODO change model, it can be more than one airport. Sneaky. 
            .WhereTravellingTo("PMI")
            .WithDuration(10)
            .WithDepartureDate(new DateTime(2023, 6, 15));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Not.Empty, "Holidays list should not be empty");
        Assert.That(holidaySearch.Results[0].Flight.Id, Is.EqualTo(6), "First flight should have ID 6");
        Assert.That(holidaySearch.Results[0].Hotel.Id, Is.EqualTo(5), "First hotel should have Id 5");
    }
    
    public void CorrectHolidayReturnedForCustomer3()
    {
        var holidaySearch = new HolidaySearch()
            .WhereDepartingFrom("") //TODO account for ANY 
            .WhereTravellingTo("LPA")
            .WithDuration(14)
            .WithDepartureDate(new DateTime(2022, 11, 10));
        
        holidaySearch.Search();

        Assert.That(holidaySearch.Results, Is.Not.Empty, "Holidays list should not be empty");
        Assert.That(holidaySearch.Results[0].Flight.Id, Is.EqualTo(7), "First flight should have ID 7");
        Assert.That(holidaySearch.Results[0].Hotel.Id, Is.EqualTo(6), "First hotel should have Id 6");
    }
}