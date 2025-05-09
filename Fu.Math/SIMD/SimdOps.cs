using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fu.Math.SIMD;

// [StructLayout(LayoutKind.Sequential)]
// public readonly struct Simd3
// {
// 	public readonly float[] X;
// 	public readonly float[] Y;
// 	public readonly float[] Z;
//
// 	public readonly int Length;
//
// 	public int PaddedLength => X?.Length ?? 0;
//
// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 	public Simd3(int length)
// 	{
// 		Length = length;
// 		var padded = GetPaddedLength(length);
//
// 		X = new float[padded];
// 		Y = new float[padded];
// 		Z = new float[padded];
// 	}
//
// 	public void Load(IEnumerable<(float x, float y, float z)> src)
// 	{
// 		var s = src.ToList();
// 		for (var i = 0; i < Length; i++) {
// 			var (x, y, z) = s[i];
// 			X[i] = x;
// 			Y[i] = y;
// 			Z[i] = z;
// 		}
// 	}
//
//
// 	[MethodImpl(MethodImplOptions.AggressiveInlining)]
// 	public (Vector<float> xs, Vector<float> ys, Vector<float> zs) At(int i)
// 		=> (new (X, i), new (Y, i), new (Z, i));
//
// 	private static int GetPaddedLength(int count) =>
// 		(count + Vector<float>.Count - 1) & ~(Vector<float>.Count - 1);
// }


public static class Simd3VecOps
{
	private static int Width => Vector<float>.Count;

	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Neg(Simd3 a, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		Vector.Negate(ax).CopyTo(result.X, i);
	// 		Vector.Negate(ay).CopyTo(result.Y, i);
	// 		Vector.Negate(az).CopyTo(result.Z, i);
	// 	}
	// }
	//
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Add(Simd3 a, Simd3 b, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		var (bx, by, bz) = b.At(i);
	//
	// 		(ax + bx).CopyTo(result.X, i);
	// 		(ay + by).CopyTo(result.Y, i);
	// 		(az + bz).CopyTo(result.Z, i);
	// 	}
	// }
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Sub(Simd3 a, Simd3 b, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		var (bx, by, bz) = b.At(i);
	//
	// 		(ax + bx).CopyTo(result.X, i);
	// 		(ay + by).CopyTo(result.Y, i);
	// 		(az + bz).CopyTo(result.Z, i);
	// 	}
	// }
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Mul(Simd3 a, Simd3 b, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		var (bx, by, bz) = b.At(i);
	//
	// 		(ax * bx).CopyTo(result.X, i);
	// 		(ay * by).CopyTo(result.Y, i);
	// 		(az * bz).CopyTo(result.Z, i);
	// 	}
	// }
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Mul(Simd3 a, float b, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		(ax * b).CopyTo(result.X, i);
	// 		(ay * b).CopyTo(result.Y, i);
	// 		(az * b).CopyTo(result.Z, i);
	// 	}
	// }
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Div(Simd3 a, Simd3 b, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		var (bx, by, bz) = b.At(i);
	//
	// 		(ax / bx).CopyTo(result.X, i);
	// 		(ay / by).CopyTo(result.Y, i);
	// 		(az / bz).CopyTo(result.Z, i);
	// 	}
	// }
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Div(Simd3 a, float b, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		(ax / b).CopyTo(result.X, i);
	// 		(ay / b).CopyTo(result.Y, i);
	// 		(az / b).CopyTo(result.Z, i);
	// 	}
	// }
	//
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static float Dot(Simd3 a, Simd3 b)
	// {
	// 	var acc = Vector<float>.Zero;
	// 	var n = a.PaddedLength;
	//
	// 	for (var i = 0; i < n; i += Vector<float>.Count)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		var (bx, by, bz) = b.At(i);
	// 		acc += ax * bx + ay * by + az * bz;
	// 	}
	//
	// 	var sum = 0f;
	// 	for (var i = 0; i < Vector<float>.Count; i++)
	// 		sum += acc[i];
	//
	// 	return sum;
	// }
	//
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Normal(Simd3 a, Simd3 result)
	// {
	// 	var n    = a.PaddedLength;
	// 	var eps  = new Vector<float>(1e-8f);
	// 	// var ones = Vector<float>.One;
	//
	//
	// 	for (var i = 0; i < n; i += Vector<float>.Count)
	// 	{
	// 		var (vx, vy, vz) = a.At(i);
	// 		var lenSq = vx * vx + vy * vy + vz * vz + eps;
	// 		var len = Vector.SquareRoot(lenSq);
	//
	// 		// multiply each component by invLen
	// 		(vx / len).CopyTo(result.X, i);
	// 		(vy / len).CopyTo(result.Y, i);
	// 		(vz / len).CopyTo(result.Z, i);
	// 	}
	// }
	//
	//
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static void Sqrt(Simd3 a, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		Vector.SquareRoot(ax).CopyTo(result.X, i);
	// 		Vector.SquareRoot(ay).CopyTo(result.Y, i);
	// 		Vector.SquareRoot(az).CopyTo(result.Z, i);
	// 	}
	// }


	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static float LengthSquared(Simd3 vec) => Dot(vec, vec);
	//
	//
	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static float Length(Simd3 a) =>
	// 	float.Sqrt(LengthSquared(a));
	//
	//
	// public static void Scale(Simd3 a, float scalar, Simd3 result)
	// {
	// 	for (var i = 0; i <= a.PaddedLength - Width; i += Width)
	// 	{
	// 		var (ax, ay, az) = a.At(i);
	// 		Vector.Multiply(ax, scalar).CopyTo(result.X, i);
	// 		Vector.Multiply(ay, scalar).CopyTo(result.Y, i);
	// 		Vector.Multiply(az, scalar).CopyTo(result.Z, i);
	// 	}
	// }
}
