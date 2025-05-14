namespace TaskFlow.Domain.Exceptions
{
    public class NotFoundException(string message) : Exception(message) { }
    public class BadRequestException(string message) : Exception(message) { }
    public class UnauthorizeException(string message) : Exception(message) { }
    public class ForbiddenException(string message) : Exception(message) { }
    public class InternalErrorException(string message, Exception? inner) : Exception(message, inner) { }

    public class DomainRule(string message) : Exception(message)
    {
        public static void When(bool hasError, string message)
        {
            if (hasError)
            {
                throw new BadRequestException(message);
            }
        }
    }

}
