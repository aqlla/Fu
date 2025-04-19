namespace Fu.Types;

public sealed class Unit : IEquatable<Unit>
{
    public static readonly Unit Instance = new();

    private Unit() {}

    public override bool Equals(object? obj) => obj is Unit;

    public bool Equals(Unit? other) => other is not null;

    public override int GetHashCode() => 0;

    public static bool operator ==(Unit? l, Unit? r) => true;

    public static bool operator !=(Unit? l, Unit? r) => false;

    public override string ToString() => nameof(Unit);
}
