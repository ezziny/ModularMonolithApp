using System;

namespace SharedKernel.Exceptions;

public class InternalServerErrorException : System.Exception
{
    public InternalServerErrorException(string message) : base(message) { }
    public InternalServerErrorException(string message, string details) : base(message) { }
    
    public string? Details { get; }
}