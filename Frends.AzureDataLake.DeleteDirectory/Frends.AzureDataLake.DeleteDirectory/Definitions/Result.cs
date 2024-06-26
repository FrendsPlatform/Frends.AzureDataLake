﻿namespace Frends.AzureDataLake.DeleteDirectory.Definitions;

/// <summary>
/// Result class usually contains properties of the return object.
/// </summary>
public class Result
{
    internal Result(bool directoryWasDeleted, string message)
    {
        DirectoryWasDeleted = directoryWasDeleted;
        Message = message;
    }

    /// <summary>
    /// Returns true if directory has been deleted.
    /// </summary>
    /// <example>true</example>
    public bool DirectoryWasDeleted { get; private set; }

    /// <summary>
    /// Description about action's result.
    /// </summary>
    /// <example>Directory deleted successfully.</example>
    public string Message { get; private set; }
}
