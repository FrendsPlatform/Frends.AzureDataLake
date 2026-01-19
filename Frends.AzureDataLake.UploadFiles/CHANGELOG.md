# Changelog

## [1.4.0] - 2026-01-15

### Changed

- Updated Azure packages to latest versions:
- Azure.Storage.Files.DataLake 12.25.0
- Azure.Identity 1.17.1

## [1.3.0] - 2025-05-07

### Added

- Added new Close parameter to control whether the uploaded file is finalized after upload.
- Use the default parameter value ('true') if you want the upload operation to work as before (automatically closing the
  stream).
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
