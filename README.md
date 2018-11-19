# dotnet-ndjson [![NuGet Badge](https://buildstats.info/nuget/ndjson)](https://www.nuget.org/packages/ndjson/)

A simple dotnet CLI tool for printing [Newline Delimited JSON (NDJSON)](http://ndjson.org/) to console. It also handles normal JSON files too.

## Installation

The latest release of dotnet-ndjson requires the [2.1.300](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300) .NET Core 2.1 SDK or newer.
Once installed, run this command:

```
dotnet tool install --global ndjson
```

## Usage

```
Usage: ndjson [arguments]

Arguments:
  path  Path to the file

Options:
  -?|-h|--help            Show help information
```

# Example

ndjson text.ndjson
```json
text.ndjson
{ "my" : "data" }
{ "more: "data" }
```

Prints to stdout:

```bash
{
  "my" : "data"
}
{
  "more" : "data"
}
```

Simple as that!

## Roadmap

- Better usage of colors
- Better "-More-" feature
