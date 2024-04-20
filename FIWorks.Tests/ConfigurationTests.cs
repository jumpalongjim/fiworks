using FIWorks.Configuration;

namespace FIWorks.Tests;

public class ConfigurationTests
{
    [Fact]
    public void RetirementHappensAfterStartYear()
    {
        var config = new Config(
            StartYear: new Year(2029),
            Subjects: [ TestFixtures.Subject(30) ]
        );
        Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Run(config));
    }
    
}
