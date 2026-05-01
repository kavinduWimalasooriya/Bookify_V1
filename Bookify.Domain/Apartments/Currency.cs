namespace Bookify.Domain.Apartments;

public record Currency
{
    public string Code { get; init; }
    private Currency(string code) => Code = code;
    
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");

    public static readonly IReadOnlyCollection<Currency> All = new[]{Usd, Eur};

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(x => x.Code == code) ?? 
               throw new ApplicationException($"Unknown currency code: {code}");
    }

}