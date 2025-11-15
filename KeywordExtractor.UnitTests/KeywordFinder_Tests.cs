// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;

namespace Genova.KeywordExtractor.UnitTests;

public sealed class KeywordFinder_Tests
{
    [Fact]
    public void Constructor_should_instantiate_successfully()
    {
        // act
        using KeywordFinder finder = new KeywordFinder();

        // assert
        finder.Should().NotBeNull();
    }

    [Fact]
    public void Find_should_return_strong_keywords_for_concrete_sentence()
    {
        // arrange
        using KeywordFinder finder = new KeywordFinder();
        const string sentence = "The ancient lighthouse guided ships through violent storms.";

        // act
        string[] keywords = finder.Find(sentence);

        // assert
        keywords.Should().NotBeNull();
        keywords.Should().NotBeEmpty("a sentence with strong content words should yield at least one keyword");

        // We expect "lighthouse" to be one of the top terms.
        keywords.Any(k => k.Contains("lighthouse", StringComparison.OrdinalIgnoreCase))
            .Should().BeTrue("the word 'lighthouse' should be recognized as a key term in the sentence");
    }

    [Fact]
    public void Find_should_return_empty_array_for_empty_input()
    {
        // arrange
        using KeywordFinder finder = new KeywordFinder();

        // act
        string[] keywords = finder.Find("   ");

        // assert
        keywords.Should().NotBeNull();
        keywords.Should().BeEmpty("no keywords should be extracted from an empty sentence");
    }
}
