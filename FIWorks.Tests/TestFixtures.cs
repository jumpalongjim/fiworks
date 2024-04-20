using FIWorks.Configuration;

namespace FIWorks.Tests;

public static class TestFixtures
{
    public static Person Subject(byte retirementAge) =>
        new Person("James", DateOnly.Parse("1969-Feb-19"), new Age(retirementAge), [new InvestmentData("I1", 10m, .002m)]);

}
