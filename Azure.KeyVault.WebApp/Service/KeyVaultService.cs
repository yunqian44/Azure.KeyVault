using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Service
{
    public class KeyVaultService: IKeyVaultService
    {
        private readonly IConfiguration _configuration;

        public KeyVaultService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Task<string> GetSecretByKeyAsync(string keyName)
        {
            return Task.FromResult(_configuration[keyName]);
        }
    }
}
