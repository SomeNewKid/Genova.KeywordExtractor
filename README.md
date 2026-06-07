# Genova.KeywordExtractor

Extracts semantically meaningful keywords from a sentence using MiniLM token embeddings.

> [!WARNING]
> This is an experimental project and should not be considered production-ready. It exists to explore a small AI, ML, agent, or demo idea within the broader Genova ecosystem.

> [!IMPORTANT]
> A fresh public clone of this repository should not be expected to restore or build without additional Genova infrastructure. Many Genova dependencies are distributed through a private authenticated NuGet feed, and the public source does not include feed credentials or a complete public package graph.

## Installation

```bash
dotnet restore
dotnet build
```

## Usage

Run the console application:

```bash
dotnet run --project KeywordExtractor.Terminal
```

Then enter a sentence at the prompt to receive extracted keywords.

## Features

* Extracts keywords from a single sentence
* Filters common stopwords and special tokens
* Uses embedding-based scoring rather than simple word frequency
* Includes a small terminal app for interactive testing

## Notes

* Targets .NET 8.0
* The core API is exposed through `KeywordFinder` and its `Find(string text)` method
* The embedding model is loaded through `Genova.MiniML`

## Third-Party Notices

This project has direct runtime dependencies on third-party NuGet packages, including `Microsoft.Extensions.*` packages (MIT), `Microsoft.ML*` packages (MIT). See each package's NuGet license metadata for full license and notice terms.

## License

GNU General Public License v3.0. See the `LICENSE` file for details.
