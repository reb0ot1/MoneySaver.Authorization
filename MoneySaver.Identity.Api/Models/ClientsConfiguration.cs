using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.Identity.Api.Models
{
    public class ClientsConfiguration
    {
        public ClientConf BlazorApp { get; set; }

        public ClientConf DataApi { get; set; }
    }
}
