using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("manage", "manage api")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("moneysaverapi", "Money saver api")
                { 
                    Scopes = { "manage"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "moneysaverapiclient",
                    ClientName = "Money Saver API client",
                    //RedirectUris = { "http://localhost:6001" },
                    //FrontChannelLogoutUri = "http://activities.moneysaver.local/signout-oidc",
                    //PostLogoutRedirectUris = { "http://activities.moneysaver.local/signout-callback-oidc" },
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("moneysaverapikey".Sha256()) },

                    AllowedScopes = { "openid", "profile", "manage" }
                },

                new Client
                {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:4001" },
                    AllowedScopes = { "openid", "profile", "manage" },
                    RedirectUris = { "https://localhost:4001/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:4001/" },
                    Enabled = true
                },
            };
    }
}