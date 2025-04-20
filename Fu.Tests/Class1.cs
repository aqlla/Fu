using System.Numerics;
using Fu.Math.Vec;

namespace Fu.Tests;

public class Class1
{

    public static void Main()
    {
        var v2i = new Vec2<int>(1, 2);
        // var v2b = new Vec2<bool>(true, false);
        var v2d = new Vec2<double>(1, 2);
        var v3d = new Vec3<double>(1, 2, 3);
        var v3f = new Vec3<double>(1, 2, 3);


        var nv = Vector3.Zero;

        var iss = v3d == v3f;
        
        
        var res = v2i.Dot(v2i);

        var resd = v2d.Dot(v2d);
        var norm = v2d.Normal();
        var lerp = v2d.Lerp(v2d, 0.5);
        
        Console.WriteLine(v2i);
    }
    
}