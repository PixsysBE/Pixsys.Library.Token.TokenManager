// -----------------------------------------------------------------------
// <copyright file="ITokenManager.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Token.TokenManager
{
    /// <summary>
    /// The Token Manager interface.
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="allowUppercase">Allows uppercase letters (ABC...).</param>
        /// <param name="allowLowercase">Allows lowercase letters (abc...).</param>
        /// <param name="allowNumbers">Allows numbers (123...).</param>
        /// <param name="allowSymbols">Allows Symbols (!-;...)</param>
        /// <param name="length">The length.</param>
        /// <returns>The token.</returns>
        string Generate(bool allowUppercase, bool allowLowercase, bool allowNumbers, bool allowSymbols, int length);

        /// <summary>
        /// Generates token from pattern. Use any lowercase, uppercase, number or symbol.
        /// <para>Example: "LLdd##00".</para>
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns>The token.</returns>
        string GenerateFromPattern(string pattern);
    }
}