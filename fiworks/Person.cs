using FIWorks.Configuration;

namespace FIWorks;

public record Person(
    string Name,
    DateOnly BirthDate,
    Age RetirementAge,
    InvestmentData[] Investments
)
{
    public Year RetirementYear => new Year((ushort)BirthDate.AddYears(RetirementAge).Year);
}
