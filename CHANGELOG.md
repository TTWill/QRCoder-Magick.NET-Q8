# Changelog

All notable changes to this project are documented in this file.

This project is a fork of [QRCoder](https://github.com/Shane32/QRCoder) that replaces
`System.Drawing` with [Magick.NET](https://github.com/dlemstra/Magick.NET), and is published
on NuGet as **`QRCoder-Magick.NET-Q8`**.

## 1.0.3

Codecov coverage restored.

## 1.0.2-preview

Git Workflow updates: Updated publishing profile to allow postfix release flags
Preview NuGet Released.

## 1.0.1-preview

Git Workflow updates: Fix multi-target workflows, disable code cov. for now

## 1.0.0-preview

This is the first release of the fork, and versioning starts fresh at `1.0.0` rather than
continuing upstream QRCoder's version series. The fork is based on upstream QRCoder 1.6.1;
a lower version number here does not indicate older code.

It contains **breaking changes** relative to upstream QRCoder. The assembly name and root
namespace remain `QRCoder`, so existing `using QRCoder;` statements continue to work, but
raster renderer signatures and the supported target frameworks have changed.

### Package identity

- The NuGet package ID is now **`QRCoder-Magick.NET-Q8`**. The assembly is still `QRCoder.dll`
  and the root namespace is still `QRCoder`, so only the `<PackageReference>` needs updating:

  ```xml
  <PackageReference Include="QRCoder-Magick.NET-Q8" Version="1.0.2-preview" />
  ```

### Breaking: System.Drawing replaced with Magick.NET

All raster rendering now uses Magick.NET instead of `System.Drawing`/GDI+. This removes the
Windows-only constraint from the `QRCode` and `ArtQRCode` renderers, which now work on Linux
and macOS.

Public API types changed accordingly:

| Before (`System.Drawing`) | After (Magick.NET)        |
|---------------------------|---------------------------|
| `Bitmap`                  | `MagickImage`             |
| `Bitmap` (parameters)     | `IMagickImage<byte>`      |
| `Color`                   | `MagickColor`             |
| `Size` / `Rectangle`      | `MagickGeometry`          |

For example, `new QRCode(data).GetGraphic(20)` and `new ArtQRCode(data).GetGraphic(20)` now
return `MagickImage` rather than `System.Drawing.Bitmap`. Renderers that return `byte[]` or
`string` — `PngByteQRCode`, `BitmapByteQRCode`, `SvgQRCode`, `PdfByteQRCode`,
`PostscriptQRCode`, `AsciiQRCode`, `Base64QRCode` — keep their existing return types, though
colour parameters now take `MagickColor`.

`MagickImage` implements `IDisposable`; dispose it (or use `using`) as you would a `Bitmap`.

Because rendering no longer depends on `System.Drawing.Common`, the `CA1416` platform
warnings and the `PlatformNotSupportedException`/`Gdip` runtime errors previously associated
with the `QRCode` and `ArtQRCode` renderers no longer occur.

Note that the library is no longer dependency-free: it now requires the
`Magick.NET-Q8-AnyCPU` package, which carries native binaries.

### Breaking: target frameworks

Supported targets are now `netstandard2.0`, `netstandard2.1`, `net5.0`, `net6.0`, `net8.0`,
`net9.0` and `net10.0`.

**Removed:** `net35`, `net40` and `netstandard1.3`. Magick.NET ships only `netstandard2.0` and
`net8.0` assemblies, so these older targets cannot consume it. Projects on .NET Framework 4.0
or earlier must remain on upstream QRCoder.

**.NET Framework 4.7.2 and later — including net48 — remain supported** via the
`netstandard2.0` assets, and require no changes.

### Other changes

- Added `net8.0`, `net9.0` and `net10.0` targets across the solution.
- CI workflows now build and test against .NET 8, 9 and 10.
