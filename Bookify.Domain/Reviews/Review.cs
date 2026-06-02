using Bookify.Domain.Abstractions;
using Bookify.Domain.Reviews.Events;

namespace Bookify.Domain.Reviews;

public sealed class Review : Entity
{
    private Review(Guid id, Guid apartmentId, Guid userId, Guid bookingId, Rating rating, Comment comment) : base(id)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        BookingId = bookingId;
        Rating = rating;
        Comment = comment;
    }

    public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BookingId { get; private set; }
    public Rating Rating { get; private set; }
    public Comment Comment { get; private set; }

    public Result<Review> Create(Guid apartmentId, Guid userId, Guid bookingId, Rating rating, Comment comment)
    {
        var review = new Review(
            Guid.NewGuid(),
            apartmentId, userId, bookingId, rating, comment);
        
        review.RaiseDomainEvent(new ReviewCreateDomainEvent(review.Id));
        return review;
    }
}