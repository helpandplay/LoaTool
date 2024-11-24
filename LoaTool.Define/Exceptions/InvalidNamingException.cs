namespace LoaTool.Define.Exceptions;
public class InvalidNamingException : Exception
{
    public InvalidNamingException()
    {
    }

    public InvalidNamingException(string message)
        : base(message)
    {
    }

    public InvalidNamingException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
