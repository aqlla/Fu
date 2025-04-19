using System.Numerics;

namespace Fu.Math.Vec;


public interface IVec<T> where T : struct;


public interface IVec2<T> : IVec<T> 
    where T : struct 
{
    T X { get; }
    T Y { get; }
    
    void Deconstruct(out T x, out T y) => 
        (x, y) = (X, Y);
}


public interface IVec3<T> : IVec<T>
    where T : struct 
{
    T X { get; }
    T Y { get; }
    T Z { get; }
    
    void Deconstruct(out T x, out T y, out T z) => 
        (x, y, z) = (X, Y, Z);
}


public interface IVec4<T> : IVec<T>  
    where T : struct 
{
    T X { get; }
    T Y { get; }
    T Z { get; }
    T W { get; }
}




public static class IVec2Ext
{
    public static T Dot<T>(this IVec2<T> self, IVec2<T> other) 
        where T : struct, INumberBase<T> 
        => self.X * other.X + self.Y * other.Y;
    
    
    public static T LengthSquared<T>(this IVec2<T> self) 
        where T : struct, INumberBase<T> 
        => Dot(self, self);
    
    
    public static T Length<T>(this IVec2<T> self) 
        where T : struct, IRootFunctions<T> 
        => T.Sqrt(LengthSquared(self));
}



public static class IVec3Ext
{
    public static T Dot<T>(this IVec3<T> self, IVec3<T> other) 
        where T : struct, INumberBase<T> 
        => self.X * other.X + self.Y * other.Y + self.Z * other.Z;
    
    
    public static T LengthSquared<T>(this IVec3<T> self) 
        where T : struct, INumberBase<T> 
        => Dot(self, self);
    
    
    public static T Length<T>(this IVec3<T> self) 
        where T : struct, IRootFunctions<T> 
        => T.Sqrt(LengthSquared(self));
}
