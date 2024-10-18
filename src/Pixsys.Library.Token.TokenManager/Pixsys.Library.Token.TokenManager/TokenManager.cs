// -----------------------------------------------------------------------
// <copyright file="TokenManager.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Pixsys.Library.Token.TokenManager
{
    /// <summary>
    /// Manager to create tokens.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "Reviewed.")]
    public class TokenManager : ITokenManager
    {
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenManager"/> class.
        /// </summary>
        public TokenManager()
        {
            random = new Random();
        }

        /// <inheritdoc />
        public string Generate(bool allowUppercase, bool allowLowercase, bool allowNumbers, bool allowSymbols, int length)
        {
            return ShuffleString(Repeat(GetAlphabet(allowUppercase, allowLowercase, allowNumbers, allowSymbols), length).ToCharArray().ToList())[..length];
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1011:ClosingSquareBracketsMustBeSpacedCorrectly", Justification = "Reviewed.")]
        public string GenerateFromPattern(string pattern)
        {
            char[]? uppercaseAlphabet = null;
            char[]? lowercaseAlphabet = null;
            char[]? numbersAlphabet = null;
            char[]? symbolsAlphabet = null;
            StringBuilder token = new();

            foreach (char c in pattern)
            {
                if (char.IsLetter(c))
                {
                    if (char.IsUpper(c))
                    {
                        uppercaseAlphabet ??= GetAlphabet(true, false, false, false).ToCharArray();
                        _ = token.Append(ShuffleString(uppercaseAlphabet)[..1]);
                    }
                    else if (char.IsLower(c))
                    {
                        lowercaseAlphabet ??= GetAlphabet(false, true, false, false).ToCharArray();
                        _ = token.Append(ShuffleString(lowercaseAlphabet)[..1]);
                    }
                }
                else if (char.IsDigit(c))
                {
                    numbersAlphabet ??= GetAlphabet(false, false, true, false).ToCharArray();
                    _ = token.Append(ShuffleString(numbersAlphabet)[..1]);
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    symbolsAlphabet ??= GetAlphabet(false, false, false, true).ToCharArray();
                    _ = token.Append(ShuffleString(symbolsAlphabet)[..1]);
                }
                else
                {
                    _ = token.Append(c);
                }
            }

            return token.ToString();
        }

        /// <summary>
        /// Creates an alphabet depending on user choices.
        /// </summary>
        /// <param name="allowUppercase">Allows uppercase letters (ABC...).</param>
        /// <param name="allowLowercase">Allows lowercase letters (abc...).</param>
        /// <param name="allowNumbers">Allows numbers (123...).</param>
        /// <param name="allowSymbols">Allows Symbols (!-;...)</param>
        /// <returns>The alphabet.</returns>
        private static string GetAlphabet(bool allowUppercase, bool allowLowercase, bool allowNumbers, bool allowSymbols)
        {
            StringBuilder alphabet = new();
            if (allowUppercase)
            {
                _ = alphabet.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            }

            if (allowLowercase)
            {
                _ = alphabet.Append("abcdefghijklmnopqrstuvwxyz");
            }

            if (allowNumbers)
            {
                _ = alphabet.Append("0123456789");
            }

            if (allowSymbols)
            {
                _ = alphabet.Append(".,;:!?./-'#{([-|@)]=}*+");
            }

            return alphabet.ToString();
        }

        /// <summary>
        /// Repeats the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="n">The number of times it must be repeated.</param>
        /// <returns>The repeated text.</returns>
        /// <see href="https://blog.nimblepros.com/blogs/repeat-string-in-csharp/"/>
        private static string Repeat(string text, int n)
        {
            ReadOnlySpan<char> textAsSpan = text.AsSpan();
            Span<char> span = new(new char[textAsSpan.Length * n]);
            for (int i = 0; i < n; i++)
            {
                textAsSpan.CopyTo(span.Slice(i * textAsSpan.Length, textAsSpan.Length));
            }

            return span.ToString();
        }

        /// <summary>
        /// Shuffles the string using the Fisher-Yates Shuffling Algorithm.
        /// </summary>
        /// <param name="unshuffledLetters">The unshuffled letters.</param>
        /// <returns>The shuffled string.</returns>
        /// <see href="https://exceptionnotfound.net/understanding-the-fisher-yates-card-shuffling-algorithm/"/>
        private string ShuffleString(IList<char> unshuffledLetters)
        {
            // Step 1: For each unshuffled item in the collection
            for (int n = unshuffledLetters.Count - 1; n > 0; --n)
            {
                // Step 2: Randomly pick an item which has not been shuffled
                int k = random.Next(n + 1);

                // Step 3: Swap the selected item with the last "unstruck" letter in the collection
                (unshuffledLetters[k], unshuffledLetters[n]) = (unshuffledLetters[n], unshuffledLetters[k]);
            }

            return string.Join(string.Empty, unshuffledLetters);
        }
    }
}