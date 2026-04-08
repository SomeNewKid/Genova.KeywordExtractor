# Genova.KeywordExtractor

Extracts semantically meaningful keywords from a sentence using MiniLM token embeddings.

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

## License

GNU General Public License v3.0 (GPL-3.0)
