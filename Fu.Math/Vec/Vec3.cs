using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Fu.Math.Vec;



public static class Vec3
{
    public static Vec3<T> Create<T>(T x, T y, T z) 
        where T : struct, INumberBase<T> => new (x, y, z);
    
    public static Vec3<T> Create<T>(T x) 
        where T : struct, INumberBase<T> => new (x, x, x);

    public static Vec3<T> Create<T>(Func<T> fn)
        where T : struct, INumberBase<T>
        => new (fn(), fn(), fn());

    public static Vec3<T> Create<T>(Func<int, T> fn)
        where T : struct, INumberBase<T>
        => new (fn(0), fn(1), fn(2));


    public static Vec3<T> Create<T>((T, T, T) tuple)
        where T : struct, INumberBase<T>
        => new (tuple.Item1, tuple.Item2, tuple.Item3);


    public static Vec3<T> CreateNonzero<T>(Func<T> fn)
        where T : struct, INumberBase<T>
    {
        var vec = Create(fn);

        while (vec.IsZero) {
            vec = Create(fn);
        }

        return vec;
    }

    public static Vec3<T> CreateNonzero<T>(Func<int, T> fn)
        where T : struct, INumberBase<T>
    {
        var vec = Create(fn);

        while (vec.IsZero) {
            vec = Create(fn);
        }

        return vec;
    }

    public static Vec3<T> FromSpherical<T>(T phi, T theta, T r)
        where T : struct, IFloatingPointIeee754<T>
        => Cartesian.FromSpherical(phi, theta, r);


    public static Vec3<T> FromSpherical<T>(T phi, T theta)
        where T : struct, IFloatingPointIeee754<T>
        => Cartesian.FromSpherical(phi, theta);


    public static Vec3<T> FromCylindrical<T>(T theta, T r, T z)
        where T : struct, IFloatingPointIeee754<T>
        => Cartesian.FromCylindrical(theta, r, z);


    public static Vec3<T> FromXz<T>(T x, T z)
        where T : struct, INumberBase<T>
        => new (x, T.Zero, z);


    public static Vec3<T> FromXz<T>((T x, T z) tuple)
        where T : struct, INumberBase<T>
        => new (tuple.x, T.Zero, tuple.z);

    public static Vec3<T> FromXz<T>(Vec2<T> xz)
        where T : struct, INumberBase<T>
        => new (xz.X, T.Zero, xz.Y);


    public static Vec3<T> FromXy<T>(T x, T y)
        where T : struct, INumberBase<T>
        => new (x, y, T.Zero);
}



[StructLayout(LayoutKind.Sequential)]
public readonly record struct Vec3<T>(T X, T Y, T Z) : IVec3<T>
    where T : struct, INumberBase<T>
{
    public static Vec3<T> One => Vec3.Create(T.One);
    public static Vec3<T> Zero => default;
    public static Vec3<T> NegOne => Vec3.Create(-T.One);

    public bool IsZero =>
        X == T.Zero && Y == T.Zero && Z == T.Zero;
    
    public T this[int index] => index switch
    {
        0 => X,
        1 => Y,
        2 => Z,
        _ => throw new IndexOutOfRangeException($"Index {index} is out of range [0..2]")
    };


    public static implicit operator Vec3<T>((T, T, T) tuple) =>
        Vec3.Create(tuple);
    
    public static Vec3<T> operator +(Vec3<T> l, IVec3<T> r) => 
        new (l.X + r.X, l.Y + r.Y, l.Z + r.Z);
    
    public static Vec3<T> operator -(Vec3<T> l, IVec3<T> r) => 
        new (l.X - r.X, l.Y - r.Y, l.Z - r.Z);
    
    public static Vec3<T> operator -(Vec3<T> vec) => 
        new (-vec.X, -vec.Y, -vec.Z);

    public static Vec3<T> operator *(Vec3<T> l, IVec3<T> r) => 
        new (l.X * r.X, l.Y * r.Y, l.Z * r.Z);
    
    public static Vec3<T> operator *(Vec3<T> l, T r) => 
        new (l.X * r, l.Y * r, l.Z * r);

    public static Vec3<T> operator *(T l, Vec3<T> r) =>
        new (l * r.X, l * r.Y, l * r.Z);
    
    public static Vec3<T> operator /(Vec3<T> l, IVec3<T> r) => 
        new (l.X / r.X, l.Y / r.Y, l.Z / r.Z);
    
    public static Vec3<T> operator /(Vec3<T> l, T r) => 
        new (l.X / r, l.Y / r, l.Z / r);

    public static Vec3<T> operator /(T l, Vec3<T> r) =>
        new (l / r.X, l / r.Y, l / r.Z);
}


public static class MathVec3DExt
{
    public static Vec3<T> Pow<T>(this IVec3<T> self, T power)
        where T : struct, IPowerFunctions<T>
        => (Math.Pow(self.X, power), Math.Pow(self.Y, power), Math.Pow(self.Z, power));

    public static Vec3<T> Add<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => (self.X + other.X, self.Y + other.Y, self.Z + other.Z);
    
    public static Vec3<T> Sub<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T>
        => (self.X - other.X, self.Y - other.Y, self.Z - other.Z);
    
    
    public static Vec3<T> Negate<T>(this IVec3<T> self) 
        where T : struct, INumberBase<T>
        => (-self.X, -self.Y, -self.Z);
    
    
    public static Vec3<T> Mul<T>(this IVec3<T> self, T other) 
        where T : struct, INumberBase<T>
        => Vec3.Create(self.X * other, self.Y * other, self.Z * other);

    public static Vec3<T> Mul<T>(this T n, IVec3<T> other)
        where T : struct, INumberBase<T>
        => Vec3.Create(n * other.X, n * other.Y, n * other.Z);

    
    public static Vec3<T> Div<T>(this IVec3<T> self, T other) 
        where T : struct, INumberBase<T>
        => Vec3.Create(self.X / other, self.Y / other, self.Z / other);
    
    public static Vec3<T> Div<T>(this T n, IVec3<T> other) 
        where T : struct, INumberBase<T>
        => Vec3.Create(n / other.X, n / other.Y, n / other.Z);
    
    
    public static T DistanceSquared<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => Sub(other, self).LengthSquared();
    
    
    public static T Distance<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, IRootFunctions<T> 
        => Sub(other, self).Length();
    
    
    public static T AngleTo<T>(this IVec3<T> self, IVec3<T> other)
        where T : struct, IFloatingPointIeee754<T>
        => T.Atan2(Cross(self, other).Length(), self.Dot(other));
    
    
    public static Vec3<T> Cross<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => Vec3.Create(
            self.Y * other.Z - self.Z * other.Y, 
            self.Z * other.X - self.X * other.Z, 
            self.X * other.Y - self.Y * other.X
        );

    
    public static Vec3<T> Normal<T>(this IVec3<T> self) 
        where T : struct, IRootFunctions<T>, IComparisonOperators<T, T, bool>
        => self.LengthSquared() is var r2 && r2 > T.Zero 
            ? Div(self, T.Sqrt(r2)) : Vec3.Create(T.Zero);
    
    
    public static Vec3<T> Lerp<T>(this IVec3<T> self, IVec3<T> to, T weight)
        where T : struct, IFloatingPointIeee754<T>
        => Vec3.Create(
            T.Lerp(self.X, to.X, weight), 
            T.Lerp(self.Y, to.Y, weight),
            T.Lerp(self.Z, to.Z, weight)
        );
}
