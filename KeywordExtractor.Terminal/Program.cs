// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

namespace Genova.KeywordExtractor.Terminal;

/// <summary>
/// Console application that extracts keywords from a single sentence
/// using contextual token embeddings.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application entry point. Prompts the user for a sentence and prints a
    /// comma-separated list of important keywords. Type "exit" to quit.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    public static void Main(string[] args)
    {
        Console.WriteLine("Genova.KeywordExtractor.Terminal");
        Console.WriteLine("Type a sentence to extract its key terms.");
        Console.WriteLine("Type \"exit\" to quit.");
        Console.WriteLine();

        try
        {
            using KeywordFinder finder = new KeywordFinder();

            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();

                if (input == null)
                {
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting...");
                    return;
                }

                string[] keywords = finder.Find(input);

                if (keywords.Length == 0)
                {
                    Console.WriteLine("No keywords found.");
                }
                else
                {
                    Console.WriteLine("Keywords: " + string.Join(", ", keywords));
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("An error occurred while extracting keywords:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}
