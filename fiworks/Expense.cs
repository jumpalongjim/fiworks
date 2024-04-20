namespace FIWorks;

public abstract class Expense : Cashflow
{
    public Expense(Year startYear, Year endYear, Money annualAmount)
        : base(startYear, Enumerable.Repeat<Money>(annualAmount, endYear - startYear + 1))
    { }
}
