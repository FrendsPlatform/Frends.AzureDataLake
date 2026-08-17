using System;
using System.Collections.Generic;
using Frends.AzureDataLake.UploadFiles.Definitions;

namespace Frends.AzureDataLake.UploadFiles.Helpers;

internal static class ErrorHandler
{
    internal static Result Handle(this Exception exception, Options options, bool throwCanceled = true)
    {
        ThrowIfCanceled(exception, throwCanceled);
        if (options.ThrowErrorOnFailure) ThrowBaseException(exception, options.ErrorMessageOnFailure);

        return ReturnResult(exception, options.ErrorMessageOnFailure);
    }

    private static void ThrowIfCanceled(Exception exception, bool throwCanceled = true)
    {
        if (throwCanceled && exception is OperationCanceledException) throw exception;
    }

    private static void ThrowBaseException(Exception exception, string customMessage = null)
    {
        if (string.IsNullOrEmpty(customMessage))
            throw new Exception(exception.Message, exception);

        throw new Exception(customMessage, exception);
    }

    private static Result ReturnResult(Exception exception, string customMessage = null)
    {
        var errorMessage = string.IsNullOrEmpty(customMessage)
            ? exception.Message
            : $"{customMessage}: {exception.Message}";

        return new Result(
            false,
            new Dictionary<string, string>(),
            errorMessage,
            new Error
            {
                Message = errorMessage,
                AdditionalInfo = exception,
            }
        );
    }
}
