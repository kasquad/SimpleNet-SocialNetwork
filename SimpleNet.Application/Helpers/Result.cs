using SimpleNet.Application.Notes.Queries.GetFriendsNotes;

namespace SimpleNet.Application.Helpers;

public class Result
{
    public bool IsSuccess { get; private set; }
    public string[] Errors { get; private set; }
    private Result() { }

    public static Result Success() => new Result() { IsSuccess = true};
    public static Result Failure(params string[] errors) => new Result() { IsSuccess = false, Errors = errors };
}
public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T Value { get; private set; }
    public string[] Errors { get; private set; }
    
    private Result() { }
    public static Result<T> Success(T value) => new Result<T>() { IsSuccess = true, Value = value };
    public static Result<T> Failure(params string[] errors) => new Result<T>() { IsSuccess = false, Errors = errors };
}

