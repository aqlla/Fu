using System.Numerics;

namespace Fu.Math;



public interface IMathIdentities<TSelf> :
    IAdditiveIdentity<TSelf, TSelf>,
    IMultiplicativeIdentity<TSelf, TSelf>
    where TSelf :
    IMultiplicativeIdentity<TSelf, TSelf>,
    IAdditiveIdentity<TSelf, TSelf>;


public interface IStaticAdditive<in TSelf, in TOther, out TResult>
{
    static abstract TResult Add(TSelf l, TOther r);
    static abstract TResult Sub(TSelf l, TOther r);
}

public interface IStaticMultiplicative<in TSelf, in TOther, out TResult>
{
    static abstract TResult Mul(TSelf l, TOther r);
    static abstract TResult Div(TSelf l, TOther r);
}

public interface IStaticAdditive<T> : IStaticAdditive<T, T, T>;
public interface IStaticMultiplicative<T> : IStaticMultiplicative<T, T, T>;




public static partial class Math
{
 
    public static T Zero<T>() where T : IAdditiveIdentity<T, T> => T.AdditiveIdentity;
    public static T NegativeZero<T>() where T : IFloatingPointIeee754<T> => T.NegativeZero;
    public static T One<T>() where T : IMultiplicativeIdentity<T, T> => T.MultiplicativeIdentity;
    public static T NegativeOne<T>() where T: ISignedNumber<T> => T.NegativeOne;
    public static T PositiveInfinity<T>() where T: IFloatingPointIeee754<T> => T.PositiveInfinity;
    public static T NegativeInfinity<T>() where T: IFloatingPointIeee754<T> => T.NegativeInfinity;
    public static T NaN<T>() where T: IFloatingPointIeee754<T> => T.NaN;
    
    public static T Epsilon<T>() where T: IFloatingPointIeee754<T> => T.Epsilon;
    public static T E<T>() where T : IFloatingPointConstants<T> => T.E;
    public static T Pi<T>() where T : IFloatingPointConstants<T> => T.Pi;
    public static T Tau<T>() where T : IFloatingPointConstants<T> => T.Tau;
    public static T TwoPi<T>() where T : IFloatingPointConstants<T> => T.Pi + T.Pi;
    
    
    // Signs
    public static T Abs<T>(T value) where T : INumber<T> => T.Abs(value); 
    public static int Sign<T>(T value) where T : INumber<T> => T.Sign(value);
    public static T Negate<T>(T value) where T: IUnaryNegationOperators<T, T> => -value;

    
    // Checks
    public static bool IsZero<T>(T value) where T : INumberBase<T> => T.IsZero(value);
    public static bool IsPositive<T>(T value) where T : INumberBase<T> => T.IsPositive(value);
    public static bool IsNegative<T>(T value) where T : INumberBase<T> => T.IsNegative(value);
    public static bool IsPositiveInfinity<T>(T value) where T : INumberBase<T> => T.IsPositiveInfinity(value);
    public static bool IsNegativeInfinity<T>(T value) where T : INumberBase<T> => T.IsNegativeInfinity(value);
    public static bool IsNan<T>(T value) where T : INumberBase<T> => T.IsNaN(value);
    
    public static T Clamp<T>(T value, T min, T max) where T : INumber<T> => T.Clamp(value, min, max);

    
    // Comparisons
    public static T Min<T>(T x, T y) where T : INumber<T> => T.Min(x, y);
    public static T Max<T>(T x, T y) where T : INumber<T> => T.Max(x, y);
    
    public static T MinNumber<T>(T x, T y) where T : INumber<T> => T.MinNumber(x, y);
    public static T MaxNumber<T>(T x, T y) where T : INumber<T> => T.MaxNumber(x, y);
    
    public static T MinMagnitudeNumber<T>(T x, T y) where T : INumberBase<T> => T.MinMagnitudeNumber(x, y);
    public static T MaxMagnitudeNumber<T>(T x, T y) where T : INumberBase<T> => T.MaxMagnitudeNumber(x, y);

    
    // Rounding
    public static T Round<T>(T value) where T : IFloatingPoint<T> => T.Round(value);
    public static T Round<T>(T value, int digits) where T : IFloatingPoint<T> => T.Round(value, digits);
    public static T Floor<T>(T value) where T : IFloatingPoint<T> => T.Floor(value);
    public static T Ceiling<T>(T value) where T : IFloatingPoint<T> => T.Ceiling(value);
    
    // Arithmetic
    public static T Add<T>(T addend, T augend) where T : IAdditionOperators<T, T, T> => addend + augend;
    public static T Sub<T>(T minuend, T subtrahend) where T : ISubtractionOperators<T, T, T> => subtrahend - minuend;
    public static T Mul<T>(T multiplicand, T multiplier) where T : IMultiplyOperators<T, T, T> => multiplicand * multiplier;
    public static T Div<T>(T dividend, T divisor) where T : IDivisionOperators<T, T, T> => dividend / divisor;
    
    // Pow
    public static T Pow<T>(T @base, T exp) where T : IPowerFunctions<T> => T.Pow(@base, exp);
    
    // Roots
    public static T Sqrt<T>(T value) where T : IRootFunctions<T> => T.Sqrt(value);
    public static T Cbrt<T>(T value) where T : IRootFunctions<T> => T.Cbrt(value);
    public static T Hypot<T>(T x, T y) where T : IRootFunctions<T> => T.Hypot(x, y);
    public static T RootN<T>(T value, int n) where T : IRootFunctions<T> => T.RootN(value, n);
    
    // Logarithms
    public static T Log<T>(T value) where T : ILogarithmicFunctions<T> => T.Log(value);
    public static T Log2<T>(T value) where T : ILogarithmicFunctions<T> => T.Log2(value);
    public static T Log10<T>(T value) where T : ILogarithmicFunctions<T> => T.Log10(value);
    public static T LogP1<T>(T x) where T : ILogarithmicFunctions<T> => T.LogP1(x);
    public static T Log2P1<T>(T x) where T : ILogarithmicFunctions<T> => T.Log2P1(x);
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
    
    public static (T sin, T cos) SinCos<T>(T value) where T : ITrigonometricFunctions<T> => T.SinCos(value);
    public static (T sin, T cos) SinCosPi<T>(T value) where T : ITrigonometricFunctions<T> => T.SinCosPi(value);
    
    public static T DegToRad<T>(T degrees) where T : ITrigonometricFunctions<T> => 
        (T.Pi * degrees) / T.CreateChecked(180);
    
    public static T RadToDeg<T>(T radians) where T : ITrigonometricFunctions<T> => 
        (radians * T.CreateChecked(180)) / T.Pi;
    
    // Hyberbolics
    public static T Acosh<T>(T value) where T : IHyperbolicFunctions<T> => T.Acosh(value);
    public static T Asinh<T>(T value) where T : IHyperbolicFunctions<T> => T.Asinh(value);
    public static T Atanh<T>(T value) where T : IHyperbolicFunctions<T> => T.Atanh(value);
    public static T Cosh<T>(T value) where T : IHyperbolicFunctions<T> => T.Cosh(value);
    public static T Sinh<T>(T value) where T : IHyperbolicFunctions<T> => T.Sinh(value);
    public static T TAnh<T>(T value) where T : IHyperbolicFunctions<T> => T.Tanh(value);

    
    // Interpolation 
    public static T Lerp<T>(T from, T to, T weight) where T : IFloatingPointIeee754<T> 
        => T.Lerp(from, to, weight);
    

}