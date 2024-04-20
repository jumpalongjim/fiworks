
namespace FIWorks;

public class Investment : Balances
{
    private const string DEFAULT_LABEL = "Investment";
    private readonly decimal annualGrowthRate;

    public Investment(Year startYear, Year endYear, Money openingBalance, decimal annualGrowth, string? label = DEFAULT_LABEL)
    : base(startYear, endYear, openingBalance)
    {
        if (endYear < startYear) throw new ArgumentException("Investment end year must be equal to or after the start year.");
        this.annualGrowthRate = annualGrowth;
    }

    protected override IEnumerable<Money> CalculateClosingValues()
    {
        Money closingBalance = this.openingValue;
        foreach (Money movement in this.movements)
        {
            var growth = closingBalance * annualGrowthRate;
            closingBalance = closingBalance + growth + movement;
            yield return closingBalance;
        }
    }
}
