using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity
{
    public Apartment(Guid id) : base(id)
    {
    }

    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string Country { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string ZipCode { get; private set; }
    public decimal Price { get; private set; }
    public decimal CleaningFee { get; private set; }
    public DateTime? LastBookedOnUtc { get; private set; }
    public List<Amenity> Amemities { get; private set; } = new();
}