using System;
using System.Collections.Generic;
using System.Linq;

namespace Fu.Seq.Extensions;


public static class SeqExt
{
    public static IEnumerable<int> Range(this int n, int start = 0) => 
        Generate.Range(start, n);


    public static IEnumerable<TOut> Repeat<TOut>(this int n, Func<TOut> fn) => 
        Generate.Repeat(n, fn);


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


    public static void Each<T>(this IEnumerable<T> source, Action<int, T> action)
    {
        var i = 0;
        foreach (var item in source) action(i++, item);
    }


    public static void Each<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var i = 0;
        foreach (var item in source) action(item, i++);
    }
}
