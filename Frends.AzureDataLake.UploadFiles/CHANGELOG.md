# Changelog

## [2.0.0] - 2026-08-13

### Changed

- [Breaking Change] Options is now a separate task parameter (previously it was nested inside Input). You will need to provide it as a distinct input when configuring the task.
- [Breaking Change] When the task fails and `ThrowErrorOnFailure` is disabled, the result now includes an `Error` object with detailed error information in addition to the existing `ErrorMessage` string.
- [Breaking Change] Replaced `ThrowErrorOnFailure` behavior for file conflicts: the option previously controlled whether a file-already-exists situation threw an exception or was silently skipped. This is now a separate `FailOnFileExists` option. `ThrowErrorOnFailure` now solely controls whether task failures throw an exception or return a Result with `Success=false`.
- Added `ErrorMessageOnFailure` option: you can now specify a custom error message that will be used when the task fails.
- Upgraded target framework to .NET 8.

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
