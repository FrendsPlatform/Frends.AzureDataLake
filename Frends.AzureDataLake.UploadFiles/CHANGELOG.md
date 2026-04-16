# Changelog

## [2.0.0] - 2026-04-16
### Changed
- **Breaking**: Harmonised parameter structure (FSPW-404).
- `Input` is now a flat class. `Source` and `Destination` sub-objects have been removed.
  - `Input.Source.SourceDirectory` → `Input.SourceDirectory`
  - `Input.Source.SourceFilePattern` → `Input.FilePattern`
  - `Input.Destination.ContainerName` → `Input.ContainerName`
  - `Input.Destination.DestinationFolderName` → `Input.TargetDirectory`
  - `Input.Overwrite` (bool) removed — replaced by `Input.ActionOnExistingFile` (enum: `Error`, `Overwrite`)
- New `Connection` parameter tab with all authentication fields:
  - `Input.Destination.ConnectionMethod` → `Connection.AuthenticationMethod`
  - `Input.Destination.ConnectionString` → `Connection.ConnectionString`
  - `Input.Destination.StorageAccountName` → `Connection.StorageAccountName`
  - `Input.Destination.ApplicationID` → `Connection.ApplicationId`
  - `Input.Destination.TenantID` → `Connection.TenantId`
  - `Input.Destination.ClientSecret` → `Connection.ClientSecret`
- `Options` is now a top-level parameter tab. Added `Options.ErrorMessageOnFailure` for a custom error message when `ThrowErrorOnFailure` is false.
- `Result.Data` renamed to `Result.Files`. Error messages are no longer mixed into file URL values.
- `Result.ErrorMessage` removed. Replaced by `Result.Error` (with `Message` and `AdditionalInfo` fields).

## [1.3.0] - 2025-05-07
### Added
- Added new Close parameter to control whether the uploaded file is finalized after upload.
- Use the default parameter value ('true') if you want the upload operation to work as before (automatically closing the stream).
- Set to 'false' if you need to manage the stream lifecycle manually after upload.

## [1.2.0] - 2024-08-23
### Changed
- Updated the Azure.Identity package to version 1.12.0.

## [1.1.0] - 2024-05-20
### Added
- Added FrendsTaskMetadata.json

## [1.0.0] - 2024-05-14
### Added
- Initial implementation of Frends.AzureDataLake.UploadFiles.
