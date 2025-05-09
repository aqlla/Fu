using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fu.Math.Vec;



[StructLayout(LayoutKind.Sequential)]
public readonly record struct Vec3<T>(T X, T Y, T Z)
    where T : unmanaged, INumberBase<T>
{
    public static readonly T ScalarTwo = T.CreateChecked(2);
    public static readonly T ScalarHalf = T.CreateChecked(0.5);

    public static readonly Vec3<T> One    = new (T.One);
    public static readonly Vec3<T> Zero   = new (T.Zero);
    public static readonly Vec3<T> NegOne = new (-T.One);
    public static readonly Vec3<T> Half   = new (ScalarHalf);
    public static readonly Vec3<T> Two    = new (ScalarTwo);

    public static readonly Vec3<T> Up      = new ( T.Zero,  T.One,   T.Zero);
    public static readonly Vec3<T> Down    = new ( T.Zero, -T.One,   T.Zero);
    public static readonly Vec3<T> Right   = new ( T.One,   T.Zero,  T.Zero);
    public static readonly Vec3<T> Left    = new (-T.One,   T.Zero,  T.Zero);
    public static readonly Vec3<T> Forward = new ( T.Zero,  T.Zero, -T.One );
    public static readonly Vec3<T> Back    = new ( T.Zero,  T.Zero,  T.One );


    public bool IsZero => X == T.Zero && Y == T.Zero && Z == T.Zero;

    public T this[int index] => index switch {
        0 => X,
        1 => Y,
        2 => Z,
        _ => throw new IndexOutOfRangeException($"Index {index} is out of range [0..2]")
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vec3(T value) : this(value, value, value) {}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vec3(ReadOnlySpan<T> vs) : this(vs[0], vs[1], vs[2]) {}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vec3<T>((T, T, T) tuple) =>
        new (tuple.Item1, tuple.Item2, tuple.Item3);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator (T x, T y, T z)(Vec3<T> vec) => vec.Tuple();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (T x, T y, T z) Tuple() => (X, Y, Z);


#region math operators

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator +(Vec3<T> l, Vec3<T> r) =>
        new (l.X + r.X, l.Y + r.Y, l.Z + r.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator -(Vec3<T> l, Vec3<T> r) =>
        new (l.X - r.X, l.Y - r.Y, l.Z - r.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator -(Vec3<T> vec) =>
        new (-vec.X, -vec.Y, -vec.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator *(Vec3<T> l, Vec3<T> r) =>
        new (l.X * r.X, l.Y * r.Y, l.Z * r.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator *(Vec3<T> l, T r) =>
        new (l.X * r, l.Y * r, l.Z * r);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator *(T l, Vec3<T> r) =>
        new (l * r.X, l * r.Y, l * r.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator /(Vec3<T> l, Vec3<T> r) =>
        new (l.X / r.X, l.Y / r.Y, l.Z / r.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator /(Vec3<T> l, T r) =>
        new (l.X / r, l.Y / r, l.Z / r);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> operator /(T l, Vec3<T> r) =>
        new (l / r.X, l / r.Y, l / r.Z);
#endregion
}



public static class Vec3
{
    public static Vec3<T> Create<T>(T x, T y, T z)
        where T : unmanaged, INumberBase<T> => new(x, y, z);

    public static Vec3<T> Create<T>(T x)
        where T : unmanaged, INumberBase<T> => new(x, x, x);

    public static Vec3<T> Create<T>(Func<int, T> fn)
        where T : unmanaged, INumberBase<T>
        => new(fn(0), fn(1), fn(2));

    public static Vec3<T> Create<T>((T, T, T) tuple)
        where T : unmanaged, INumberBase<T>
        => new(tuple.Item1, tuple.Item2, tuple.Item3);

    public static Vec3<T> CreateNonzero<T>(Func<int, T> fn)
        where T : unmanaged, INumberBase<T>
    {
        Vec3<T> vec;
        do { vec = Create(fn); } while (vec.IsZero);

        return vec;
    }
}


public static class Vec
{
    public static Vec3<T> FromSpherical<T>(T phi, T theta, T r)
        where T : unmanaged, IFloatingPointIeee754<T>
        => Cartesian.FromSpherical(phi, theta, r);

    public static Vec3<T> FromSpherical<T>(T phi, T theta)
        where T : unmanaged, IFloatingPointIeee754<T>
        => Cartesian.FromSpherical(phi, theta);

    public static Vec3<T> FromCylindrical<T>(T theta, T r, T z)
        where T : unmanaged, IFloatingPointIeee754<T>
        => Cartesian.FromCylindrical(theta, r, z);

    public static Vec3<T> FromXz<T>(T x, T z)
        where T : unmanaged, INumberBase<T>
        => new(x, T.Zero, z);

    public static Vec3<T> FromXz<T>((T x, T z) tuple)
        where T : unmanaged, INumberBase<T>
        => new(tuple.x, T.Zero, tuple.z);

    public static Vec3<T> FromXz<T>(Vec2<T> xz)
        where T : unmanaged, INumberBase<T>
        => new(xz.X, T.Zero, xz.Y);

    public static Vec3<T> FromXy<T>(T x, T y)
        where T : unmanaged, INumberBase<T>
        => new(x, y, T.Zero);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Sum<T>(this IEnumerable<Vec3<T>> source)
        where T : unmanaged, INumberBase<T>
        => Sum<T>(source.ToArray().AsSpan());


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Sum<T>(this ReadOnlySpan<Vec3<T>> source)
        where T : unmanaged, INumberBase<T>
    {
        Span<T> sums = stackalloc T[3];
        //[source[0].X, source[0].Y, source[0].Z];

        foreach (var t in source) {
            sums[0] += t.X;
            sums[1] += t.Y;
            sums[2] += t.Z;
        }

        return new Vec3<T>(sums);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Avg<T>(this IEnumerable<Vec3<T>> source)
        where T : unmanaged, INumberBase<T>
        => Avg<T>(source.ToArray().AsSpan());


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Avg<T>(this ReadOnlySpan<Vec3<T>> source)
        where T : unmanaged, INumberBase<T>
        => Sum(source) / T.CreateChecked(source.Length);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Negate<T>(this Vec3<T> vec)
        where T : unmanaged, INumberBase<T>
        => new (-vec.X, -vec.Y, -vec.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Add<T>(this Vec3<T> vec, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => new (vec.X + other.X, vec.Y + other.Y, vec.Z + other.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Sub<T>(this Vec3<T> vec, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => new (vec.X - other.X, vec.Y - other.Y, vec.Z - other.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Mul<T>(this Vec3<T> vec, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => new(vec.X * other.X, vec.Y * other.Y, vec.Z * other.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Mul<T>(this Vec3<T> vec, T other)
        where T : unmanaged, INumberBase<T>
        => new (vec.X * other, vec.Y * other, vec.Z * other);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Mul<T>(T n, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => new (n * other.X, n * other.Y, n * other.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Div<T>(this Vec3<T> vec, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => new (vec.X / other.X, vec.Y / other.Y, vec.Z / other.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Div<T>(this Vec3<T> vec, T other)
        where T : unmanaged, INumberBase<T>
        => new (vec.X / other, vec.Y / other, vec.Z / other);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Div<T>(T n, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => new (n / other.X, n / other.Y, n / other.Z);



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Pow<T>(this Vec3<T> vec, T power)
        where T : unmanaged, IPowerFunctions<T>
        => new (Math.Pow(vec.X, power), Math.Pow(vec.Y, power), Math.Pow(vec.Z, power));



    // Get an up vector which is non-collinear with the normal.
    public static Vec3<T> Up<T>(this Vec3<T> normal)
        where T : unmanaged, INumberBase<T>, IComparisonOperators<T, T, bool> =>
        T.Abs(Dot(normal, Vec3<T>.Up)) < T.CreateChecked(0.99)
            ? Vec3<T>.Up : Vec3<T>.Forward;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Normal<T>(this Vec3<T> self)
        where T : unmanaged, IRootFunctions<T>, IComparisonOperators<T, T, bool>
        => LengthSquared(self) is var r2 && r2 > T.Zero
            ? Div(self, T.Sqrt(r2))
            : Vec3<T>.Zero;


    public static (Vec3<T> tan, Vec3<T> bitan) Orthonormal<T>(this Vec3<T> normal)
        where T : unmanaged, IRootFunctions<T>, IComparisonOperators<T, T, bool>
    {
        var tan = normal.Cross(Up(normal));
        var bitan = normal.Cross(tan);
        return (Normal(tan), Normal(bitan));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Dot<T>(this Vec3<T> vec, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => vec.X * other.X + vec.Y * other.Y + vec.Z * other.Z;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T LengthSquared<T>(this Vec3<T> vec)
        where T : unmanaged, INumberBase<T>
        => Dot(vec, vec);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Length<T>(this Vec3<T> vec)
        where T : unmanaged, IRootFunctions<T>
        => T.Sqrt(Dot(vec, vec));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T DistanceSquaredTo<T>(this Vec3<T> self, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => Dot(other, self);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T DistanceTo<T>(this Vec3<T> self, Vec3<T> other)
        where T : unmanaged, IRootFunctions<T>
        => T.Sqrt(Dot(other, self));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Cross<T>(this Vec3<T> self, Vec3<T> other)
        where T : unmanaged, INumberBase<T>
        => new (
            self.Y * other.Z - self.Z * other.Y,
            self.Z * other.X - self.X * other.Z,
            self.X * other.Y - self.Y * other.X
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T AngleTo<T>(this Vec3<T> self, Vec3<T> other)
        where T : unmanaged, IFloatingPointIeee754<T>
        => T.Atan2(Length(Cross(self, other)), Dot(self, other));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T AzimuthTo<T>(this Vec3<T> self, Vec3<T> other)
        where T : unmanaged, IFloatingPointIeee754<T>
        => AzimuthTo(self, other, Orthonormal(self.Normal()));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T AzimuthTo<T>(
        this Vec3<T> self, Vec3<T> other, (Vec3<T> tan, Vec3<T> bitan) orthonormal)
        where T : unmanaged, IFloatingPointIeee754<T>
        => AzimuthTo(self, other, orthonormal.tan, orthonormal.bitan);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T AzimuthTo<T>(this Vec3<T> self, Vec3<T> other, Vec3<T> tangent, Vec3<T> bitangent)
        where T : unmanaged, IFloatingPointIeee754<T>
    {
        var disp = other - self;
        return Math.Atan2(disp.Dot(bitangent), disp.Dot(tangent));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<Vec3<T>, T> AzimuthTo<T>(Vec3<T> self)
        where T : unmanaged, IFloatingPointIeee754<T>
    {
        var (tangent, bitangent) = Orthonormal(self.Normal());

        return other => {
            var disp = other - self;
            return Math.Atan2(disp.Dot(bitangent), disp.Dot(tangent));
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Mid<T>(this Vec3<T> v1, Vec3<T> v2)
        where T : unmanaged, INumberBase<T>
        => new(
            (v1.X + v2.X) * Vec3<T>.ScalarHalf,
            (v1.Y + v2.Y) * Vec3<T>.ScalarHalf,
            (v1.Z + v2.Z) * Vec3<T>.ScalarHalf
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> Lerp<T>(this Vec3<T> self, Vec3<T> to, T weight)
        where T : unmanaged, IFloatingPointIeee754<T>
        => new (
            T.Lerp(self.X, to.X, weight),
            T.Lerp(self.Y, to.Y, weight),
            T.Lerp(self.Z, to.Z, weight)
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] ToArray<T>(in Vec3<T> vec)
        where T : unmanaged, INumberBase<T>
        => [vec.X, vec.Y, vec.Z];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(in Vec3<T> vec)
        where T : unmanaged, INumberBase<T>
        => new ([vec.X, vec.Y, vec.Z]);
}
