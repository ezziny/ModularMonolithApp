using System;

namespace SharedKernel.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
    public NotFoundException(string entityType, object identifier)
    : base($"The {entityType} with identifier {identifier} wasn't found")
    {
        
    }
}
