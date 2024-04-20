namespace FIWorks.Tests;

public class InvestmentTests
{
    [Fact]
    public void InvestmentEndIsAfterStart()
    {
        // check valid args
        var validInvestment = new Investment(new Year(2000), new Year(2001), Money.Zero, 0m);
        // check invalid args
        Assert.Throws<ArgumentException>(() => new Investment(new Year(2000), new Year(1999), Money.Zero, 0m));
    }

    [Fact]
    public void InvestmentGrowsByGivenRate()
    {
        decimal initialBalance = 10m;
        decimal growthRate = 0.06m;
        var investment = new Investment(new Year(2000), new Year(2001), new Money(initialBalance), growthRate);
        decimal afterYear1 = initialBalance * (1 + growthRate);
        decimal afterYear2 = afterYear1 * (1 + growthRate);

        Assert.Equal(afterYear1, investment.ElementAt(0));
        Assert.Equal(afterYear2, investment.ElementAt(1));
    }

    [Fact]
    public void InvestmentGrowsWithContributions()
    {
        decimal initialBalance = 10m;
        var annualContribution = new Money(2m);
        var investment = new Investment(new Year(2000), new Year(2001), new Money(initialBalance), 0m);
        var contributions = new Cashflow(new Year(2000), [annualContribution, annualContribution]);
        investment.AddCashflow(contributions);
        decimal afterYear1 = initialBalance + annualContribution;
        decimal afterYear2 = afterYear1 + annualContribution;

        Assert.Equal(afterYear1, investment.ElementAt(0));
        Assert.Equal(afterYear2, investment.ElementAt(1));
    }

    [Fact]
    public void CheckAgainstExcel()
    {
        var investment1 = new Investment(new Year(2024), new Year(2031), new Money(218542m), 0.025m);
        Assert.Equal(266272m, Math.Round(investment1.Last()));

        var investment2 = new Investment(new Year(2024), new Year(2031), new Money(7217m), 0.025m);
        var contributions = new Cashflow(new Year(2024), Money.ConstantCashflow(7200m, new Year(2024), 8));
        investment2.AddCashflow(contributions);
        Assert.Equal(71693m, Math.Round(investment2.Last()));
    }
}
