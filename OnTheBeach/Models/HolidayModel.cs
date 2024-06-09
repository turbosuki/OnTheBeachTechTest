namespace onthebeach.Models;

public class HolidayModel
{
    public HolidayModel(FlightModel flight, HotelModel hotel)
    {
        _flight = flight;
        _hotel = hotel;
        CalculateTotalPrice();
    }
    
    private FlightModel _flight;
    public FlightModel Flight
    {
        get => _flight;
        set
        {
            _flight = value;
            CalculateTotalPrice();
        }
    }

    private HotelModel _hotel;

    public HotelModel Hotel
    {
        get => _hotel;
        set
        {
            _hotel = value;
            CalculateTotalPrice();
        }
    }
    
    
    public int TotalPrice { get; private set; }
    
    private void CalculateTotalPrice()
    {
        TotalPrice = Hotel.PricePerNight * Hotel.Nights + Flight.Price;
    }
}