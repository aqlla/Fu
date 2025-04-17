namespace Fu.Math.Vec;


public interface IVec<T>
{
    // ReadOnlySpan<T> Components { get; }
}

public interface IVec2<T> : IVec<T> where T : struct {
    T X { get; }
    T Y { get; }
}

public interface IVec3<T> : IVec<T>  where T : struct {
    T X { get; }
    T Y { get; }
    T Z { get; }
}

public interface IVec4<T> : IVec<T>  where T : struct {
    T X { get; }
    T Y { get; }
    T Z { get; }
    T W { get; }
}


public interface IVec2<out TSelf, T> : IVec2<T>
    where TSelf : struct, IVec2<TSelf, T> 
    where T : struct
{
    static abstract TSelf Create(T x, T y);
}


public interface IVec3<out TSelf, T> : IVec3<T>
    where TSelf : struct, IVec3<TSelf, T> 
    where T : struct

{
    static abstract TSelf Create(T x, T y, T z);
}


public interface IVec4<out TSelf, T> : IVec4<T> 
    where TSelf : struct, IVec4<TSelf, T>
    where T : struct 
{
    static abstract TSelf Create(T x, T y, T z, T w);
}
