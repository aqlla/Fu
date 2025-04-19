namespace Fu.Results;

public static class FailAccessories
{
    /// <inheritdoc cref="Results.Fail.If{TData}"/>
    public static IResult<TData> Fail<TData>(
        this TData value, Func<TData, bool> predicate, string message)
        => Results.Fail.If(predicate, message, value);
    
    /// <inheritdoc cref="Results.Fail.If{TData}"/>
    public static IResult<TData> FailIf<TData>(
        this TData value, Func<TData, bool> predicate, string message) 
        => Results.Fail.If(predicate, message, value);


    /// <inheritdoc cref="Results.Fail.IfWith{TData}"/>
    public static IResult<TData> FailIf<TData>(
        this TData value, Func<TData, bool> predicate, Func<TData, string> msgFactory)
        => Results.Fail.IfWith(predicate, msgFactory, value);
    


    /// <inheritdoc cref="Results.Fail.When{TData}"/>
    public static Task<IResult<TData>> FailWhen<TData>(
        this Task<TData> task, Func<TData, bool> predicate, string message)
        => Results.Fail.When(predicate, message, task);

    
    /// <inheritdoc cref="Results.Fail.WhenWith{TData}"/>
    public static Task<IResult<TData>> FailWhen<TData>(
        this Task<TData> task, Func<TData, bool> predicate, Func<TData, string> msgFactory)
        => Results.Fail.WhenWith(predicate, msgFactory, task);


    /// <summary>
    /// Check the value within an <see cref="ISuccess{TData}"/> for some
    /// condition, and <c>Fail</c> with the provided error message if that
    /// condition is met.
    /// </summary>
    /// <param name="result">Input to check.</param>
    /// <param name="predicate">
    /// A <see cref="Func{TData, Boolean}"/> to define the condition.
    /// </param>
    /// <param name="message">Error message to provide for a failure.</param>
    /// <typeparam name="TData">The type of data to check.</typeparam>
    /// <returns>
    /// If input is already an <see cref="IFailure{TData}"/>, return it as-is.<br /> 
    /// If input is as <see cref="ISuccess{TData}"/> and <c>predicate(result.Value)</c>
    /// evaluates to true, return a new <see cref="IFailure{TData}"/> with the
    /// provided error message.<br /> 
    /// Otherwise, return the successful input value as-is. 
    /// </returns>
    public static IResult<TData> OrFailIf<TData>(
        this IResult<TData> result, Func<TData, bool> predicate, string message)
        => result.FlatMap(Results.Fail.IfCurry(predicate, message));


    /// <inheritdoc cref="Results.Fail.When{TData}"/>
    public static async Task<IResult<TData>> OrFailWhen<TData>(
        this Task<IResult<TData>> task, Func<TData, bool> predicate, string message) 
        => OrFailIf(await task, predicate, message);
    
    
    
    /// <inheritdoc cref="Results.Fail.IfNull{TData}"/>
    public static IResult<TData> FailIfNull<TData>(
        this TData? value, string message) 
        => Results.Fail.IfNull(value, message);
    
    
    /// <inheritdoc cref="Results.Fail.WhenNull{TData}"/>
    public static Task<IResult<TData>> FailIfNull<TData>(
        this Task<TData?> task, string message) 
        => Results.Fail.WhenNull(task, message);
    
    
    /// <inheritdoc cref="Results.Fail.WhenNull{TData}"/>
    public static Task<IResult<TData>> FailIfNull<TData>(
        this Task<IResult<TData?>> task, string message) 
        => Results.Fail.OrWhenNull(task, message);
}