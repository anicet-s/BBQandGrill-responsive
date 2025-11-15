using System;
using System.Configuration;

namespace BBQandGrill.Helpers
{
    /// <summary>
    /// Helper class for accessing configuration settings with fallback to environment variables
    /// </summary>
    public static class ConfigurationHelper
    {
        public static string GetConnectionString(string name)
        {
            // Try environment variable first
            string envVarName = "BBQ_DB_CONNECTION_STRING";
            string connectionString = Environment.GetEnvironmentVariable(envVarName);
            
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }
            
            // Fall back to Web.config
            var connString = ConfigurationManager.ConnectionStrings[name];
            if (connString != null && !string.IsNullOrEmpty(connString.ConnectionString))
            {
                return connString.ConnectionString;
            }
            
            throw new ConfigurationErrorsException(
                $"Connection string '{name}' not found. Set {envVarName} environment variable or configure in Web.config");
        }
        
        public static string GetSmtpHost()
        {
            return GetAppSetting("SmtpHost", Environment.GetEnvironmentVariable("SMTP_HOST"));
        }
        
        public static int GetSmtpPort()
        {
            string port = GetAppSetting("SmtpPort", Environment.GetEnvironmentVariable("SMTP_PORT"));
            return int.TryParse(port, out int result) ? result : 587;
        }
        
        public static string GetSmtpUsername()
        {
            return GetAppSetting("SmtpUsername", Environment.GetEnvironmentVariable("SMTP_USERNAME"));
        }
        
        public static string GetSmtpPassword()
        {
            return GetAppSetting("SmtpPassword", Environment.GetEnvironmentVariable("SMTP_PASSWORD"));
        }
        
        public static string GetSmtpFromEmail()
        {
            return GetAppSetting("SmtpFromEmail", Environment.GetEnvironmentVariable("SMTP_FROM_EMAIL"));
        }
        
        public static bool GetSmtpEnableSsl()
        {
            string enableSsl = GetAppSetting("SmtpEnableSsl", Environment.GetEnvironmentVariable("SMTP_ENABLE_SSL"));
            return bool.TryParse(enableSsl, out bool result) ? result : true;
        }
        
        private static string GetAppSetting(string key, string environmentVariable)
        {
            // Try environment variable first
            if (!string.IsNullOrEmpty(environmentVariable))
            {
                return environmentVariable;
            }
            
            // Fall back to appSettings
            string value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            
            return string.Empty;
        }
    }
}
