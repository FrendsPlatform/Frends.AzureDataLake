# Changelog

## [1.4.0] - 2026-08-07

### Added

- Added an `Options` parameter with `ThrowErrorOnFailure` and `ErrorMessageOnFailure` settings to control error handling behavior. When `ThrowErrorOnFailure` is set to `false`, the task returns a result with `Success = false` and error details instead of throwing an exception.
- The result now includes `Success` and `Error` properties for easier error checking.

### Changed

- Upgraded target framework from .NET 6 to .NET 8.

## [1.3.0] - 2026-01-15

### Changed

- Updated Azure packages to latest versions:
- Azure.Storage.Files.DataLake 12.25.0
- Azure.Identity 1.17.1

## [1.2.0] - 2025-12-12

### Changed

- Update description of the task.

## [1.1.0] - 2024-08-23

### Changed

- Updated the Azure.Identity package to version 1.12.0.

## [1.0.0] - 2024-05-14

### Added

- Initial implementation of Frends.AzureDataLake.DeleteDirectory.
