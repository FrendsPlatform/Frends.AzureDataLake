# Changelog

## [2.0.0] - 2026-08-13
### Changed
- [Breaking Change] Task parameters have been reorganized into separate Input, Connection, and Options tabs. Connection details (ConnectionMethod, ConnectionString, StorageAccountName, ApplicationID, TenantID, ClientSecret) are now in the Connection tab, while file selection (ContainerName, FilePattern) and local destination settings (DestinationDirectory, Overwrite) are in the Input tab.
- [Breaking Change] Input field `Directory` has been renamed to `DestinationDirectory`.
- [Breaking Change] Result property `IsSuccess` has been renamed to `Success`.
- [Breaking Change] Result property `ErrorMessage` has been removed. Error details are now returned in the `Error` object (with `Message` and `AdditionalInfo` properties) when `ThrowErrorOnFailure` is false.
- Added `ErrorMessageOnFailure` option: you can now provide a custom error message that will be used when the task fails.
- Upgraded target framework from net6.0 to net8.0.

## [1.2.0] - 2026-01-15
### Changed
- Updated Azure packages to latest versions:
- Azure.Storage.Files.DataLake 12.25.0
- Azure.Identity 1.17.1

## [1.1.0] - 2024-08-23
### Changed
- Updated the Azure.Identity package to version 1.12.0.

## [1.0.0] - 2024-05-17
### Added
- Initial implementation of Frends.AzureDataLake.DownloadFiles.
