namespace Bookify.Domain.Apartments;

public record Money(decimal Amount,Currency Currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidOperationException("Currency have to be same currency");
        }
        
        return new Money(first.Amount + second.Amount, first.Currency);
    }

    public static Money operator -(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidOperationException("Currency have to be same currency");
        }

        if (first.Amount < second.Amount)
        {
            throw new InvalidOperationException("First amount must be greater than or equal to second amount");
        }
        
        return new Money(first.Amount - second.Amount, first.Currency);
    }
    
    public static Money Zero() => new Money(0, Currency.Usd);
    
    public static Money Zero(Currency currency) => new(0, currency);

    public bool IsZero() => this == Zero();
}