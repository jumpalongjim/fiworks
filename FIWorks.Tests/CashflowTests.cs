namespace FIWorks.Tests;

public class CashflowTests
{
    [Fact]
    public void RangeHasCorrectStartAndEndYears()
    {
        Cashflow range = new(new(2000), [new(1m), new(2m), new(3m)]);
        Assert.Equal(new Year(2000), range.Start);
        Assert.Equal(new Year(2002), range.End);
    }

    [Fact]
    public void SummedRangesHaveSameStartYear()
    {
        Cashflow r1 = new(new(2000), [new(2m), new(3m)]);
        Cashflow r2 = new(new(2001), [new(2m)]);
        Cashflow combined = r1 + r2;
        Assert.Equal(r1.Start, combined.Start);
    }

    [Fact]
    public void SummedRangesHaveEndYearOfLatestInputRange()
    {
        Cashflow rangeEnding2002 = new(new(2000), [new(2m), new(3m), new(4m)]);
        Cashflow rangeEnding2003 = new(new(2002), [new(2m), new(3m)]);
        Cashflow combined = rangeEnding2002 + rangeEnding2003;
        Assert.Equal(new Year(2003), combined.End);
    }

    [Fact]
    public void SummedRangesHaveSummedValues()
    {
        Cashflow range1 = new(new(2000), [new(2m), new(3m), new(4m)]);
        Cashflow range2 = new(new(2002), [new(-1m), new(5m)]);
        Cashflow combined = range1 + range2;
        Assert.Equal([2m, 3m, 3m, 5m], combined.Select(s => (decimal)s));
    }

}
