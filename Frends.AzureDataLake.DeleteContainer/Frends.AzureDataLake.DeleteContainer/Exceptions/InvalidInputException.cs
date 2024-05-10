using System;

namespace Frends.AzureDataLake.DeleteContainer.Exceptions;

/// <summary>
/// Exception thrown input is invalid.
/// </summary>
public class InvalidInputException : Exception
{
    /// <summary>
    /// Expeptions constructor
    /// </summary>
    public InvalidInputException(string message)
        : base(message) { }
}
