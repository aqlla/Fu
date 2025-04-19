using Fu.Types.Errors;

namespace Fu.Results;


public sealed record Success<TData>(TData Data) : ISuccess<TData>;


public sealed record Failure<TData>(IError Error) : IFailure<TData>;


    
