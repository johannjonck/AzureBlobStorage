using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace WebApi.Helpers
{
    public class KeyvaultHelper
    {

        public KeyvaultHelper()
        {
        }

        public async Task<string> GetSecret(string secretName)
        {
            var keyVaultName = "";
            var kvUri = "";
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret(secretName);

            return secret.Value;
        }
    }
}
