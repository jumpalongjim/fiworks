namespace FIWorks;

public struct Year(ushort value)
{
    private readonly ushort value = value;

    public Year() : this(0) {}
    public Year(uint intValue) : this((ushort)intValue) {}

    public static Year Undefined => new Year();

    public static bool operator ==(Year left, Year right) => left.value == right.value;
    public static bool operator !=(Year left, Year right) => !(left.value == right.value);
    public static bool operator <=(Year left, Year right) => left.value <= right.value;
    public static bool operator >=(Year left, Year right) => left.value >= right.value;

    public static ushort operator -(Year left, Year right) => (ushort)(left.value - right.value);
    public static Year operator +(Year start, int additional) => new Year((ushort)(start.value + additional));
    public static Year operator -(Year start, int deduction) => new Year((ushort)(start.value - deduction));
    public static implicit operator uint(Year year) => (uint)(year.value);
    // public static implicit operator int(Year year) => (int)(year.value);

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        if (obj is Year)
        {
            return this == (Year)obj;
        }
        return false;
    }

    public override int GetHashCode() => value.GetHashCode();

    public override string ToString() => value == 0 ? "Year undefined" : value.ToString();
}
