using Fu.Math.Vec;

namespace Fu.Tests;

public class Class1
{

    public static void Main()
    {
        var v2i = new Vec2<int>(1, 2);
        var v2d = new Vec2<double>(1, 2);
        
        
        var res = v2i.Dot(v2i);

        var resd = v2d.Dot(v2d);
        var norm = v2d.Normal();
        var lerp = v2d.Lerp(v2d, 0.5);
        
        Console.WriteLine(v2i);
    }
    
}