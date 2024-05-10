using System;

namespace Frends.AzureDataLake.DeleteContainer.Exceptions;

/// <summary>
/// Exception thrown when container to delete does not exist.
/// </summary>
public class ContainerNotFoundException : Exception
{
    /// <summary>
    /// Expeptions constructor
    /// </summary>
    public ContainerNotFoundException()
        : base("DeleteContainer error: Container not found.") { }
}
