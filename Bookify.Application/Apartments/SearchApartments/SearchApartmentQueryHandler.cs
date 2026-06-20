using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Apartments.SearchApartments;

internal sealed class SearchApartmentQueryHandler : IQueryHandler<SearchApartmentQuery,IReadOnlyList<ApartmentResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private static readonly int[] ActiveBookingStatuses =
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed,
    };

    public SearchApartmentQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }


    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        const string sql = """
            SELECT 
            a.Id AS Id,
            a.name AS Name,
            a.description AS Description,
            a.price_amount AS Price,    
            a.price_currency AS Currency,
            a.address_country AS Country,
            a.address_state AS State,
            a.address_city AS City,
            a.address_zip_code AS ZipCode,
            a.address_street AS Street,
            FROM apartments a 
            WHERE NOT EXISTS (
                SELECT 1 FROM bookings AS b
                WHERE a.Id = b.Id
                AND b.duration_start <= @EndDate 
                AND b.duration_end >= @StartDate
                AND b.status = ANY(@ActiveBookingStatuses)
            )
            """;
        var result = await connection
            .QueryAsync<ApartmentResponse,AddressResponse,ApartmentResponse>(
                sql,
                (apartment, address) =>
                {
                    apartment.Address = address;
                    return apartment;
                },
            new
                {
                    request.StartDate,
                    request.EndDate,
                    ActiveBookingStatuses
                },
                splitOn:"Country");
        
        return result.ToList();
    }
}