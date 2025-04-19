using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Fu.Math.Vec;



public static class Vec3
{
    public static Vec3<T> Create<T>(T x, T y, T z) 
        where T : struct, INumberBase<T> => new(x, y, z);
    
    public static Vec3<T> Create<T>(T x) 
        where T : struct, INumberBase<T> => new(x, x, x);
}



[StructLayout(LayoutKind.Sequential)]
public readonly struct Vec3<T>(T x, T y, T z) : IVec3<T>
    where T : struct, INumberBase<T>
{
    private readonly T[] components = [x, y, z];
    
    public ReadOnlySpan<T> Components => components;

    
    public T X => components[0];
    public T Y => components[1];
    public T Z => components[2];
    
    
    public static Vec3<T> Zero => Vec3.Create(T.Zero);
    public static Vec3<T> One => Vec3.Create(T.One);
    
    
    public static Vec3<T> operator +(Vec3<T> l, IVec3<T> r) => 
        new(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
    
    public static Vec3<T> operator -(Vec3<T> l, IVec3<T> r) => 
        new(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
    
    public static Vec3<T> operator -(Vec3<T> vec) => 
        new(-vec.X, -vec.Y, -vec.Z);

    public static Vec3<T> operator *(Vec3<T> l, IVec3<T> r) => 
        new(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
    
    public static Vec3<T> operator *(Vec3<T> l, T r) => 
        new(l.X * r, l.Y * r, l.Z * r);
    
    public static Vec3<T> operator /(Vec3<T> l, IVec3<T> r) => 
        new(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
    
    public static Vec3<T> operator /(Vec3<T> l, T r) => 
        new(l.X / r, l.Y / r, l.Z / r);

}


public static class MathVec3DExt
{
    public static Vec3<T> Add<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => Vec3.Create(self.X + other.X, self.Y + other.Y, self.Z + other.Z);
    
    public static Vec3<T> Sub<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T>
        => Vec3.Create(self.X - other.X, self.Y - other.Y, self.Z - other.Z);
    
    
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

    
    public static Vec3<T> Negate<T>(this IVec3<T> self) 
        where T : struct, INumberBase<T>
        => Vec3.Create(-self.X, -self.Y, -self.Z);
    

    public static T Dot<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => self.X * other.X + self.Y * other.Y + self.Z * other.Z;
    
    
    public static T DistanceSquared<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => LengthSquared(Sub(other, self));
    
    
    public static T Distance<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, IRootFunctions<T> 
        => Length(Sub(other, self));

    
    public static T LengthSquared<T>(this IVec3<T> self) 
        where T : struct, INumberBase<T> 
        => Dot(self, self);
    
    
    public static T Length<T>(this IVec3<T> self) 
        where T : struct, IRootFunctions<T> 
        => T.Sqrt(LengthSquared(self));
    
    
    public static Vec3<T> Cross<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => Vec3.Create(
            self.Y * other.Z - self.Z * other.Y, 
            self.Z * other.X - self.X * other.Z, 
            self.X * other.Y - self.Y * other.X
        );
    
    
    public static T AngleTo<T>(this IVec3<T> self, IVec3<T> other)
        where T : struct, IFloatingPointIeee754<T>
        => T.Atan2(Length(Cross(self, other)), Dot(self, other));
    
    
    public static Vec3<T> Normal<T>(this IVec3<T> self) 
        where T : struct, IRootFunctions<T>, IComparisonOperators<T, T, bool>
        => LengthSquared(self) is var r2 && r2 > T.Zero 
            ? Div(self, T.Sqrt(r2)) : Vec3.Create(T.Zero);
    
    
    public static Vec3<T> Lerp<T>(this IVec3<T> self, IVec3<T> to, T weight)
        where T : struct, IFloatingPointIeee754<T>
        => Vec3.Create(
            T.Lerp(self.X, to.X, weight), 
            T.Lerp(self.Y, to.Y, weight),
            T.Lerp(self.Z, to.Z, weight)
        );
}
