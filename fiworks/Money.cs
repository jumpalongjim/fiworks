namespace FIWorks;

public readonly struct Money(decimal value)
{
    private readonly decimal value = value;

    public bool IsPositive => value >= 0m;
    public static Money Zero => new Money(0m);
    public static Money operator +(Money a, Money b) => new Money(a.value + b.value);
    public static Money operator -(Money a, Money b) => new Money(a.value - b.value);
    public static Money operator *(Money a, decimal b) => new Money(a.value * b);
    public static implicit operator decimal(Money sterling) => sterling.value;

    public override string ToString() => value.ToString("#,0");
    public string Thousands() => value.ToString("#,");

    public static Cashflow ConstantCashflow(decimal value, Year start, int count) =>
        new Cashflow(start, Enumerable.Repeat(new Money(value), count));

}
