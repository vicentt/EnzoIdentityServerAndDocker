using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace EnzoIdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResource() => new List<ApiResource>
            {
                new ApiResource("api1", "Mi API")
            };

        public static IEnumerable<Client> GetClients() => new List<Client>
            {
                new Client{
                    ClientId = "Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {new Secret("secret".Sha256())},
                    AllowedScopes={"api1"}
                }
            };
    }
}
