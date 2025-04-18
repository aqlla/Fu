using System.Numerics;

namespace Fu.Math.Vec;


public readonly record struct Vec2<T>(T X, T Y) : 
    IVec2<Vec2<T>, T>,
    IMathVecOperators<Vec2<T>, T>, 
    IMathVecScalarOperators<Vec2<T>, T>
    where T : struct, INumberBase<T>
{
    public static Vec2<T> Create(T x) => new(x, x);
    public static Vec2<T> Create(T x, T y) => new(x, y);
    
    
    public static Vec2<T> Zero => Create(T.Zero);
    public static Vec2<T> One => Create(T.One);
    
    
    public static Vec2<T> operator -(Vec2<T> vec) => 
        new(-vec.X, -vec.Y);
    
    
    public static Vec2<T> operator +(Vec2<T> l, Vec2<T> r) => 
        new(l.X + r.X, l.Y + r.Y);
    
    public static Vec2<T> operator -(Vec2<T> l, Vec2<T> r) => 
        new(l.X - r.X, l.Y - r.Y);
    
    
    public static Vec2<T> operator *(Vec2<T> l, Vec2<T> r) => 
        new(l.X * r.X, l.Y * r.Y);
    
    public static Vec2<T> operator *(Vec2<T> l, T r) => 
        new(l.X * r, l.Y * r);
    
    public static Vec2<T> operator *(T l, Vec2<T> r) => 
        new(l * r.X, l * r.Y);
    
    
    public static Vec2<T> operator /(Vec2<T> l, Vec2<T> r) => 
        new(l.X / r.X, l.Y / r.Y);
    
    public static Vec2<T> operator /(Vec2<T> l, T r) => 
        new(l.X / r, l.Y / r);

    
    public T Dot(Vec2<T> other) => 
        X * other.X + Y * other.Y;
}


public static class MathVec2DExt
{
    public static T LengthSquared<T>(this Vec2<T> self) 
        where T : struct, INumberBase<T> 
        => self.Dot(self);
    
    public static T Cross<T>(this IVec2<T> self, Vec2<T> other) 
        where T : struct, INumberBase<T> 
        => self.X * other.Y - self.Y * other.X;
    
    public static T AngleTo<T>(this Vec2<T> self, Vec2<T> other)
        where T : struct, IFloatingPointIeee754<T>
        => T.Atan2(self.Cross(other), self.Dot(other));
    

    public static T AngleToPoint<T>(this IVec2<T> self, IVec2<T> to)
        where T : struct, IFloatingPointIeee754<T>
        => T.Atan2(to.Y - self.Y, to.X - self.X);
    
    public static Vec2<T> Normal<T>(this Vec2<T> self) 
        where T : struct, 
            IComparisonOperators<T, T, bool>, 
            IRootFunctions<T>
        => self.LengthSquared() is var m && m > T.Zero 
            ? self / T.Sqrt(m) : Vec2<T>.Zero;
    
    
    public static Vec2<T> Lerp<T>(this Vec2<T> self, Vec2<T> to, T weight)
        where T : struct, IFloatingPointIeee754<T>
        => Vec2<T>.Create(
            T.Lerp(self.X, to.X, weight),
            T.Lerp(self.Y, to.Y, weight)
        );
    
    
    public static Vec2<T> Slerp<T>(this Vec2<T> self, Vec2<T> to, T weight)
        where T : struct, IFloatingPointIeee754<T>
    {
        var r1 = self.LengthSquared();
        var r2 = to.LengthSquared();
    
        if (r1 == T.AdditiveIdentity || r2 == T.AdditiveIdentity)
            return self.Lerp(to, weight);
    
        var from = T.Sqrt(r1);
        var num = T.Lerp(from, T.Sqrt(r2), weight);
        return self.Rotate(self.AngleTo(to) * weight) * Math.Div(num, from);
    }
    
    
    public static Vec2<T> Rotate<T>(this Vec2<T> self, T angle)
        where T : struct, ITrigonometricFunctions<T>
    {
        var (sin, cos) = T.SinCos(angle);
        return Vec2<T>.Create(
            x: self.X * cos - self.Y * sin, 
            y: self.X * sin + self.Y * cos
        );
    }
}
