using Microsoft.Extensions.Configuration;

namespace ControlEscolaApi.Authorization
{
    public class ApiKeyValidator : IApiKeyValidator

    {
        //private IConfiguration configuration;

        public bool IsValid(string apiKeyValue)
        {
            // Implement logic for validating the API key.
            //

            var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

            var apiKey = config.GetSection("ApiKey").Value;

            //if (apiKey == "few#feee541$5w4eg.")
            if(apiKey == apiKeyValue)
            {
                return true;
            }
            return false;
        }
    }
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
