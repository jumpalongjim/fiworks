

namespace FIWorks;

public class Projection
{
    internal Projection(IEnumerable<IndividualProjection> individuals)
    {
        Individuals = individuals.ToArray();
        TotalFunds = SumAllFunds();
    }

    public Balances TotalFunds { get; init; }

    public bool RemainsSolvent => TotalFunds.All(m => m.IsPositive);

    public IndividualProjection[] Individuals { get; }

    public string SingleLineText()
    {
        string balancesText = string.Join(", ", TotalFunds.Select(m => m.ToString()));
        return $"Funds {TotalFunds.Start}-{TotalFunds.End}: {balancesText}";
    }

    private Balances SumAllFunds()
    {
        var totalFunds = Balances.Empty(Individuals.First().Funds.Start);
        foreach(var individual in Individuals)
        {
            totalFunds = totalFunds.Combine(individual.Funds);
        }
        return totalFunds;
    }
}
