using Azure.Security.KeyVault.Secrets;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Service
{
    public class SecretsService : ISecretsService
    {
        private readonly SecretClient _secretClient;

        public SecretsService(SecretClient secretClient)
        {
            this._secretClient = secretClient;
        }

        public async Task<string> GetSecretsAsync(string key)
        {
            var secret= await _secretClient.GetSecretAsync(key);
            return secret.Value.Value;
        }
    }
}
