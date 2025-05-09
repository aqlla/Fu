using System;
using System.Collections.Generic;
using System.Numerics;
using Fu.Math.Vec;

namespace Fu.Math;

public static class Plot
{
	public static IEnumerable<Vec3<T>> Range<T>(int n, Func<int, Vec3<T>> fn)
		where T : unmanaged, IFloatingPointIeee754<T>
	{
		for (var i = 0; i < n; i++)
			yield return fn(i);
	}


	// Series generated points

	public static Func<int, Vec3<T>> Spherical<T>(int n, T step, T? coverage = null)
		where T : unmanaged, IFloatingPointIeee754<T>
	{
		var twoPi = T.CreateChecked(Math.TwoPi);
		var tn = T.CreateChecked(n);

		return idx => {
			var i = T.CreateChecked(idx);

			var pct = i / (tn - T.One) * (coverage ?? T.One);
			var phi = Math.Acos(pct + pct - T.One);
			var theta = twoPi * i / step;

			return Vec.Vec.FromSpherical(phi, theta);
		};
	}


	public static Func<int, Vec3<T>> FibonacciSphere<T>(int n, T coverage)
		where T : unmanaged, IFloatingPointIeee754<T>
		=> Spherical(n, T.CreateChecked(1.618033988749895), coverage);



	public static Func<int, Vec2<T>> Spiroid<T>(int n, T step, T? distribution = null)
		where T : unmanaged, IFloatingPointIeee754<T>
	{
		var twoPi = T.CreateChecked(Math.TwoPi);
		var tn = T.CreateChecked(n);

		return i => {
			var ti = T.CreateChecked(i);
			var pct = ti / (tn - T.One);
			var distance = Math.Pow(pct, distribution ?? T.One);
			var angle = twoPi * step * ti;

			return Vec2.FromPolar(angle, distance);
		};
	}
}
