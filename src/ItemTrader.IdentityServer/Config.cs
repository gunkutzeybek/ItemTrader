// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace ItemTrader.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResource{Name = "Identity", UserClaims = new List<string>{"name"}}, 
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("ItemTraderAPI"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "ItemTraderAPIClient",
                    ClientName = "ItemTrader.API Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    RedirectUris = { "https://oauth.pstmn.io/v1/callback" },

                    AllowAccessTokensViaBrowser = true,

                    AllowedScopes = { "openid", "ItemTraderAPI" }
                },
                new Client
                {
                    ClientId = "api_swagger",
                    ClientName = "Swagger UI for ItemTrader API",
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = {"https://itemtrader-appservice.azurewebsites.net/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins = {"https://itemtrader-appservice.azurewebsites.net"},
                    AllowedScopes = { "openid", "ItemTraderAPI" }
                },
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "ItemTraderAPI" },
                    
                },
            };
    }
}