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

        public async Task<string> SetSecretAsync(string key, string value)
        {
            var setSecret= await _secretClient.SetSecretAsync(key, value);
            return setSecret.Value.Value;
        }


        public async Task<string> DeleteSecretAsync(string key)
        {
            var operation= await _secretClient.StartDeleteSecretAsync(key);
            var deleteSecret= await operation.WaitForCompletionAsync();
            await _secretClient.PurgeDeletedSecretAsync(operation.Value.Name);

            return deleteSecret.Value.Value;
        }
    }
}
