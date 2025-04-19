namespace Fu.Fn;


public static class Fn
{


    /// <summary>
    /// Returns an adaptor that provides the given mapping function as the selector input to the pipeline.
    /// Often used to partially apply a mapping to an enumerable-like combinator.
    /// </summary>
    public static Func<Func<Func<TIn, TOut>, IEnumerable<TOut>>, Func<IEnumerable<TOut>>> 
        WithSelector<TIn, TOut>(Func<TIn, TOut> selector) => pipeline => () => pipeline(selector);
    

    public static Func<Func<Func<TIn, IEnumerable<TOut>>, IEnumerable<TOut>>, Func<IEnumerable<TOut>>> 
        WithSelector<TIn, TOut>(Func<TIn, IEnumerable<TOut>> selector) => pipeline => () => pipeline(selector);
    
    
    public static Func<Func<Func<TIn, TOut>, TIn, IEnumerable<TOut>>, Func<TIn, IEnumerable<TOut>>> 
        WithSelectorArgs<TIn, TOut>(Func<TIn, TOut> selector) => pipeline => args => pipeline(selector, args);
}



public static class GenericExt
{
    // Currying
    
    public static Func<TIn2, TOut> Curry<TIn1, TIn2, TOut>(
        this Func<TIn1, TIn2, TOut> fn, TIn1 arg1) 
        => arg2 => fn(arg1, arg2);
    
}