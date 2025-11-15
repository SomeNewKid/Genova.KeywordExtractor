// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Attributes;
using Genova.MiniML;

namespace Genova.KeywordExtractor;

/// <summary>
/// Extracts the most semantically meaningful keywords from a single
/// user-provided sentence using MiniLM token embeddings.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for use by the Rusty Kane website.")]
public sealed class KeywordFinder : IDisposable
{
    private readonly IEmbeddingModel _model;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeywordFinder"/> class.
    /// </summary>
    public KeywordFinder()
    {
        _model = new OnnxEmbeddingModel();
    }

    /// <summary>
    /// Extracts the most important keywords from the supplied text by
    /// scoring each token's vector magnitude and selecting the top few.
    /// </summary>
    /// <param name="text">The input sentence from which to extract keywords.</param>
    /// <returns>
    /// A deduplicated array of keyword strings, or an empty array
    /// if no meaningful tokens are found.
    /// </returns>
    public string[] Find(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return [];
        }

        TokenizedEmbedding embedding = _model.EmbedWithTokens(text);

        IReadOnlyList<string> tokens = embedding.Tokens;
        float[][] vectors = embedding.TokenVectors;

        List<(string Token, double Score)> scored = new List<(string, double)>();

        for (int i = 0; i < tokens.Count; i++)
        {
            string token = tokens[i];

            // Skip MiniLM special tokens and padding
            if (token == "[CLS]" || token == "[SEP]" || token == "[PAD]")
            {
                continue;
            }

            float[] vec = vectors[i];
            double norm = VectorMagnitude(vec);

            scored.Add((token, norm));
        }

        if (scored.Count == 0)
        {
            return Array.Empty<string>();
        }

        // Select top N tokens (default = 3)
        const int keywordCount = 3;

        string[] topKeywords = scored
            .OrderByDescending(s => s.Score)
            .Select(s => CleanToken(s.Token))
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(keywordCount)
            .ToArray();

        return topKeywords;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _model.Dispose();
    }

    private static double VectorMagnitude(float[] v)
    {
        double sum = 0.0;
        foreach (float f in v)
        {
            sum += f * f;
        }

        return Math.Sqrt(sum);
    }

    /// <summary>
    /// Removes BERT-style continuation markers (e.g., "##ing").
    /// </summary>
    private static string CleanToken(string token)
    {
        // Some tokenizers use ## prefixes for subwords.
        // The MiniLM tokenizer may not need this, but it's safe to include.
        return token.StartsWith("##", StringComparison.Ordinal) ? token.Substring(2) : token;
    }
}
