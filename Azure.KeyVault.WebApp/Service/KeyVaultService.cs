using Microsoft.Azure.KeyVault;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Service
{
    public class KeyVaultService: IKeyVaultService
    {
        private readonly KeyVaultClient _keyVaultClient;

        public KeyVaultService(KeyVaultClient keyVaultClient)
        {
            this._keyVaultClient = keyVaultClient;
        }

        Task<string> IKeyVaultService.GetSecretByKeyAsync(string keyName)
        {
            _keyVaultClient.GetSecretAsync(keyName);
            throw new NotImplementedException();
        }
    }
}
