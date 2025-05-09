using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Fu.Seq;


public static class As
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T[] Array<T>(IEnumerable<T> source)
		=> source.ToArray();


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Span<T> Span<T>(IEnumerable<T> source)
		=> source.ToArray();
}


public static class Seq
{
	public static IEnumerable<T> Of<T>(params ReadOnlySpan<T> ts) =>
		ts.ToArray();


	public static IEnumerable<int> Range()
	{
		var counter = 0;
		while (counter < int.MaxValue) yield return counter++;
	}

	public static IEnumerable<int> Range(int start, int n)
	{
		var counter = start;
		while (n-- > start - 1) yield return counter++;
	}

	public static IEnumerable<int> Range(int n)
	{
		var counter = 0;
		while (n-- > 0) yield return counter++;
	}

	public static IEnumerable<TOut> Repeat<TOut>(this int n, Func<TOut> fn)
	{
		while (n-- > 0) yield return fn();
	}


	public static Func<T, IEnumerable<T>> Repeat<T>(int n) => val => n.Repeat(val);

	public static IEnumerable<T> Repeat<T>(this int n, T val)
	{
		while (n-- > 0) yield return val;
	}


	public static IEnumerable<TOut> Splat<T1, T2, TOut>(
		this IEnumerable<(T1 a, T2 b)> source, Func<T1, T2, TOut> fn)
		=> source.Select(x => fn(x.a, x.b));

	public static IEnumerable<TOut> Splat<T1, T2, T3, TOut>(
		this IEnumerable<(T1 a, T2 b, T3 c)> source, Func<T1, T2, T3, TOut> fn)
		=> source.Select(x => fn(x.a, x.b, x.c));

	public static IEnumerable<TOut> Splat<T1, T2, T3, T4, TOut>(
		this IEnumerable<(T1 a, T2 b, T3 c, T4 d)> source, Func<T1, T2, T3, T4, TOut> fn)
		=> source.Select(x => fn(x.a, x.b, x.c, x.d));

	public static IEnumerable<(T fst, T snd)> Pairs<T>(IEnumerable<T> source)
		=> Pairwise(source, ValueTuple.Create);


