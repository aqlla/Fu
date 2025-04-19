using Fu.Types;
using Fu.Types.Errors;

namespace Fu.Results;


public struct Fail
{
    /// <summary>
    /// Check the value against some condition, and <c>Fail</c> with the
    /// provided error message if the condition is met.
    /// </summary>
    /// <param name="value">Input to check.</param>
    /// <param name="predicate">
    /// A <see cref="Func{TData, Boolean}"/> to define the condition.
    /// </param>
    /// <param name="message">Error message to provide for a failure.</param>
    /// <typeparam name="TData">The type of data to check.</typeparam>
    /// <returns>
    /// <see cref="IFailure{TData}"/> if <c>predicate(value)</c> evaluates to
    /// true. Otherwise, return <see cref="ISuccess{TData}"/> containing the
    /// input value.
    /// </returns>
    public static IResult<TData> If<TData>(Func<TData, bool> predicate, string message, TData value)
        => predicate(value) ? Result.Fail<TData>(message) : Result.From(value);
    
    /// <summary>
    /// Checks the value using a predicate and produces a failure result
    /// with a custom error message if the predicate condition is met.
    /// </summary>
    /// <param name="value">The value to evaluate against the predicate.</param>
    /// <param name="predicate">
    /// A function that evaluates the value and returns a boolean indicating
    /// if the failure condition is met.
    /// </param>
    /// <param name="msgFactory">
    /// A function that generates a custom error message based on the value.
    /// </param>
    /// <typeparam name="TData">The type of the value being evaluated.</typeparam>
    /// <returns>
    /// An instance of <see cref="IResult{TData}"/> representing a failure
    /// if the predicate condition is met, or success if the condition is not met.
    /// </returns>
    public static IResult<TData> IfWith<TData>(
        Func<TData, bool> predicate, Func<TData, string> msgFactory, TData value)
        => If(predicate, msgFactory(value), value);

    /// <inheritdoc cref="If{TData}"/>
    public static Func<TData, IResult<TData>> IfCurry<TData>(
        Func<TData, bool> predicate, string message)
        => value => If(predicate, message, value);
    
    
    /// <summary>
    /// Check if value is null and <c>Fail</c> with provided error message if
    /// so. Otherwise, proceed with non-nullable inner value. 
    /// </summary>
    /// <param name="value">a nullable to check</param>
    /// <param name="message">failure message</param>
    /// <typeparam name="TData">underlying data type</typeparam>
    /// <returns>
    /// If value is not null, <see cref="ISuccess{TData}"/> containing the
    /// non-nullable value is not null, otherwise a <see cref="IFailure{TData}"/>
    /// with the provided message. 
    /// </returns>
    public static IResult<TData> IfNull<TData>(TData? value, string message) 
        => value switch {
            null => Result.Fail<TData>(message),
            _    => Result.From(value)
        };


    public static IResult<TData> OrIfNull<TData>(IResult<TData?> result, string message)
        => result.FlatMap(IfNullCurry<TData>(message));


    /// <summary>
    /// Creates a curried function that checks if a nullable value is null and
    /// returns a failure with the provided error message if so. Otherwise,
    /// it returns a success containing the non-nullable inner value.
    /// </summary>
    /// <param name="message">The error message to return if the value is null.</param>
    /// <typeparam name="TData">The underlying data type of the nullable value.</typeparam>
    /// <returns>
    /// A function that takes a nullable value of type <typeparamref name="TData"/>,
    /// and returns an <see cref="IResult{TData}"/> indicating success or failure.
    /// </returns>
    public static Func<TData?, IResult<TData>> IfNullCurry<TData>(string message)
        => value => IfNull(value, message);


    /// <summary>
    /// Evaluates the result of an asynchronous operation and creates a failure result
    /// if the specified condition evaluates to true.
    /// </summary>
    /// <param name="task">A <see cref="Task{TData}"/>.</param>
    /// <param name="predicate">
    /// A function that defines the condition to check against the resolved value
    /// of the asynchronous operation.
    /// </param>
    /// <param name="message">
    /// The error message to include in the failure result if the condition is met.
    /// </param>
    /// <typeparam name="TData">The type returned from the Task.</typeparam>
    /// <returns>
    /// A <see cref="Task{IResult}"/>, where <c>IResult</c> is an
    /// <see cref="IFailure{TData}"/> if the failure condition is met. <br />
    /// Otherwise, <c>IResult</c> is <see cref="ISuccess{TData}"/> containing the
    /// awaited value.
    /// </returns>
    public static async Task<IResult<TData>> When<TData>(
        Func<TData, bool> predicate, string message, Task<TData> task)
        => If(predicate, message, await task);
    
    
    /// <summary>
    /// Evaluates the result of an asynchronous operation and fails with a provided custom error message if a specified condition is met.
    /// </summary>
    /// <param name="task">A task representing the asynchronous operation that produces a result.</param>
    /// <param name="predicate">A function that determines whether the condition is met for the result of the task.</param>
    /// <param name="msgFactory">A function that generates the error message based on the result of the task.</param>
    /// <typeparam name="TData">The type of the result produced by the task.</typeparam>
    /// <returns>
    /// A task representing the asynchronous operation that produces an <see cref="IResult{TData}"/>. The result is a failure if the condition specified by the predicate is met, otherwise it is a success containing the result of the task.
    /// </returns>
    public static async Task<IResult<TData>> WhenWith<TData>(
        Func<TData, bool> predicate, Func<TData, string> msgFactory, Task<TData> task)
        => IfWith(predicate, msgFactory, await task);
     
