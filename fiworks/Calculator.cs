using FIWorks.Configuration;

namespace FIWorks;

public static class Calculator
{
    public static Projection Run(Config config)
    {
        Validate(config);
        var startYear = config.StartYear;
        var endYear = new Year((ushort)config.Subjects.Select(s => s.BirthDate.AddYears(100)).Max().Year);
        var individualProjections = config.Subjects.Select(s => IndividualCalculation(startYear, endYear, s)).ToArray();
        return new Projection(individualProjections);
    }

    private static void Validate(Config config)
    {
        foreach (var person in config.Subjects)
        {
            if (person.RetirementYear <= config.StartYear)
            {
                throw new ArgumentOutOfRangeException($"{person.Name} retires in {person.RetirementYear}, before the projection starts.");
            }
        }
    }

    public static IndividualProjection IndividualCalculation(Year start, Year end, Person person)
    {
        var investments = person.Investments
            .Select(id => new Investment(start, end, new Money(id.OpeningBalance), id.AnnualGrowthRate, id.Label));
        var funds = Balances.Empty(start);
        foreach (var investment in investments)
        {
            funds = funds.Combine(investment);
        }
        return new IndividualProjection(funds);
    }
}
