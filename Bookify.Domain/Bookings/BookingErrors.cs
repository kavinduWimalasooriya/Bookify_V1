using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

public static class BookingErrors
{
    public static readonly Error NotPending = new(
        "BookingErrors.NotPending",
        "The booking is not pending.");
    
    public static readonly Error NotFound = new(
        "BookingErrors.NotFound",
        "The booking was not found");

    public static readonly Error Overlap = new(
        "BookingErrors.Overlap",
        "The booking was overlapped");

    public static readonly Error NotReserved = new(
        "BookingErrors.NotReserved",
        "The booking was not reserved");
    
    public static readonly Error NotConfirmed = new(
        "Booking.NotReserved",
        "The booking is not confirmed");

    public static readonly Error AlreadyStarted = new(
        "Booking.AlreadyStarted",
        "The booking is already started");
}