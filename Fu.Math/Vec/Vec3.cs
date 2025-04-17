using System.Numerics;

namespace Fu.Math.Vec;

public readonly record struct Vec3<T>(T X, T Y, T Z) :
    IVec3<Vec3<T>, T>,
    IMathVecOperators<Vec3<T>, T>,
    IMathVecScalarOperators<Vec3<T>, T>
    where T : struct, INumberBase<T>
{
    public static Vec3<T> Create(T x) => new(x, x, x);
    public static Vec3<T> Create(T x, T y, T z) => new(x, y, z);
    
    
    public static Vec3<T> Zero => Create(T.Zero);
    public static Vec3<T> One => Create(T.One);
    
    
    public static Vec3<T> operator -(Vec3<T> vec) => 
        new(-vec.X, -vec.Y, -vec.Z);
    
    
    public static Vec3<T> operator +(Vec3<T> l, Vec3<T> r) => 
        new(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
    
    public static Vec3<T> operator -(Vec3<T> l, Vec3<T> r) => 
        new(l.X - r.X, l.Y - r.Y, l.Z - r.Z);

    
    public static Vec3<T> operator *(Vec3<T> l, Vec3<T> r) => 
        new(l.X * r.X, l.Y * r.Y, l.Z * r.Z);
    
    public static Vec3<T> operator *(Vec3<T> l, T r) => 
        new(l.X * r, l.Y * r, l.Z * r);
    
    public static Vec3<T> operator *(T l, Vec3<T> r) => 
        new(l * r.X, l * r.Y, l * r.Z);

    
    public static Vec3<T> operator /(Vec3<T> l, Vec3<T> r) => 
        new(l.X / r.X, l.Y / r.Y, l.Z / r.Z);
    
    public static Vec3<T> operator /(Vec3<T> l, T r) => 
        new(l.X / r, l.Y / r, l.Z / r);

    
    public T Dot(Vec3<T> other) => 
        X * other.X + Y * other.Y + Z * other.Z;
}


public static class MathVec3DExt
{
    public static T DistanceSquared<T>(this Vec3<T> self, Vec3<T> other) 
        where T : struct, INumberBase<T> 
        => (other - self).LengthSquared();
    
    
    public static T Distance<T>(this Vec3<T> self, Vec3<T> other) 
        where T : struct, IRootFunctions<T> 
        => (other - self).Length();

    public static T LengthSquared<T>(this Vec3<T> self) 
        where T : struct, INumberBase<T> 
        => self.Dot(self);
    
    
    public static T Length<T>(this Vec3<T> self) 
        where T : struct, IRootFunctions<T> 
        => T.Sqrt(self.LengthSquared());
    
    public static Vec3<T> Cross<T>(this Vec3<T> self, Vec3<T> other) 
        where T : struct, INumberBase<T> 
        => new (
            self.Y * other.Z - self.Z * other.Y, 
            self.Z * other.X - self.X * other.Z, 
            self.X * other.Y - self.Y * other.X
        );
    
    public static T AngleTo<T>(this Vec3<T> self, Vec3<T> other)
        where T : struct, IFloatingPointIeee754<T>
        => T.Atan2(Length(Cross(self, other)), self.Dot(other));
    
    
    public static Vec3<T> Normal<T>(this Vec3<T> self) 
        where T : struct, IRootFunctions<T>, IComparisonOperators<T, T, bool>
        => self.LengthSquared() is T m && m > T.Zero 
            ? self / T.Sqrt(m) : Vec3<T>.Zero;
    
    public static Vec3<T> Lerp<T>(this Vec3<T> self, Vec3<T> to, T weight)
        where T : struct, IFloatingPointIeee754<T>
        => Vec3<T>.Create(
            T.Lerp(self.X, to.X, weight), 
            T.Lerp(self.Y, to.Y, weight),
            T.Lerp(self.Z, to.Z, weight)
        );
}
