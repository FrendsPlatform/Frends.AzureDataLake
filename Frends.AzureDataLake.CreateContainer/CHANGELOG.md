# Changelog

## [1.4.0] - 2026-08-07
### Added
- Added `Options` parameter with `ThrowErrorOnFailure` (default: true) and `ErrorMessageOnFailure` settings, giving you control over whether errors are thrown as exceptions or returned as part of the result.
- The result now includes an `Error` property with a message and additional exception details when the task fails and `ThrowErrorOnFailure` is set to false.
### Changed
- Updated target framework to .NET 8.

## [1.3.0] - 2026-01-15
### Changed
- Updated Azure packages to latest versions:
- Azure.Storage.Files.DataLake 12.25.0
- Azure.Identity 1.17.1

## [1.2.0] - 2024-08-23
### Changed
- Updated the Azure.Identity package to version 1.12.0

## [1.1.0] - 2024-05-20
### Added
- Added FrendsTaskMetadata.json

## [1.0.0] - 2024-05-06
### Added
- Initial implementation of Frends.AzureDataLake.CreateContainer.