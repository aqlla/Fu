using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Fu.Math.Vec;



public static class EnumerablePairsExt
{
    public static IEnumerable<(int i, int j, T a, T b)> UniquePairs<T>(this List<T> source) =>
        source.SelectMany(
            (a, i) => source.Skip(i + 1).Select(
                (b, j) => (i, j + i + 1, a, b)
            )
        );
    
    public static IEnumerable<(TParticle item, Vec3<T> vec)> UniquePairwise<TParticle, T>(
        this List<TParticle> particles, Func<TParticle, TParticle, Vec3<T>> fn
    ) where T : struct, INumberBase<T>
    {
        var pairs = particles.UniquePairs();
            
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