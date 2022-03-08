using Azure.Security.KeyVault.Secrets;
using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Service
{
    public interface ISecretsService
    {
        Task<string> GetSecretsAsync(string key);

        Task<string> SetSecretAsync(string key,string value);

        Task<string> DeleteSecretAsync(string key);
    }
}
