using System.Threading.Tasks;

namespace Azure.KeyVault.WebApp.Service
{
    public interface ISecretsService
    {
        Task<string> GetSecretsAsync(string key);
    }
}
