namespace TheLightingControllerLib.Connection;

public class ConnectionException : Exception
{
    public ConnectionException(string? message) : base(message)
    {
    }

    public ConnectionException(string? message, Exception inner) : base(message, inner)
    {
    }
}