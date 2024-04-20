namespace FIWorks.Tests;

public class BalancesTests
{
    [Fact]
    public void BalancesStartYearIsDefined()
    {
        var balances = new Balances(new(2000), new(2000), new(0m));
        Assert.Equal(new Year(2000), balances.Start);
        Assert.Equal(new Year(2000), balances.End);
    }

    [Fact]
    public void OpeningBalanceIsDefined()
    {
        var balances = new Balances(new(2000), new(2000), new(1.23m));
        Assert.Equal(1.23m, balances.First());
    }

    [Fact]
    public void BalancesYearsExpandToAddedRanges()
    {
        var cashflow = new Cashflow(new Year(2002), [new Money(2m), new Money(3m)]);
        var balances = new Balances(new(2000), new(2000), new(0m));
        balances.AddCashflow(cashflow);
        Assert.Equal(new Year(2000), balances.Start);
        Assert.Equal(new Year(2003), balances.End);
    }

    [Fact]
    public void BalancesAreUpdatedByMovements()
    {
        var balances = new Balances(new(2000), new(2000), new(.2m));
        var cashflow = new Cashflow(new(2001), [new(1m), new(1.5m), new(-1.2m)]);
        balances.AddCashflow(cashflow);
        Assert.Equal<decimal>([0.2m, 1.2m, 2.7m, 1.5m], balances.Select(m => (decimal)m));
    }
}
