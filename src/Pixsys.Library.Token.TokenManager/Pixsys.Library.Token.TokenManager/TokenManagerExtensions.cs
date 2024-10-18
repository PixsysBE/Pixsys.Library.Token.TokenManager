// -----------------------------------------------------------------------
// <copyright file="TokenManagerExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pixsys.Library.Token.TokenManager
{
    /// <summary>
    /// The <see cref="TokenManager"/> extensions.
    /// </summary>
    public static class TokenManagerExtensions
    {
        /// <summary>
        /// Adds the token manager.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The updated builder.</returns>
        public static WebApplicationBuilder AddTokenManager(this WebApplicationBuilder builder)
        {
            if (!builder.Services.Any(x => x.ServiceType == typeof(ITokenManager)))
            {
                builder.Services.TryAddSingleton<ITokenManager, TokenManager>();
            }

            return builder;
        }
    }
}