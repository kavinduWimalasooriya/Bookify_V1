using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings;

public class PricingService
{
    public PricingDetails CalculatePrice(Apartment apartment, DateRange dateRange)
    {
        var currency = apartment.Price.Currency;

        var priceForPeriod = new Money(apartment.Price.Amount * dateRange.LengthInDays, currency);

        decimal percentageUpCharge = 0;
        foreach (var amenity in apartment.Amenities)
        {
            percentageUpCharge += amenity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.05m,
                Amenity.AirConditioner => 0.01m,
                Amenity.Parking => 0.01m,
                _ => 0
            };
        }
        var amenityUpCharge = Money.Zero(currency);
        if (percentageUpCharge > 0)
        {
            amenityUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
        }
        
        var totalPrice = Money.Zero(currency);
        totalPrice += priceForPeriod;
        if (!apartment.CleaningFee.IsZero())
        {
            totalPrice += apartment.CleaningFee;
        }

        totalPrice += amenityUpCharge;
        
        return new PricingDetails(priceForPeriod, apartment.CleaningFee,amenityUpCharge, totalPrice);
    }

}

public record PricingDetails(Money PriceForPeriod, Money CleaningFee, Money AmenitiesUpCharge, Money TotalPrice);