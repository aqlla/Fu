using System.Numerics;

namespace Fu.Math;


public static class Math
{
    public const double Pi = 3.141592653589793115997963468544185161590576171875;
    public const double TwoPi = 2 * 3.141592653589793115997963468544185161590576171875;
    public const double Fib = 1.618033988749895;
    public const double Epsilon = 4.94065645841247E-324;


    public const float Pif = (float) Pi;
    public const float TwoPif = (float) TwoPi;
    public const float EpsilonF = 1.401298E-45f;

    public static class Const<T>
        where T: ISignedNumber<T>, IMinMaxValue<T>
    {
        
        public static T Zero => T.Zero;
        public static T One => T.One;
        public static T NegativeOne => T.NegativeOne;
     
        public static T MinValue => T.MinValue;
        public static T MaxValue => T.MaxValue;
    }
    
    
    public static class FpConst<T>
        where T: IFloatingPointIeee754<T>
    {
        public static T PositiveInfinity => T.PositiveInfinity;
        public static T NegativeInfinity => T.NegativeInfinity;
        public static T NaN => T.NaN;
    
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public static T Epsilon => T.Epsilon;
        public static T E => T.E;
        public static T Tau => T.Tau;

        // ReSharper disable once MemberHidesStaticFromOuterClass
        public static T Pi => T.Pi;
        public static T TwoPi => T.Pi + T.Pi;
        public static T NegativeZero => T.NegativeZero;

    }
    
    
    
    // Signs
    public static T Abs<T>(T value) where T : INumber<T> => T.Abs(value); 
    public static int Sign<T>(T value) where T : INumberBase<T> =>
        value switch {
            T when T.IsNegative(value) => -1,
            T when T.IsPositive(value) => 1,
            _ => 0,
        };
    public static T Negate<T>(T value) where T: IUnaryNegationOperators<T, T> => -value;

    
    // Checks
    public static bool IsZero<T>(T value) where T : INumberBase<T> => 
        T.IsZero(value);
    
    public static bool IsPositive<T>(T value) where T : INumberBase<T> => 
        T.IsPositive(value);
    
    public static bool IsNegative<T>(T value) where T : INumberBase<T> => 
        T.IsNegative(value);
    
    public static bool IsPositiveInfinity<T>(T value) where T : INumberBase<T> => 
        T.IsPositiveInfinity(value);
    
    public static bool IsNegativeInfinity<T>(T value) where T : INumberBase<T> => 
        T.IsNegativeInfinity(value);
    
    public static bool IsNan<T>(T value) where T : INumberBase<T> => 
        T.IsNaN(value);
    
    public static T Clamp<T>(T value, T min, T max) where T : INumber<T> => 
        T.Clamp(value, min, max);

    
    // Comparisons
    public static T Min<T>(T x, T y) where T : INumber<T> => T.Min(x, y);
    public static T Max<T>(T x, T y) where T : INumber<T> => T.Max(x, y);
    
    
    public static T MinNumber<T>(T x, T y) where T : INumber<T> => T.MinNumber(x, y);
    public static T MaxNumber<T>(T x, T y) where T : INumber<T> => T.MaxNumber(x, y);
    
    
    public static T MinMagnitudeNumber<T>(T x, T y) where T : INumberBase<T> => 
        T.MinMagnitudeNumber(x, y);
    
    public static T MaxMagnitudeNumber<T>(T x, T y) where T : INumberBase<T> => 
        T.MaxMagnitudeNumber(x, y);

    
    // Rounding
    public static T Round<T>(T value) where T : IFloatingPoint<T> => 
        T.Round(value);
    
    public static T Round<T>(T value, int digits) where T : IFloatingPoint<T> => 
        T.Round(value, digits);
    
    public static T Floor<T>(T value) where T : IFloatingPoint<T> => 
        T.Floor(value);
    
    public static T Ceiling<T>(T value) where T : IFloatingPoint<T> => 
        T.Ceiling(value);
    
    
    // Arithmetic
    public static T Add<T>(T addend, T augend) where T : IAdditionOperators<T, T, T> => 
        addend + augend;
    
    public static T Sub<T>(T minuend, T subtrahend) where T : ISubtractionOperators<T, T, T> => 
        subtrahend - minuend;
    
    public static T Mul<T>(T multiplicand, T multiplier) where T : IMultiplyOperators<T, T, T> => 
        multiplicand * multiplier;
    
    public static T Div<T>(T dividend, T divisor) where T : IDivisionOperators<T, T, T> => 
        dividend / divisor;
    
    
    // Pow
    public static T Pow<T>(T @base, T exp) where T : IPowerFunctions<T> => T.Pow(@base, exp);
    
    
    // Roots
    public static T Sqrt<T>(T value) where T : IRootFunctions<T> => T.Sqrt(value);
    public static T Cbrt<T>(T value) where T : IRootFunctions<T> => T.Cbrt(value);
    public static T Hypot<T>(T x, T y) where T : IRootFunctions<T> => T.Hypot(x, y);
    public static T RootN<T>(T value, int n) where T : IRootFunctions<T> => T.RootN(value, n);
    
    
    // Logarithms
    public static T Log<T>(T value) where T : ILogarithmicFunctions<T> => T.Log(value);
    public static T LogP1<T>(T x) where T : ILogarithmicFunctions<T> => T.LogP1(x);

