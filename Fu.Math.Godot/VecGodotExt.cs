using System.Numerics;
using System.Runtime.CompilerServices;
using Fu.Math.Vec;

using Vector2 = Godot.Vector2;
using Vector2I = Godot.Vector2I;
using Vector3 = Godot.Vector3;
using Vector3I = Godot.Vector3I;

namespace Fu.Math.Godot;


public static class Vec2GodotExt
{
    public static Vector2 ToGodot(this Vec2<double> vec) =>
        new((float)vec.X, (float)vec.Y);

    
    public static Vector2 ToGodot(this Vec2<decimal> vec) =>
        new((float)vec.X, (float)vec.Y);

    
    public static Vector2 ToGodot(this Vec2<float> vec) =>
        new(vec.X, vec.Y);

    
    public static Vector2 ToGodot(this Vec2<int> vec) =>
        new(vec.X, vec.Y);

    
    public static Vector2I ToGodotI(this Vec2<int> vec) =>
        new(vec.X, vec.Y);

    
    public static Vector2 ToGodot(this Vec2<long> vec) =>
        new(vec.X, vec.Y);


    public static Vector2 ToGodot<T>(this Vec2<T> vec)
        where T : unmanaged, INumberBase<T>
        => new(float.CreateChecked(vec.X), float.CreateChecked(vec.Y));



    public static Vec2<int> ToVec(this Vector2I vec) =>
        Vec2.Create(vec.X, vec.Y);

    public static Vec2<float> ToVec(this Vector2 vec) =>
        Vec2.Create(vec.X, vec.Y);

    public static Vec2<double> ToVecDouble(this Vector2 vec) =>
        Vec2.Create<double>(vec.X, vec.Y);

    public static Vec2<T> ToVec<T>(this Vector2 vec)
        where T : unmanaged, INumberBase<T>
        => Vec2.Create(T.CreateChecked(vec.X), T.CreateChecked(vec.Y));
}


public static class Vec3GodotExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToGodot(this Vec3<double> vec) => 
        new ((float) vec.X, (float) vec.Y, (float) vec.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToGodot(this Vec3<decimal> vec) => 
        new ((float) vec.X, (float) vec.Y, (float) vec.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToGodot(this Vec3<float> vec) => 
        new (vec.X, vec.Y, vec.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToGodot(this Vec3<int> vec) => 
        new (vec.X, vec.Y, vec.Z);
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToGodot(this Vec3<long> vec) => 
        new (vec.X, vec.Y, vec.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToGodot<T>(this Vec3<T> vec)
        where T: unmanaged, INumberBase<T> => new (
            float.CreateChecked(vec.X), 
            float.CreateChecked(vec.Y), 
            float.CreateChecked(vec.Z)
        );
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<int> ToVec(this in Vector3I vec) =>
        new (vec.X, vec.Y, vec.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<float> ToVec(this in Vector3 vec) =>
        new (vec.X, vec.Y, vec.Z);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vec3<T> ToVec<T>(this in Vector3 vec)
        where T: unmanaged, INumberBase<T> => Vec3.Create(
            T.CreateChecked(vec.X), 
            T.CreateChecked(vec.Y), 
            T.CreateChecked(vec.Z)
        );
    
}


public static class GodotVectorCompatExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToGodot(this in Vector2 vec) => vec;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToGodot(this in Vector3 vec) => vec;
}
