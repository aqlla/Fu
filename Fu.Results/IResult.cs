using Fu.Types.Errors;

namespace Fu.Results;

public interface IResult;

public interface IResult<out TData> : IResult;


public interface ISuccess<out TData> : IResult<TData>
{
    TData Data { get; }
}

public interface IFailure<out TData> : IResult<TData>
{
    IError Error { get; }
}