	public static IEnumerable<TResult> Pairwise<T, TResult>(
		IEnumerable<T> source, Func<T, T, TResult> fn
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

	public static List<TResult> Pairwise<T, TResult>(ReadOnlySpan<T> span, Func<T, T, TResult> fn)
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


	public static IEnumerable<(T fst, T snd)> BOrderedPairs<T>(IEnumerable<T> source)
		=> BOrderedPairwise(source.ToArray(), ValueTuple.Create);


	public static List<(T fst, T snd)> BOrderedPairs<T>(T[] ts)
		=> BOrderedPairs<T>(ts.AsSpan());

	public static List<(T fst, T snd)> BOrderedPairs<T>(ReadOnlySpan<T> span)
		=> BOrderedPairwise(span, ValueTuple.Create);


	public static IEnumerable<TResult> BOrderedPairwise<T, TResult>(IEnumerable<T> source, Func<T, T, TResult> fn)
		=> BOrderedPairwise(source.ToList(), fn);


	public static Func<IEnumerable<T>, IEnumerable<TResult>> BOrderedPairwise<T, TResult>(Func<T, T, TResult> fn)
		=> source => BOrderedPairwise(source.ToList(), fn);


	public static IEnumerable<TResult> BOrderedPairwise<T, TResult>(List<T> xs, Func<T, T, TResult> fn)
		=> xs.SelectMany((a, i) => xs.Skip(i + 1).Select(b => fn(a, b)));



	public static List<TResult> BOrderedPairwise<T, TResult>(T[] ts, Func<T, T, TResult> fn) =>
		BOrderedPairwise(ts.AsSpan(), fn);


	public static List<TResult> BOrderedPairwise<T, TResult>(ReadOnlySpan<T> span, Func<T, T, TResult> fn)
	{
		var pairs = new List<TResult>();

		for (var i = 0; i < span.Length; i++) {
			for (var j = i + 1; j < span.Length; j++) {
				pairs.Add(fn(span[i], span[j]));
			}
		}

		return pairs;
	}




	public static IEnumerable<(T fst, T snd)> OrderedPairs<T>(this IEnumerable<T> source)
		=> source.ToArray().OrderedPairs();

	public static IEnumerable<(T fst, T snd)> OrderedPairs<T>(this T[] xs)
		=> xs.OrderedPairwise(ValueTuple.Create);




	public static IEnumerable<TOut> OrderedPairs<T, TOut>(this IEnumerable<T> source, Func<T, T, TOut> fn)
		=> source.ToArray().OrderedPairwise(fn);

	public static IEnumerable<TOut> OrderedPairwise<T, TOut>(this T[] xs, Func<T, T, TOut> fn)
	{
		var len = xs.Length;
		if (len < 2) yield break;

		// walk forward…
		for (var i = 0; i < len - 1; i++)
			yield return fn(xs[i], xs[i + 1]);

		yield return fn(xs[^1], xs[0]);
	}


/// <summary>
    /// All k-length combinations of the source (order doesn't matter).
    /// </summary>
    public static IEnumerable<T[]> Combinations<T>(
        this IEnumerable<T> source,
        int k
    )
{
        var xs = source.ToArray();
        return k switch {
            <= 0 => Array.Empty<T>().Yield(),
            _ when k > xs.Length => [],
            _ => Comb(0, k).Select(idxArr => idxArr.Select(i => xs[i]).ToArray())
        };

        IEnumerable<int[]> Comb(int start, int count) {
            if (count == 0) {
                yield return [];
            }
            else {
                for (var i = start; i <= xs.Length - count; i++) {
					foreach (var tail in Comb(i + 1, count - 1)) {
						yield return new[] { i }.Concat(tail).ToArray();
					}
				}
            }
        }
    }


	/// <summary>
    /// All k-length permutations of the source (order DOES matter).
    /// </summary>
    public static IEnumerable<T[]> Permutations<T>(
        this IEnumerable<T> source, int k
    ) {
        var xs = source.ToArray();
        if (k <= 0 || k > xs.Length) yield break;
        foreach (var combo in xs.Combinations(k))
        {
            // generate all orderings of this combination
            foreach (var perm in Permute(combo, 0, combo.Length - 1))
                yield return perm;
        }
    }

    // Heap's algorithm for in-place permutations
    private static IEnumerable<T[]> Permute<T>(T[] arr, int l, int r)
    {
        if (l == r) {
            yield return arr.ToArray();
        } else {
            for (var i = l; i <= r; i++) {
                Swap(arr, l, i);
                foreach (var _ in Permute(arr, l + 1, r))
                    yield return _;
                Swap(arr, l, i);
            }
        }
    }

    private static void Swap<T>(T[] a, int i, int j)
        => (a[i], a[j]) = (a[j], a[i]);

    // helper to wrap a single element into an enumerable
    private static IEnumerable<T> Yield<T>(this T item)
    {
        yield return item;
    }



	public static IEnumerable<TResult> CircularPairwise<T, TResult>(this IEnumerable<T> src, Func<T, T, TResult> fn)
	{
		var arr = src.ToArray();
		if (arr.Length == 0) yield break;

		for (var i = 0; i < arr.Length; i++) {
			yield return fn(arr[i], arr[(i + 1) % arr.Length]);
		}
	}

	public static IEnumerable<(T fst, T snd)> CircularPairs<T>(this IEnumerable<T> src)
		=> src.CircularPairwise(ValueTuple.Create);



	public static Dictionary<TKey, List<TVal>> AggregateByMany<TIn, TKey, TVal>(
		this IEnumerable<TIn> source,
		Func<TIn, IEnumerable<TKey>> key,
		Func<TIn, TVal> val
		) where TKey : notnull
	{
		Dictionary<TKey, List<TVal>> acc = [];
		foreach (var item in source) {
			var v = val(item);
			foreach (var k in key(item)) {
				if (!acc.TryGetValue(k, out var list))
					acc[k] = list = [];
				list.Add(v);
			}
		}

		return acc;
	}
}




public static class Select
{

	public static Func<IEnumerable<int>, IEnumerable<T>> From<T>(IReadOnlyList<T> source) =>
		idxs => idxs.Select(source.ElementAt);

	public static Func<TIn, TOut[]> AsArrays<TIn, TOut>(this Func<TIn, IEnumerable<TOut>> fn) =>
		input => fn(input).ToArray();

	public static Func<TIn, List<TOut>> AsLists<TIn, TOut>(this Func<TIn, IEnumerable<TOut>> fn) =>
		input => fn(input).ToList();


	public static Func<TIn[], TOut> WithArrays<TIn, TOut>(this Func<ReadOnlySpan<TIn>, TOut> fn) =>
		input => fn(input.AsSpan());
}
