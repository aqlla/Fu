using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Fu.Math.Vec;


public static class Vec2
{
    public static Vec2<T> Create<T>(T x, T y) 
        where T : struct, INumberBase<T> => new (x, y);

    public static Vec2<T> Create<T>((T, T) tuple)
        where T : struct, INumberBase<T> =>
        new (tuple.Item1, tuple.Item2);
    
    public static Vec2<T> Create<T>(T x) 
        where T : struct, INumberBase<T> => new (x, x);

    public static Vec2<T> Create<T>(Func<T> fn)
        where T : struct, INumberBase<T> =>
        new (fn(), fn());

    public static Vec2<T> Create<T>(Func<int, T> fn)
        where T : struct, INumberBase<T> =>
        new (fn(0), fn(1));

    public static Vec2<T> FromPolar<T>(T theta, T r)
        where T : struct, IFloatingPointIeee754<T>
        => Cartesian.FromPolar(theta, r);
}
    


[StructLayout(LayoutKind.Sequential)]
public readonly record struct Vec2<T>(T X, T Y) : IVec2<T>
    where T : struct, INumberBase<T>
{
    public static Vec2<T> Zero => default;
    public static Vec2<T> One => Vec2.Create(T.One);
    public static Vec2<T> NegOne => Vec2.Create(-T.One);

    public bool IsZero => X == T.Zero && Y == T.Zero;

    public T this[int index] => index switch {
        0 => X,
        1 => Y,
        _ => throw new IndexOutOfRangeException($"Index {index} is out of range [0..1]")
    };


    public static implicit operator Vec2<T>((T, T) tuple) =>
        new(tuple.Item1, tuple.Item2);
    
    
    public static Vec2<T> operator -(Vec2<T> vec) => 
        new(-vec.X, -vec.Y);
    
    
    public static Vec2<T> operator +(Vec2<T> l, IVec2<T> r) => 
        new(l.X + r.X, l.Y + r.Y);
    
    public static Vec2<T> operator -(Vec2<T> l, IVec2<T> r) => 
        new(l.X - r.X, l.Y - r.Y);
    
    
    public static Vec2<T> operator *(Vec2<T> l, IVec2<T> r) => 
        new(l.X * r.X, l.Y * r.Y);
    
    public static Vec2<T> operator *(Vec2<T> l, T r) => 
        new(l.X * r, l.Y * r);
    
    public static Vec2<T> operator *(T l, Vec2<T> r) => 
        new(l * r.X, l * r.Y);
    
    
    public static Vec2<T> operator /(Vec2<T> l, IVec2<T> r) => 
        new(l.X / r.X, l.Y / r.Y);
    
    public static Vec2<T> operator /(Vec2<T> l, T r) => 
        new(l.X / r, l.Y / r);

    public static Vec2<T> operator /(T l, Vec2<T> r) =>
        new (l / r.X, l / r.Y );
}



public static class MathVec2DExt
{
    public static Vec2<T> Pow<T>(this IVec2<T> self, T power)
        where T : struct, IPowerFunctions<T>
        => (Math.Pow(self.X, power), Math.Pow(self.Y, power));

    public static Vec2<T> Add<T>(this IVec2<T> self, IVec2<T> other) 
        where T : struct, INumberBase<T> 
        => (self.X + other.X, self.Y + other.Y);
    
    public static Vec2<T> Sub<T>(this IVec2<T> self, IVec2<T> other) 
        where T : struct, INumberBase<T>
        => (self.X - other.X, self.Y - other.Y);
    
    
    public static Vec2<T> Mul<T>(this IVec2<T> self, T other) 
        where T : struct, INumberBase<T>
        => (self.X * other, self.Y * other);

    public static Vec2<T> Mul<T>(this T n, IVec2<T> other)
        where T : struct, INumberBase<T>
        => Vec2.Create(n * other.X, n * other.Y);
    
    
    public static Vec2<T> Div<T>(this IVec2<T> self, T other) 
        where T : struct, INumberBase<T>
        => Vec2.Create(self.X / other, self.Y / other);

    public static Vec2<T> Div<T>(this T n, IVec2<T> other)
        where T : struct, INumberBase<T> 
        => Vec2.Create(n / other.X, n / other.Y);
    
    
    public static Vec2<T> Negate<T>(this IVec2<T> self) 
        where T : struct, INumberBase<T>
        => Vec2.Create(-self.X, -self.Y);
    
    
    
    public static T DistanceSquared<T>(this IVec2<T> self, IVec2<T> other) 
        where T : struct, INumberBase<T> 
        => Sub(other, self).LengthSquared();
    
    
    public static T Length<T>(this IVec2<T> self) 
        where T : struct, IRootFunctions<T> 
        => T.Sqrt(self.LengthSquared());
    
    
    public static T Distance<T>(this IVec2<T> self, IVec2<T> other) 
        where T : struct, IRootFunctions<T> 
        => Length(Sub(other, self));
    
    
    public static T Cross<T>(this IVec2<T> self, IVec2<T> other) 
        where T : struct, INumberBase<T> 
        => self.X * other.Y - self.Y * other.X;
    
    
    public static T AngleTo<T>(this IVec2<T> self, IVec2<T> other)
        where T : struct, IFloatingPointIeee754<T>
        => T.Atan2(Cross(self, other), self.Dot(other));
    
    
    public static Vec2<T> Normal<T>(this IVec2<T> self) 
        where T : struct, IComparisonOperators<T, T, bool>, IRootFunctions<T>
        => self.LengthSquared() is var r2 && r2 > T.Zero 
            ? Div(self, T.Sqrt(r2)) : Vec2.Create(T.Zero);
    
    
    public static Vec2<T> Lerp<T>(this IVec2<T> self, IVec2<T> to, T weight)
        where T : struct, IFloatingPointIeee754<T>
        => Vec2.Create(
            T.Lerp(self.X, to.X, weight),
            T.Lerp(self.Y, to.Y, weight)
        );
    
    
    public static Vec2<T> Slerp<T>(this IVec2<T> self, IVec2<T> target, T weight)
        where T : struct, IFloatingPointIeee754<T>
    {
        var r1 = self.LengthSquared();
        var r2 = target.LengthSquared();
    
        if (r1 == T.AdditiveIdentity || r2 == T.AdditiveIdentity)
            return Lerp(self, target, weight);
    
        var from = T.Sqrt(r1);
        var num = T.Lerp(from, T.Sqrt(r2), weight);
        return Rotate(self, self.AngleTo(target) * weight) * Math.Div(num, from);
    }
    
    
    public static Vec2<T> Rotate<T>(this IVec2<T> self, T angle)
        where T : struct, ITrigonometricFunctions<T>
    {
        var (sin, cos) = T.SinCos(angle);
        return Vec2.Create(
            x: self.X * cos - self.Y * sin, 
            y: self.X * sin + self.Y * cos
        );
    }
}
