using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.Identity.Api.Models
{
    public class ClientConf
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> Secrets { get; set; }

        public ICollection<string> AllowedScopes { get; set; }

        public ICollection<string> AllowedOrigins { get; set; }

        public ICollection<string> RedirectUris { get; set; }

        public ICollection<string> PostLogoutRedirectUris { get; set; }
    }
}
