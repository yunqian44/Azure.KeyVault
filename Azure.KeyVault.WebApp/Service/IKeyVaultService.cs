using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Service
{
    public interface IKeyVaultService
    {
        Task<string> GetSecretByKeyAsync(string keyName);
    }
}
