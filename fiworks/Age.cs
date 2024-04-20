namespace FIWorks;

public readonly struct Age(byte value)
{
    private readonly byte value = value;

    public static implicit operator Byte(Age age) => age.value;
}