    public static T Log2<T>(T value) where T : ILogarithmicFunctions<T> => T.Log2(value);
    public static T Log2P1<T>(T x) where T : ILogarithmicFunctions<T> => T.Log2P1(x);

    public static T Log10<T>(T value) where T : ILogarithmicFunctions<T> => T.Log10(value);
    public static T Log10P1<T>(T x) where T : ILogarithmicFunctions<T> => T.Log10P1(x);

    
    // Trigonometry
    public static T Atan2<T>(T y, T x) where T : IFloatingPointIeee754<T> => T.Atan2(y, x);
    public static T Atan2Pi<T>(T y, T x) where T : IFloatingPointIeee754<T> => T.Atan2Pi(y, x);

    public static T Acos<T>(T value) where T : ITrigonometricFunctions<T> => T.Acos(value);
    public static T AcosPi<T>(T value) where T : ITrigonometricFunctions<T> => T.AcosPi(value);
    
    public static T Asin<T>(T value) where T : ITrigonometricFunctions<T> => T.Asin(value);
    public static T AsinPi<T>(T value) where T : ITrigonometricFunctions<T> => T.AsinPi(value);
    
    public static T Atan<T>(T value) where T : ITrigonometricFunctions<T> => T.Atan(value);
    public static T AtanPi<T>(T value) where T : ITrigonometricFunctions<T> => T.AtanPi(value);
    
    public static T Cos<T>(T value) where T : ITrigonometricFunctions<T> => T.Cos(value);
    public static T CosPi<T>(T value) where T : ITrigonometricFunctions<T> => T.CosPi(value);
    
    public static T Sin<T>(T value) where T : ITrigonometricFunctions<T> => T.Sin(value);
    public static T SinPi<T>(T value) where T : ITrigonometricFunctions<T> => T.SinPi(value);
    
    public static T Tan<T>(T value) where T : ITrigonometricFunctions<T> => T.Tan(value);
    public static T TanPi<T>(T value) where T : ITrigonometricFunctions<T> => T.TanPi(value);
    
    public static (T sin, T cos) SinCos<T>(T value) 
        where T : ITrigonometricFunctions<T> => T.SinCos(value);
    
    public static (T sin, T cos) SinCosPi<T>(T value) 
        where T : ITrigonometricFunctions<T> => T.SinCosPi(value);
    
    public static T DegToRad<T>(T degrees) where T : ITrigonometricFunctions<T> => 
        (T.Pi * degrees) / T.CreateChecked(180);
    
    public static T RadToDeg<T>(T radians) where T : ITrigonometricFunctions<T> => 
        radians * T.CreateChecked(180) / T.Pi;
    
    
    // Hyberbolics
    public static T Acosh<T>(T value) where T : IHyperbolicFunctions<T> => T.Acosh(value);
    public static T Asinh<T>(T value) where T : IHyperbolicFunctions<T> => T.Asinh(value);
    public static T Atanh<T>(T value) where T : IHyperbolicFunctions<T> => T.Atanh(value);
    
    public static T Cosh<T>(T value) where T : IHyperbolicFunctions<T> => T.Cosh(value);
    public static T Sinh<T>(T value) where T : IHyperbolicFunctions<T> => T.Sinh(value);
    public static T Tanh<T>(T value) where T : IHyperbolicFunctions<T> => T.Tanh(value);

    
    // Interpolation 
    public static T Lerp<T>(T from, T to, T weight) where T : IFloatingPointIeee754<T> 
        => T.Lerp(from, to, weight);
}

public static class Cartesian
{
    public static (T x, T y) FromPolar<T>(T theta, T r)
        where T : struct, IFloatingPointIeee754<T>
    {
        var (sin, cos) = Math.SinCos(theta);
        return (
            x: r * cos,
            y: r * sin
        );
    }


    // 𝑥 = 𝑟 * sin 𝜃 * cos 𝜑
    // y = 𝑟 * sin 𝜃 * sin 𝜑
    // z = 𝑟 * cos 𝜃
    public static (T x, T y, T z) FromSpherical<T>(T phi, T theta)
        where T : struct, IFloatingPointIeee754<T>
    {
        var (tSin, tCos) = Math.SinCos(theta);
        var (pSin, pCos) = Math.SinCos(phi);

        return (
            x: pSin * tCos,
            y: pSin * tSin,
            z: pCos
        );
    }


    public static (T x, T y, T z) FromSpherical<T>(T phi, T theta, T r)
        where T : struct, IFloatingPointIeee754<T>
    {
        var (x, y, z) = FromSpherical(phi, theta);
        return (x * r, y * r, z * r);
    }


    // 𝑥 = 𝑟 cos 𝜃
    // y = 𝑟 sin 𝜃
    // z = 𝑧
    public static (T x, T y, T z) FromCylindrical<T>(T theta, T r, T z)
        where T : struct, IFloatingPointIeee754<T>
    {
        var (x, y) = FromPolar(theta, r);
        return (x, y, z);
    }
}
