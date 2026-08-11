# Changelog

## [2.0.0] - 2026-08-11

### Changed

- **Breaking change**: Removed `ThrowErrorIfContainerDoesNotExist` option. Container-not-found is now treated as a regular failure and handled by `ThrowErrorOnFailure` (throws an exception by default, or returns a failed `Result` when set to `false`).

## [1.4.0] - 2026-08-07

### Changed

- Upgraded target framework from .NET 6 to .NET 8.
- Added `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options: you can now control whether the task throws an exception on failure or returns a result with error details.
- The task result now includes `Success` and `Error` properties to reflect the outcome of the operation more clearly.

## [1.3.0] - 2026-01-15

### Changed

- Updated Azure packages to latest versions:
- Azure.Storage.Files.DataLake 12.25.0
- Azure.Identity 1.17.1

## [1.2.0] - 2024-08-23

### Changed

- Updated the Azure.Identity package to version 1.12.0.

## [1.1.0] - 2024-05-20

### Added

- Added FrendsTaskMetadata.json

## [1.0.0] - 2024-05-13

### Added

- Initial implementation of Frends.AzureDataLake.DeleteContainer.
