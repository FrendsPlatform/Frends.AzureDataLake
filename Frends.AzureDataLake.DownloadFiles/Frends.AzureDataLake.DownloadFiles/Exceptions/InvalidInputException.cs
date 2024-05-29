using System;

namespace Frends.AzureDataLake.DownloadFiles.Exceptions;

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
