namespace P14_AI_Blog_Generator_Backend.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message)
        : base(message)
    {
    }
}