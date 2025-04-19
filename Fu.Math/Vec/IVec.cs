using System;

namespace Fu.Math.Vec;


public interface IVec<T>
{
    ReadOnlySpan<T> Components { get; }
}


public interface IVec2<T> : IVec<T> where T : struct {
    T X => Components[0];
    T Y => Components[1];
}

public interface IVec3<T> : IVec<T>  where T : struct {
    T X => Components[0];
    T Y => Components[1];
    T Z => Components[2];
}

public interface IVec4<T> : IVec<T>  where T : struct {
    T X { get; }
    T Y { get; }
    T Z { get; }
    T W { get; }
}