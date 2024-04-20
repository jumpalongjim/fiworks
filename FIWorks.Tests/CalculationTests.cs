using FIWorks.Configuration;

namespace FIWorks.Tests;

public class CalculationTests
{
    [Fact]
    public void ProjectionForSinglePersonIsUptoAge100()
    {
        var config = new Config(
            StartYear: new Year(2020),
            Subjects: [
                new Person (
                    Name: "James",
                    BirthDate: DateOnly.Parse("1969-02-19"),
                    RetirementAge: new Age(60),
                    Investments: [
                        new InvestmentData("TH", 15000m, 0.025m)
                    ]
                )
            ]
        );
        var projection = Calculator.Run(config);
        Assert.Equal(2069u, projection.Individuals[0].Funds.End);
    }

    [Fact]
    public void ProjectionForMultiplePeopleIsUptoYoungestAge100()
    {
        var config = new Config(
            StartYear: new Year(2020),
            Subjects: [
                new Person (
                    Name: "James",
                    BirthDate: DateOnly.Parse("1969-02-19"), // aged 100 in year 2069
                    RetirementAge: new Age(60),
                    Investments: [
                        new InvestmentData("TH", 15000m, 0.025m)
                    ]
                ),
                new Person (
                    Name: "David",
                    BirthDate: DateOnly.Parse("1980-02-19"), // aged 40 in year 2080
                    RetirementAge: new Age(60),
                    Investments: [
                        new InvestmentData("TH", 15000m, 0.025m)
                    ]
                )
            ]
        );
        var projection = Calculator.Run(config);
        Assert.Equal(2080u, projection.TotalFunds.End);
    }

}
