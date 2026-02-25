using System;

namespace SharedKernel.Exceptions;

public class BadRequestException : System.Exception
{
    public BadRequestException(string message) : base(message) { }
    public BadRequestException(string message, string details) : base(message) { }

    public string? Details { get; }
}