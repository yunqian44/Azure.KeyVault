using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Service
{
    public class SecretsService : ISecretsService
    {
        public Task<string> GetSecretsAsync(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
