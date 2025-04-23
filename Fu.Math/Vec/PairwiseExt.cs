using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Fu.Math.Vec;



public static class EnumerablePairsExt
{
    public static IEnumerable<(T fst, T snd)> Pairwise<T>(this IEnumerable<T> source)
        => Pairwise(source, ValueTuple.Create);


    public static IEnumerable<TResult> Pairwise<T, TResult>(
        this IEnumerable<T> source, Func<T, T, TResult> fn
    ) {
        using var enumerator = source.GetEnumerator();
        if (!enumerator.MoveNext()) { yield break; }

        var fst = enumerator.Current;

        while (enumerator.MoveNext()) {
            var snd = enumerator.Current;
            yield return fn(fst, snd);
            fst = snd;
        }
    }

    public static List<TResult> Pairwise<T, TResult>(this ReadOnlySpan<T> span, Func<T, T, TResult> fn)
        where T : IEquatable<T>
    {
        var pairs = new List<TResult>();

        foreach (T fst in span) {
            foreach (T snd in span) {
                if (!fst.Equals(snd)) {
                    pairs.Add(fn(fst, snd));
                }
            }
        }

        return pairs;
    }


    public static IEnumerable<(T fst, T snd)> UniquePairs<T>(this IEnumerable<T> source)
        => UniquePairs(source.ToList(), ValueTuple.Create);


    public static List<(T fst, T snd)> UniquePairs<T>(this ReadOnlySpan<T> span)
        => UniquePairs(span, ValueTuple.Create);


    public static IEnumerable<TResult> UniquePairs<T, TResult>(this IEnumerable<T> source, Func<T, T, TResult> fn)
        => UniquePairs(source.ToList(), fn);


    public static IEnumerable<TResult> UniquePairs<T, TResult>(this List<T> lst, Func<T, T, TResult> fn)
        => lst.SelectMany((a, i) => lst.Skip(i + 1).Select(b => fn(a, b)));


    public static List<TResult> UniquePairs<T, TResult>(this ReadOnlySpan<T> span, Func<T, T, TResult> fn)
    {
        var pairs = new List<TResult>();

        for (var i = 0; i < span.Length; i++)
        {
            for (var j = i + 1; j < span.Length; j++)
            {
                pairs.Add(fn(span[i], span[j]));
            }
        }

        return pairs;
    }



    public static IEnumerable<(int i, int j, T a, T b)> IndexedUniquePairs<T>(this List<T> list) =>
        list.SelectMany((a, i) => list.Skip(i + 1).Select((b, j) => (i, j + i + 1, a, b)));


    public static IEnumerable<(TParticle item, Vec3<T> vec)> UniquePairwise<TParticle, T>(
        this List<TParticle> particles, Func<TParticle, TParticle, Vec3<T>> fn
    ) where T : struct, INumberBase<T>
    {
        var pairs = particles.IndexedUniquePairs();
            
        var applied = pairs.Select(pair => (pair.i, pair.j, f: fn(pair.a, pair.b)));
            
        var equalOpposites = applied.SelectMany(
            static triple => new[] { (triple.i, triple.f), (triple.j, -triple.f) }
        );

        var aggregated = equalOpposites.AggregateIndexed(particles.Count,
            static (acc, current) => acc + current
        );
            
        return aggregated.Zip(particles, static (force, particle) => (particle, force));
    }
    
    public static List<T> AggregateIndexed<T>(
        this IEnumerable<(int index, T value)> source,
        int count, Func<T, T, T> aggregator
    ) {
        var dest = new List<T>(new T[count]);
        
        foreach (var (index, value) in source) {
            dest[index] = aggregator(dest[index], value);
        }

        return dest;
    }
}
