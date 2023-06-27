namespace KdG.DI.exceptions;

public class TooManyContstructorsException:Exception
{
    public TooManyContstructorsException(string? message) : base(message)
    {
        
    }
}