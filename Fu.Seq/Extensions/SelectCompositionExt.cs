using System;
using System.Collections.Generic;
using System.Linq;

namespace Fu.Seq.Extensions;


public static class SelectCompositionExt
{
	public static IEnumerable<TOut> Select<TSource, TR1, TOut>(
		this IEnumerable<TSource> source, Func<TSource, TR1> f, Func<TR1, TOut> g)
		=> source.Select(x => g(f(x)));

	public static IEnumerable<TOut> Select<TSource, TR1, TR2, TOut>(
		this IEnumerable<TSource> source,
		Func<TSource, TR1> f,
		Func<TR1, TR2> g,
		Func<TR2, TOut> h)
		=> source.Select(x => h(g(f(x))));

	public static IEnumerable<TOut> Select<TSource, TR1, TR2, TR3, TOut>(
		this IEnumerable<TSource> source,
		Func<TSource, TR1> f,
		Func<TR1, TR2> g,
		Func<TR2, TR3> h,
		Func<TR3, TOut> i)
		=> source.Select(x => i(h(g(f(x)))));


	public static void Each<TSource, TR1>(
		this IEnumerable<TSource> source, Func<TSource, TR1> f, Action<TR1> g)
		=> source.Each(x => g(f(x)));

	public static void Each<TSource, TR1, TR2>(
		this IEnumerable<TSource> source,
		Func<TSource, TR1> f,
		Func<TR1, TR2> g,
		Action<TR2> h)
		=> source.Each(x => h(g(f(x))));

	public static void Each<TSource, TR1, TR2, TR3>(
		this IEnumerable<TSource> source,
		Func<TSource, TR1> f,
		Func<TR1, TR2> g,
		Func<TR2, TR3> h,
		Action<TR3> i)
		=> source.Each(x => i(h(g(f(x)))));
}
