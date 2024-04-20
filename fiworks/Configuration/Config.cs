using Microsoft.VisualBasic;

namespace FIWorks.Configuration;

public record Config(
        Year StartYear,
        Person[] Subjects,
        IncomeSource[]? Incomes = null,
        Expense[]? Expenses = null
    )
{
    public Expense[] Expenses { get; init; } = Expenses ?? [];
}