using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Fu.Math.Vec;

namespace Fu.Seq.Extensions;



public static class EnumerablePairsExt
{
    public static IEnumerable<(T fst, T snd)> Pairs<T>(this IEnumerable<T> source)
        => Seq.Pairwise(source, ValueTuple.Create);


    public static IEnumerable<TResult> Pairwise<T, TResult>(
        this IEnumerable<T> source, Func<T, T, TResult> fn)
        => Seq.Pairwise(source, fn);


    public static IEnumerable<(T fst, T snd)> OrderedPairs<T>(this IEnumerable<T> source)
        => OrderedPairwise(source.ToList(), ValueTuple.Create);


    public static IEnumerable<TResult> OrderedPairwise<T, TResult>(this IEnumerable<T> source, Func<T, T, TResult> fn)
        => OrderedPairwise(source.ToList(), fn);


    public static IEnumerable<TResult> OrderedPairwise<T, TResult>(this List<T> xs, Func<T, T, TResult> fn)
        => xs.SelectMany((a, i) => xs.Skip(i + 1).Select(b => fn(a, b)));



    public static IEnumerable<(int i, int j, T a, T b)> IndexedUniquePairs<T>(this List<T> list) =>
        list.SelectMany((a, i) => list.Skip(i + 1).Select((b, j) => (i, j + i + 1, a, b)));


    public static IEnumerable<(TParticle item, Vec3<T> vec)> UniquePairwise<TParticle, T>(
        this List<TParticle> particles, Func<TParticle, TParticle, Vec3<T>> fn
    ) where T : unmanaged, INumberBase<T>
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
