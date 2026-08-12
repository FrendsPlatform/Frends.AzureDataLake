# Changelog

## [2.0.0] - 2026-08-12
### Changed
- [Breaking Change] The task now targets .NET 8.
- [Breaking Change] The `IsSuccess` property has been renamed to `Success` in the result object.
- [Breaking Change] The `ErrorMessage` string property has been removed from the result object. When the task fails and `ThrowErrorOnFailure` is false, error details are now returned in the `Error` object (use `Error.Message` for the error message).
- Added a new `ErrorMessageOnFailure` option: you can now provide a custom error message that will be used when the task fails, instead of the default technical error message.

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
