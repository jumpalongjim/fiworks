namespace FIWorks;

using System.Collections;
using System.Linq;

public class Cashflow : IEnumerable<Money>
{
    protected readonly Year startYear;
    private readonly List<Money> values;

    public Cashflow(Year startYear, IEnumerable<Money> values)
    {
        this.startYear = startYear;
        this.values = new List<Money>(values);
    }

    private uint Years => (uint)values.Count;
    public Year Start => startYear;
    public Year End => startYear + values.Count - 1;
    public IEnumerator<Money> GetEnumerator() => values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Cashflow operator +(Cashflow rangeA, Cashflow rangeB)
    {
        Year firstYear = new(Math.Min(rangeA.startYear, rangeB.startYear));
        uint numYears = new Year(Math.Max(rangeA.End, rangeB.End)) - firstYear + 1u;
        var firstRange = rangeA.Start <= rangeB.Start ? rangeA : rangeB;
        var secondRange = rangeA.Start > rangeB.Start ? rangeA : rangeB;
        
        uint secondRangeOffet = rangeB.Start - rangeA.Start;
        var firstRangePadding = Enumerable.Repeat(Money.Zero, (int)(numYears - firstRange.Years));
        var secondRangePadding = Enumerable.Repeat(Money.Zero, (int)(numYears - secondRange.Years - secondRangeOffet));
        var paddedFirstRange = firstRange.Concat(firstRangePadding);
        var paddedSecondRange = Enumerable.Repeat(Money.Zero, (int)secondRangeOffet).Concat(secondRange).Concat(secondRangePadding);
        var resultValues = paddedFirstRange.Zip(paddedSecondRange, (a, b) => a + b);
        return new Cashflow(firstYear, resultValues);
    }
}
