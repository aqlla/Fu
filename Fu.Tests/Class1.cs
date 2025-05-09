using System.Numerics;
using Fu.Math.Vec;
using Fu.Seq;
using Fu.Seq.Extensions;

namespace Fu.Tests;

public sealed class Class1
{

    public static void Main()
    {
        var v1 = new Vec3<int>(1, 1, 2);
        var v2 = new Vec3<int>(3, 4, 8);
        var v3 = new Vec3<int>(9, 1, 2);

        var res = Seq.Seq.Of(v1, v2, v3).Sum();

        Console.WriteLine(res);
    }


    public static string CommaJoin<T>(IEnumerable<T> items) =>
        string.Join(",", items);

    public static void PrintCmp<T1, T2>(IEnumerable<T1[]> xss, IEnumerable<T2[]> yss) {
        var i = 0;
        foreach (var xy in xss.Zip(yss)) {
            var x = CommaJoin(xy.First);
            var y = CommaJoin(xy.Second);
            Console.WriteLine($"Index: {i++}");
            Console.WriteLine($"  [{CommaJoin(x)}]");
            Console.WriteLine($"  [{CommaJoin(y)}]");
            Console.WriteLine();
        }
    }
}
