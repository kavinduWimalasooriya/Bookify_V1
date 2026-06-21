using Bookify.Application.Abstractions.Behaviors;

namespace Bookify.Application.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; }
    public  ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }
    
}