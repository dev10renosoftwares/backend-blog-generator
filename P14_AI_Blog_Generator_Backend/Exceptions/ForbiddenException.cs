namespace P14_AI_Blog_Generator_Backend.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message)
        : base(message)
    {
    }
}