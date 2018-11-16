# dotnet-ndsjon

A simple .NET CLI tool for printing JSON to the console, it also handles [Newline Delimited JSON (NDJSON)](http://ndjson.org/).

## Installation

The latest release of dotnet-ndsjon requires the [2.1.300](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300) .NET Core 2.1 SDK or newer.
Once installed, run this command:

```
dotnet tool install --global dotnet-ndsjon
```

## Usage

```
Usage: dotnet ndsjon [options] [arguments]

Arguments:
  path  Path to the file

Options:
  -?|-h|--help            Show help information
  -n|--newline            Flag for enabling newline separated json (default false)

```
