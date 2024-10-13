namespace ConsoleApp1;

public class ApplicationInternalException(string? message = null, Exception? innerException = null)
    : ApplicationException(message: message ?? "Internal application exception", innerException: innerException);