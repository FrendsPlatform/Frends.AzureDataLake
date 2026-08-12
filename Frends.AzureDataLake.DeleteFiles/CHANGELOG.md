# Changelog

## [1.3.0] - 2026-08-12
### Changed
- The task now targets .NET 8.
- Added a new `ErrorMessageOnFailure` option: you can now provide a custom error message that will be used when the task fails, instead of the default technical error message.
- The result object now exposes a `Success` property and an `Error` object (with `Message` and exception details) when the task fails and `ThrowErrorOnFailure` is false. The previous `IsSuccess` and `ErrorMessage` properties are still available for backwards compatibility.

## [1.2.0] - 2026-01-15
### Changed
- Updated Azure packages to latest versions:
- Azure.Storage.Files.DataLake 12.25.0
- Azure.Identity 1.17.1

## [1.1.0] - 2024-08-23
### Changed
- Updated the Azure.Identity package to version 1.12.0.

## [1.0.0] - 2024-05-21
### Added
- Initial implementation of Frends.AzureDataLake.DeleteFiles.
