using FIWorks;
using FIWorks.Configuration;

var inputs = new Config(
    StartYear: new Year(2024),

    Subjects: [
        new Person(
        Name: "James",
        BirthDate: DateOnly.Parse("1969-Feb-19"),
        RetirementAge: new Age(62),
        Investments: [
            new InvestmentData("TH Pension", 18000m, .025m)
        ]
        )
    ]
);

var projection = Calculator.Run(inputs);
foreach (var ip in projection.Individuals)
{
    Console.WriteLine(ip.SingleLineText());
}
Console.WriteLine($"Remains solvent: {projection.RemainsSolvent}");
