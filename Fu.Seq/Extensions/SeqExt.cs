using System;
using System.Collections.Generic;
using System.Linq;

namespace Fu.Seq.Extensions;


public static class SeqExt
{
    public static IEnumerable<int> Range(this int n) =>
        Seq.Range(n);

    public static IEnumerable<int> Range(this int n, int start) =>
        Seq.Range(start, n);


    public static Func<T> EmitWith<T>(this IEnumerable<T> source, Func<ReadOnlySpan<T>, T> predicate)
    {
        var arr = source.ToArray();
        return () => predicate(arr.AsSpan());
    }
    
    
    public static IEnumerable<TResult> ZipWith<T1, T2, TResult>(
        this (IEnumerable<T1>, IEnumerable<T2>) source,
        Func<T1, T2, TResult> zipper)
    {
        var (first, second) = source;
        return first.Zip(second, zipper);
    }


    public static IEnumerable<(T1, T2)> Zip<T1, T2, TResult>(this (IEnumerable<T1>, IEnumerable<T2>) source)
    {
        var (first, second) = source;
        return first.Zip(second, static (a, b) => (a, b));
    }


    public static (List<T1>, List<T2>) Unzip<T1, T2>(this IEnumerable<(T1, T2)> source)
        => source.Aggregate(
            seed: (new List<T1>(), new List<T2>()), 
            func: static (acc, x) => ([..acc.Item1, x.Item1], [..acc.Item2, x.Item2])
        );



    public static IEnumerable<T> Unfold<T>(this IEnumerable<T> ts, int n, Func<T, IEnumerable<T>> fn)
    {
        while (n-- > 0) ts = ts.SelectMany(fn);
        return ts;
    }


    //
    // public static IEnumerable<T> Unfold<T>(this IEnumerable<T> ts, Func<T, IEnumerable<T>> fn)
    // {
    //     var tsl = ts.ToList();
    //     while (true) {
    //         tsl = tsl.SelectMany(fn).ToList();
    //         foreach (var t in tsl) yield return t;
    //     }
    // }

    /// <summary>
    /// Unfold where the output is just the next state.
    /// Stops when <paramref name="coalg"/> returns null.
    /// </summary>
    public static IEnumerable<TState> Unfold<TState>(
        TState seed,
        Func<TState, TState?> coalg
    )
    {
        var state = seed;
        while (true)
        {
            var next = coalg(state);
            if (next is null)
                yield break;
            yield return next;
            state = next;
        }
    }
    
    
    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source) =>
        source.SelectMany(static x => x);


    
    public static TOut Into<TSrc, TOut>(
        this IEnumerable<TSrc> source,
        Func<IEnumerable<TSrc>, TOut> fn)
        => fn(source);


    public static void Each<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var i in source) action(i);
    }


    public static void Each<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var i = 0;
        foreach (var item in source) action(item, i++);
    }
}
