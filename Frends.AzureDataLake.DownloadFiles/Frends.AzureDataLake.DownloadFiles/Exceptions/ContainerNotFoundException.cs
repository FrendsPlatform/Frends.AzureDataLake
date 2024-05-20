using System;

namespace Frends.AzureDataLake.DownloadFiles.Exceptions;

/// <summary>
/// Exception thrown when container does not exist.
/// </summary>
public class ContainerNotFoundException : Exception
{
    /// <summary>
    /// Exceptions constructor
    /// </summary>
    public ContainerNotFoundException(string containerName)
        : base($"Container {containerName} not found.") { }
}
