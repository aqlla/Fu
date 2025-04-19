using Fu.Types;

namespace Fu.Results;



public interface ICanTry
{
    static abstract IResult<TOut> Try<TOut>(Func<TOut> fn);
}

public interface ICanTryArgs
{
    static abstract IResult<TOut> Try<TArgs, TOut>(Func<TArgs, TOut> fn, TArgs args);
}

public interface ICanTryCurry
{
    static abstract Func<TArgs, IResult<TOut>> Try<TArgs, TOut>(Func<TArgs, TOut> fn);
}


public interface ICanTryAction
{
    static abstract IResult<Unit> TryAction(Action fn);
}

public interface ICanTryActionArgs
{
    static abstract IResult<Unit> TryAction<TArgs>(Action<TArgs> fn, TArgs args);
}

public interface ICanTryActionCurry
{
    static abstract Func<TArgs, IResult<Unit>> TryAction<TArgs>(Action<TArgs> fn);
}

public interface ICanTryAsync
{
    static abstract Task<IResult<TOut>> TryAsync<TOut>(Func<Task<TOut>> fn);
}

public interface ICanTryArgsAsync
{
    static abstract Task<IResult<TOut>> TryAsync<TArgs, TOut>(Func<TArgs, Task<TOut>> fn, TArgs args);
}

public interface ICanTryCurryAsync
{
    static abstract Func<TArgs, Task<IResult<TOut>>> TryAsync<TArgs, TOut>(Func<TArgs, Task<TOut>> fn);
}
