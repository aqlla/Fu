namespace Fu.Types.Errors;


public interface IMessage {
    string Message { get; }
}



public interface IError : IMessage;

public interface IError<out TContext> : IError {
    TContext Context { get; }
}



public sealed record Error(string Message) : IError;


public sealed record ExceptionError<TException>(TException Context) 
    : IError<TException>
    where TException : Exception
{
    public string Message => Context.Message;
}