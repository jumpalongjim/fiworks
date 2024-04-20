namespace FIWorks;

public record IndividualProjection(Balances Funds)
{
    public string SingleLineText()
    {
        string balancesText = string.Join(", ", Funds.Select(m => m.Thousands()));
        return $"Funds {Funds.Start}-{Funds.End}: {balancesText}";
    }
}
