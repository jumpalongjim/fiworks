using System.Collections;

namespace FIWorks;

public class Balances : IEnumerable<Money>
{
    protected readonly Money openingValue;
    protected readonly Year endYear;
    protected Cashflow movements;

    public static Balances Empty(Year startYear) => new Balances(startYear, startYear, Money.Zero);
    public Balances(Year startYear, Year endYear, Money openingBalance)
    {
        this.openingValue = openingBalance;
        this.endYear = endYear;
        this.movements = new Cashflow(startYear, Enumerable.Repeat(Money.Zero, (endYear - startYear + 1)));
    }

    public Year Start => movements.Start;
    public Year End => movements.Start + movements.Count() - 1;

    public void AddCashflow(Cashflow range)
    {
        movements += range;
    }

    public Balances Combine(Balances otherBalances)
    {
        var b1Movements = new Cashflow(movements.Start, this.AnnualMovements);
        var b2Movements = new Cashflow(movements.Start, otherBalances.AnnualMovements);
        var totalBalances = new Balances(movements.Start, endYear, openingValue + otherBalances.openingValue);
        totalBalances.AddCashflow(b1Movements);
        totalBalances.AddCashflow(b2Movements);
        return totalBalances;
    }

    public IEnumerator<Money> GetEnumerator() => CalculateClosingValues().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<Money> AnnualMovements
    {
        get
        {
            var movementsAfterYear1 = this.Zip(this.Skip(1), (x, y) => y - x);
            return movementsAfterYear1.Prepend(this.First() - openingValue);
        }
    }

    protected virtual IEnumerable<Money> CalculateClosingValues()
    {
        Money closingBalance = this.openingValue;
        foreach (Money movement in this.movements)
        {
            closingBalance += movement;
            yield return closingBalance;
        }
    }
}
