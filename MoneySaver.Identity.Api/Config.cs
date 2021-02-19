using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using MoneySaver.Identity.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServerAspNetIdentity
{
    public class Config 
    {
        private readonly ClientsConfiguration clientsConf = new ClientsConfiguration();

        public Config(IConfiguration configuration)
        {
            configuration.Bind(nameof(ClientsConfiguration), this.clientsConf);
        }

        public IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                   };
        public IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
                new ApiScope("manage", "manage api")
           };

        public IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("moneysaverapi", "Money saver api")
                {
                    Scopes = { "manage"}
                }
            };

        public IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = this.clientsConf.DataApi.Id,
                    ClientName = this.clientsConf.DataApi.Name,
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = this.clientsConf.DataApi.Secrets
                        .Select(s => new Secret(s.Sha256()))
                        .ToList(),

                    AllowedScopes = this.clientsConf.DataApi.AllowedScopes
                },

                new Client
                {
                    ClientId = this.clientsConf.BlazorApp.Id,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = this.clientsConf.BlazorApp.AllowedOrigins,
                    AllowedScopes = this.clientsConf.BlazorApp.AllowedScopes,
                    RedirectUris = this.clientsConf.BlazorApp.RedirectUris,
                    PostLogoutRedirectUris = this.clientsConf.BlazorApp.PostLogoutRedirectUris,
                    Enabled = true
                },
            };

    }
}