namespace Fu.Seq;

public static partial class Seq 
{
    
    public static IEnumerable<int> Range(int n) => 
        Enumerable.Range(0, n);
    
    public static IEnumerable<int> Range(int n, int start) => 
        Enumerable.Range(start, n);
    

    public static IEnumerable<TOut> Repeat<TOut>(int n, Func<TOut> fn) => Range(n).Select(_ => fn());
    

    public static IEnumerable<TOut> Select<TOut>(int n, int start, Func<int, TOut> fn) => 
        Range(n, start).Select(fn);
    
    

}



public static class Generate
{
        
    /// <summary>
    /// Creates a stateful function that generates a sequence of values based
    /// on an initial state and a state transition function.
    /// </summary>
    /// <typeparam name="T">The type of values in the sequence.</typeparam>
    /// <param name="initial">
    /// The initial state or starting value of the sequence.
    /// </param>
    /// <param name="getNext">
    /// A function that defines the transition logic for generating the next
    /// value based on the current state.
    /// </param>
    /// <returns>
    /// A function that returns the next value in the sequence when invoked
    /// and updates the state accordingly.
    /// </returns>
    public static Func<T> Stateful<T>(T initial, Func<T, T> getNext)
    {
        var _state = initial;
        return () => _state = getNext(_state);
    }
        
    public static Func<T> Stateful<T>(Func<T, T> getNext) where T : struct => 
        Stateful(default, getNext);
}