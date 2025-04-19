using System;
using System.Collections.Generic;
using System.Linq;

namespace Fu.Seq;




public static class Generate
{

    public static IEnumerable<int> Range()
    {
        int counter = 0;
        while (true) yield return counter++;
    }
    
    public static IEnumerable<int> Range(int n, int start = 0) 
    {
        while (n-- > 0) yield return start++;
    }
    
    public static IEnumerable<TOut> Repeat<TOut>(int n, Func<TOut> fn)
    {
        while (n-- > 0) yield return fn();
    }
    

    public static Func<T> From<T>(IEnumerable<T> ts, Func<ReadOnlySpan<T>, T> selector)
    {
        var tsArray = ts.ToArray();
        return () => selector(tsArray.AsSpan());
    }
    
    
    /// <summary>
    /// Creates a stateful function that generates a sequence of values based
    /// on an initial state and a state transition function.
    /// </summary>
    /// <typeparam name="TState">The type of values in the sequence.</typeparam>
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
    public static Func<TState> Stateful<TState>(TState initial, Func<TState, TState> getNext) 
        => () => initial = getNext(initial);
}