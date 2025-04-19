using Fu.Types.Errors;

namespace Fu.Results;


public static class ResultExt
{

    public static TData ThrowFailure<TData>(this IResult<TData> result) =>
        result switch {
            ISuccess<TData> { Data: var data } => data,
            IFailure<TData> { Error: var error } => throw new Exception(error.Message),
            _ => throw new InvalidDataException("Result is not a success or failure")
        };

    public static async Task<TData> ThrowFailure<TData>(this Task<IResult<TData>> result) => 
        ThrowFailure(await result);

    public static TOut Match<TIn, TOut>(
        this IResult<TIn> result, 
        Func<TIn, TOut> onSuccess, 
        Func<IError, TOut> onFailure)
        => result switch {
            ISuccess<TIn> { Data: var data }  => onSuccess(data),
            IFailure<TIn> { Error: var error } => onFailure(error),
            
            // No other types should exist, unfortunately we don't have
            // algebraic data types in C# yet. 
            _ => throw new InvalidDataException("Result is not a success or failure")
        };
    
    public static void MatchAction<TIn>(
        this IResult<TIn> result, 
        Action<TIn> onSuccess, 
        Action<IError> onFailure
    ) {
        switch (result) {
            case ISuccess<TIn> { Data: var data }:
                onSuccess(data);
                break;
            case IFailure<TIn> { Error: var error }:
                onFailure(error);
                break;
        }
    }

    public static TOut MatchValue<TIn, TOut>(this IResult<TIn> result, TOut onSuccess, TOut onFailure) 
        => result switch {
            ISuccess<TIn> => onSuccess,
            IFailure<TIn> => onFailure,

            // No other types should exist; unfortunately, we don't have
            // algebraic data types in C# yet. 
            _ => throw new InvalidDataException("Result is not a success or failure")
        };
    
    public static IResult<TOut> Map<TIn, TOut>(this IResult<TIn> result, Func<TIn, TOut> fn) 
        => result.Match(
            onSuccess: Result.From(fn),
            onFailure: Result.Fail<TOut>
        );
    
    public static IResult<TOut> FlatMap<TIn, TOut>(this IResult<TIn> result, Func<TIn, IResult<TOut>> fn) 
        => result.Match(onSuccess: fn, onFailure: Result.Fail<TOut>);
    
    
    public static IResult<TOut> FlatMap<TIn, TCtx, TOut>(
        this IResult<TIn> result, Func<TIn, TCtx, IResult<TOut>> fn, TCtx context)
        => result.FlatMap(d => fn(d, context));
}






public static class ResultDebugExt
{
    public static IResult<T> Peek<T>(this IResult<T> result, Action<T?, IError?> action)
    {
        result.MatchAction(
            onSuccess: d => action(d, null),
            onFailure: e => action(default, e)
        );
        
        return result;
    }
}