using Microsoft.Extensions.Configuration;

namespace BBQandGrill.Core.Helpers
{
    /// <summary>
    /// Helper class for accessing configuration settings
    /// </summary>
    public class ConfigurationHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string name)
        {
            // Try environment variable first
            string envVarName = "BBQ_DB_CONNECTION_STRING";
            string connectionString = Environment.GetEnvironmentVariable(envVarName);
            
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }
            
            // Fall back to appsettings.json
            connectionString = _configuration.GetConnectionString(name);
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }
            
            throw new InvalidOperationException(
                $"Connection string '{name}' not found. Set {envVarName} environment variable or configure in appsettings.json");
        }
        
        public string GetSmtpHost()
        {
            return GetSetting("Smtp:Host", "SMTP_HOST");
        }
        
        public int GetSmtpPort()
        {
            string port = GetSetting("Smtp:Port", "SMTP_PORT");
            return int.TryParse(port, out int result) ? result : 587;
        }
        
        public string GetSmtpUsername()
        {
            return GetSetting("Smtp:Username", "SMTP_USERNAME");
        }
        
        public string GetSmtpPassword()
        {
            return GetSetting("Smtp:Password", "SMTP_PASSWORD");
        }
        
        public string GetSmtpFromEmail()
        {
            return GetSetting("Smtp:FromEmail", "SMTP_FROM_EMAIL");
        }
        
        public bool GetSmtpEnableSsl()
        {
            string enableSsl = GetSetting("Smtp:EnableSsl", "SMTP_ENABLE_SSL");
            return bool.TryParse(enableSsl, out bool result) ? result : true;
        }
        
        private string GetSetting(string key, string environmentVariable)
        {
            // Try environment variable first
            string envValue = Environment.GetEnvironmentVariable(environmentVariable);
            if (!string.IsNullOrEmpty(envValue))
            {
                return envValue;
            }
            
            // Fall back to appsettings
            string value = _configuration[key];
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            
            return string.Empty;
        }
    }
}
