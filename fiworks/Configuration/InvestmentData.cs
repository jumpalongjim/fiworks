namespace FIWorks.Configuration;

public record InvestmentData(
    string Label,
    decimal OpeningBalance,
    decimal AnnualGrowthRate
);