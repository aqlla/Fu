using System.Numerics;
using Fu.Math.Vec;

using Vector2 = Godot.Vector2;
using Vector2I = Godot.Vector2I;
using Vector3 = Godot.Vector3;
using Vector3I = Godot.Vector3I;

namespace Fu.Math.Godot;


public static class Vec2GodotExt
{
    public static Vector2 ToGodot(this IVec2<double> vec) =>
        new((float)vec.X, (float)vec.Y);

    
    public static Vector2 ToGodot(this IVec2<decimal> vec) =>
        new((float)vec.X, (float)vec.Y);

    
    public static Vector2 ToGodot(this IVec2<float> vec) =>
        new(vec.X, vec.Y);

    
    public static Vector2 ToGodot(this IVec2<int> vec) =>
        new(vec.X, vec.Y);

    
    public static Vector2I ToGodotI(this IVec2<int> vec) =>
        new(vec.X, vec.Y);

    
    public static Vector2 ToGodot(this IVec2<long> vec) =>
        new(vec.X, vec.Y);


    public static Vector2 ToGodot<T>(this IVec2<T> vec)
        where T : struct, INumberBase<T>
        => new(float.CreateChecked(vec.X), float.CreateChecked(vec.Y));



    public static Vec2<int> ToVec(this Vector2I vec) =>
        Vec2.Create(vec.X, vec.Y);

    public static Vec2<float> ToVec(this Vector2 vec) =>
        Vec2.Create(vec.X, vec.Y);

    public static Vec2<double> ToVecDouble(this Vector2 vec) =>
        Vec2.Create<double>(vec.X, vec.Y);

    public static Vec2<T> ToVec<T>(this Vector2 vec)
        where T : struct, INumberBase<T>
        => Vec2.Create(T.CreateChecked(vec.X), T.CreateChecked(vec.Y));
}


public static class Vec3GodotExt
{
    public static Vector3 ToGodot(this IVec3<double> vec) => 
        new ((float) vec.X, (float) vec.Y, (float) vec.Z);
    
    
    public static Vector3 ToGodot(this IVec3<decimal> vec) => 
        new ((float) vec.X, (float) vec.Y, (float) vec.Z);
    
    
    public static Vector3 ToGodot(this IVec3<float> vec) => 
        new (vec.X, vec.Y, vec.Z);

    
    public static Vector3 ToGodot(this IVec3<int> vec) => 
        new (vec.X, vec.Y, vec.Z);
    
    
    public static Vector3 ToGodot(this IVec3<long> vec) => 
        new (vec.X, vec.Y, vec.Z);

    
    public static Vector3 ToGodot<T>(this IVec3<T> vec)
        where T: struct, INumberBase<T> => new (
            float.CreateChecked(vec.X), 
            float.CreateChecked(vec.Y), 
            float.CreateChecked(vec.Z)
        );
    
    
    public static Vec3<int> ToVec(this Vector3I vec) => 
        Vec3.Create(vec.X, vec.Y, vec.Z);
    
    public static Vec3<float> ToVec(this Vector3 vec) =>
        Vec3.Create(vec.X, vec.Y, vec.Z);
    
    public static Vec3<double> ToVecDouble(this Vector3 vec) =>
        Vec3.Create<double>(vec.X, vec.Y, vec.Z);
    
    public static Vec3<T> ToVec<T>(this Vector3 vec) 
        where T: struct, INumberBase<T> => Vec3.Create(
            T.CreateChecked(vec.X), 
            T.CreateChecked(vec.Y), 
            T.CreateChecked(vec.Z)
        );
    
}