    /// <summary>
    /// Check if a task results in a null value and <c>Fail</c> with provided
    /// error message if so. Otherwise, proceed with non-nullable inner value. 
    /// </summary>
    /// <param name="task">A task with a possible null value to check.</param>
    /// <param name="message">failure message</param>
    /// <typeparam name="TData">underlying data type</typeparam>
    /// <returns>
    /// If value is not null, <see cref="ISuccess{TData}"/> containing the
    /// non-nullable value is not null, otherwise a <see cref="IFailure{TData}"/>
    /// with the provided message. 
    /// </returns>
    public static async Task<IResult<TData>> WhenNull<TData>(Task<TData?> task, string message) 
        => IfNull(await task, message);
    
    
    public static async Task<IResult<TData>> OrWhenNull<TData>(
        Task<IResult<TData?>> task, string message) 
        => OrIfNull(await task, message);
}

public struct Result :
    ICanTry, ICanTryArgs, ICanTryAsync,
    ICanTryCurry, ICanTryCurryAsync,
    ICanTryAction, ICanTryActionArgs, 
    ICanTryActionCurry
{
    /// <summary>
    /// Constructs a successful result with no value (Unit).
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IResult{Unit}"/> representing a successful
    /// operation without a result value.
    /// </returns>
    public static IResult<Unit> Ok(Unit _) => new Success<Unit>(Unit.Instance);

    /// <inheritdoc cref="Result.Ok(Unit)" />
    public static IResult<Unit> Ok() => Ok(Unit.Instance);
    
    
    /// <summary>
    /// Constructs a successful result containing the provided value.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of the data contained in the result.
    /// </typeparam>
    /// <param name="value">The value to wrap in a result.</param>
    /// <returns>
    /// An instance of <see cref="IResult{TData}"/> representing a successful
    /// operation containing the provided value.
    /// </returns>
    public static IResult<TData> From<TData>(TData value) =>
        new Success<TData>(value);


    /// <summary>
    /// Creates a curry function that wraps the provided function, returning a result
    /// that encapsulates the output value.
    /// </summary>
    /// <param name="fn">A function that takes an input of type <typeparamref name="TIn"/>
    /// and returns a value of type <typeparamref name="TOut"/>.</param>
    /// <typeparam name="TIn">The type of the input parameter of the function.</typeparam>
    /// <typeparam name="TOut">The type of the return value of the function.</typeparam>
    /// <returns>
    /// A currying function that takes an input of type <typeparamref name="TIn"/> and
    /// returns an <see cref="IResult{TOut}"/> containing either the success value
    /// or failure details.
    /// </returns>
    public static Func<TIn, IResult<TOut>> From<TIn, TOut>(Func<TIn, TOut> fn) =>
        value => From(fn(value));


    /// <summary>
    /// Constructs a failure result using the specified error message.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of the data expected in the result.
    /// </typeparam>
    /// <param name="message">Message detailing the reason for failure.</param>
    /// <returns>
    /// An instance of <see cref="IResult{TData}"/> representing a failed
    /// operation with the provided error details.
    /// </returns>
    public static IResult<TData> Fail<TData>(string message) =>
        Fail<TData>(new Error(message));

    
    /// <summary>
    /// Constructs a failure result using the specified error.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of the data expected in the result.
    /// </typeparam>
    /// <param name="error">The error detailing the reason for failure.</param>
    /// <returns>
    /// An instance of <see cref="IResult{TData}"/> representing a failed operation
    /// with the provided error details.
    /// </returns>
    public static IResult<TData> Fail<TData>(IError error) =>
        new Failure<TData>(error);


    /// <summary>
    /// Creates a failure result from the provided exception.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of the data that would have been contained in the result.
    /// </typeparam>
    /// <param name="ex">The exception to encapsulate in the result.</param>
    /// <returns>
    /// An instance of <see cref="IResult{TData}"/> representing a failed operation
    /// encapsulating the provided exception as its error.
    /// </returns>
    public static IResult<TData> Fail<TData>(Exception ex) => 
        new Failure<TData>(new ExceptionError<Exception>(ex));


    /// <summary>
    /// Asynchronously fails with the specified error and returns a failed result.
    /// </summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <typeparam name="TData">The type of the data expected in the result.</typeparam>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains
    /// an <see cref="IResult{TData}"/> indicating the failure with the provided error.
    /// </returns>
    public static Task<IResult<TData>> FailAsync<TData>(Error error) => 
        Task.FromResult(Fail<TData>(error));


    #region Curried

    public static Func<IResult<TIn>, TOut> 
        MatchWith<TIn, TOut>(Func<TIn, TOut> onSuccess, Func<IError, TOut> onFailure) 
        => result => result.Match(onSuccess, onFailure);
    
    
    public static Action<IResult<TIn>> 
        MatchWithAction<TIn>(Action<TIn> onSuccess, Action<IError> onFailure) 
        => result => result.MatchAction(onSuccess, onFailure);
    
    
    public static Func<IResult<TIn>, IResult<TOut>> 
        MapWith<TIn, TOut>(Func<TIn, TOut> fn) => result => result.Map(fn);
    
    
    public static Func<IResult<TIn>, TCtx, IResult<TOut>> 
        MapWithContext<TIn, TCtx, TOut>(Func<TIn, TCtx, TOut> fn) 
        => (result, context) => result.Map(data => fn(data, context));

    
    public static Func<IResult<TIn>, IResult<TOut>> 
        FlatMapWith<TIn, TOut>(Func<TIn, IResult<TOut>> fn) => result => result.FlatMap(fn);
    
    
    public static Func<IResult<TIn>, TCtx, IResult<TOut>> 
        FlatMapWithContext<TIn, TCtx, TOut>(Func<TIn, TCtx, IResult<TOut>> fn) 
        => (result, context) => result.FlatMap(data => fn(data, context));

    #endregion
    
    
    #region Try

    public static IResult<TOut> Try<TOut>(Func<TOut> fn)
    {
        try {
            return From(fn());
        }
        catch (Exception e) {
            return Fail<TOut>(e);
        }
    }

    public static IResult<TOut> Try<TArgs, TOut>(Func<TArgs, TOut> fn, TArgs args)
    {
        try {
            return From(fn(args));
        }
        catch (Exception e) {
            return Fail<TOut>(e);
        }
    }

    public static async Task<IResult<TOut>> TryAsync<TArgs, TOut>(Func<TArgs, Task<TOut>> fn, TArgs args)
    {
        try {
            return From(await fn(args));
        }
        catch (Exception e) {
            return Fail<TOut>(e);
        }
    }
    
    public static async Task<IResult<Unit>> TryAsync<TArgs>(Func<TArgs, Task> fn, TArgs args)
    {
        try {
            await fn(args);
            return From(Unit.Instance);
        }
        catch (Exception e) {
            return Fail<Unit>(e);
        }
    }
    

    public static async Task<IResult<TOut>> TryAsync<TOut>(Func<Task<TOut>> fn)
    {
        try {
            return From(await fn());
        }
        catch (Exception e) {
            return Fail<TOut>(e);
        }
    }
    

    public static async ValueTask<IResult<TOut>> TryAsync<TOut>(Func<ValueTask<TOut>> fn)
    {
        try {
            return From(await fn());
        }
        catch (Exception e) {
            return Fail<TOut>(e);
        }
    }
    

    public static async Task<IResult<TOut>> TryTask<TOut>(Task<TOut> task)
    {
        try {
            return From(await task);
        }
        catch (Exception e) {
            return Fail<TOut>(e);
        }
    }


    public static Func<TArgs, IResult<TOut>> Try<TArgs, TOut>(Func<TArgs, TOut> fn) =>
        args => {
            try {
                return From(fn(args));
            }
            catch (Exception e) {
                return Fail<TOut>(e);
            }
        };

    public static Func<TArgs, Task<IResult<TOut>>> TryAsync<TArgs, TOut>(Func<TArgs, Task<TOut>> fn) =>
        async args => {
            try {
                return From(await fn(args));
            }
            catch (Exception e) {
                return Fail<TOut>(e);
            }
        };


    public static IResult<Unit> TryAction(Action fn)
    {
        try {
            fn();
            return From(Unit.Instance);
        }
        catch (Exception e) {
            return Fail<Unit>(e);
        }
    }

    public static IResult<Unit> TryAction<TArgs>(Action<TArgs> fn, TArgs args)
    {
        try {
            fn(args);
            return From(Unit.Instance);
        }
        catch (Exception e) {
            return Fail<Unit>(e);
        }
    }

    public static Func<TArgs, IResult<Unit>> TryAction<TArgs>(Action<TArgs> fn) =>
        args => {
            try {
                fn(args);
                return From(Unit.Instance);
            }
            catch (Exception e) {
                return Fail<Unit>(e);
            }
        };

    #endregion
}
