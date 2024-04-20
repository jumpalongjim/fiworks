namespace FIWorks.Tests;
using FIWorks;
using FIWorks.Configuration;

public class SolvencyTests
{
    [Fact]
    public void EmptyExpensesRemainsSolvent()
    {
        var config = new Config(
            StartYear: new Year(2024),
            Subjects: [TestFixtures.Subject(65)]
        );

        var projection = Calculator.Run(config);
        Assert.True(projection.RemainsSolvent);
    }

    [Fact]
    public void PlanWithExpensesAndNoIncomeIsInsolvent()
    {
        var config = new Config(
            StartYear: new Year(2024),
            Subjects: [TestFixtures.Subject(65)],
            Expenses: new [] { new FlatExpense(new Year(2030), new Year(2040), new Money(-100m))}
        );

        var projection = Calculator.Run(config);
        Assert.False(projection.RemainsSolvent);
    }
}