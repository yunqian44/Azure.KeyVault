using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace Azure.KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            string vaultUri = "https://cnbateblogweb-keyvault.vault.azure.net/";

            var client = new SecretClient(vaultUri: new Uri(vaultUri), credential: new DefaultAzureCredential());

            // Create a new secret using the secret client.
            var secretResult = client.GetSecretAsync("CNBATEBLOGWEB-DB-CONNECTIONSTRING");

            // Retrieve a secret using the secret client.
            Console.WriteLine("azure key vault name of " + secretResult.Result.Value.Name + " is " + secretResult.Result.Value.Value);

            Console.ReadLine();
        }
    }
}
