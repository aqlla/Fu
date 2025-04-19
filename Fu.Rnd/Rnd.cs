using System.Numerics;

using Fu.Seq;
using Fu.Seq.Extensions;

namespace Fu.Rnd;

public static partial class Rnd
{
    private static readonly Random _random = new();
    
    
    // ** Random Values ** //
    
    // int
    public static int Int(int min, int max) => _random.Next(min, max);
    
    public static int Int(int max) => _random.Next(max);
    
    
    public static double Double(double min, double max) => 
        _random.NextDouble() * (max - min) + min;
    public static double Double(double max) => 
        _random.NextDouble() * max;
    
    
    public static float Float(float min, float max) => 
        (float) Double(min, max);
    public static float Float(float max) => 
        (float) Double(max);
    
    
    
    // String
    public static string String(int n, string chars) => 
        new string(Generate.Repeat(n, chars.EmitWith(Pick)).ToArray());

    
    public static string String(int n) => 
        String(n, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_");


    // ** Incrementors ** // 
    
    public static int Add(int value, int atMost, int atLeast = 0) =>
        Int(value + atLeast, value + atMost);

    

    /// <summary>
    /// Creates a function that calculates a new integer value within a specified range relative to an input value.
    /// </summary>
    /// <param name="atMost">
    /// The maximum value by which the input value can increase.
    /// </param>
    /// <param name="atLeast">
    /// The optional minimum value by which the input value can increase. Defaults to 0 if not specified.
    /// </param>
    /// <returns>
    /// A function that takes an integer input value and returns a new integer within the specified range.
    /// </returns>
    public static Func<int, int> Adder(int atMost, int atLeast = 0) =>
        value => Add(value, atMost, atLeast);



    // ** Stateful Generators ** //

    /// <summary>
    /// Creates a stateful function that generates an increasing sequence of integers
    /// starting from the specified initial value and incrementing within a defined range.
    /// </summary>
    /// <param name="maxStep">
    /// The maximum value by which the integer can increase at each step.
    /// </param>
    /// <param name="minStep">
    /// The optional minimum value by which the integer can increase at each step. Defaults to 0 if not specified.
    /// </param>
    /// <param name="start">
    /// The optional starting value of the sequence. Defaults to 0 if not specified.
    /// </param>
    /// <returns>
    /// A function that returns the next integer in the sequence when invoked,
    /// incrementing the value by a random amount within the specified range.
    /// </returns>
    public static Func<int> Ascending(int maxStep, int minStep, int start = 0) =>
        Generate.Stateful(start, Adder(atMost: maxStep, atLeast: minStep));
    

    
    // ** Random Selection ** //

    
    /// <summary>
    /// Selects a random element from an array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="xs">The array of elements.</param>
    /// <returns>A randomly selected element from the array.</returns>
    public static T Pick<T>(ReadOnlySpan<T> xs) => 
        xs[Int(xs.Length)];
    
}