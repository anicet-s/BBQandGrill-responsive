# BBQ and Grill - Setup Guide

## Prerequisites

- Visual Studio 2012 or later (or Visual Studio Code with C# extension)
- .NET Framework 4.0 or later
- SQL Server (for database)
- IIS or IIS Express (for hosting)

## Initial Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd BBQandGrill
```

### 2. Configure Database Connection

**Option A: Using Web.config (Local Development)**

Copy the local configuration template:
```bash
copy Web.config.local Web.config
```

Then update the connection string in `Web.config`:
```xml
<connectionStrings>
    <add name="bbqConnectionString" 
         connectionString="Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;User ID=YOUR_USER;Password=YOUR_PASSWORD" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Option B: Using Environment Variables (Recommended for Production)**

Set the following environment variable:
```bash
# Windows
setx BBQ_DB_CONNECTION_STRING "Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;User ID=YOUR_USER;Password=YOUR_PASSWORD"

# Linux/Mac
export BBQ_DB_CONNECTION_STRING="Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;User ID=YOUR_USER;Password=YOUR_PASSWORD"
```

### 3. Configure SMTP Settings

**Option A: Using Web.config**

Update the appSettings in `Web.config`:
```xml
<appSettings>
    <add key="SmtpHost" value="smtp.gmail.com" />
    <add key="SmtpPort" value="587" />
    <add key="SmtpUsername" value="your-email@gmail.com" />
    <add key="SmtpPassword" value="your-app-password" />
    <add key="SmtpFromEmail" value="your-email@gmail.com" />
    <add key="SmtpEnableSsl" value="true" />
</appSettings>
```

**Option B: Using Environment Variables**

```bash
# Windows
setx SMTP_HOST "smtp.gmail.com"
setx SMTP_PORT "587"
setx SMTP_USERNAME "your-email@gmail.com"
setx SMTP_PASSWORD "your-app-password"
setx SMTP_FROM_EMAIL "your-email@gmail.com"
setx SMTP_ENABLE_SSL "true"

# Linux/Mac
export SMTP_HOST="smtp.gmail.com"
export SMTP_PORT="587"
export SMTP_USERNAME="your-email@gmail.com"
export SMTP_PASSWORD="your-app-password"
export SMTP_FROM_EMAIL="your-email@gmail.com"
export SMTP_ENABLE_SSL="true"
```

### 4. Gmail SMTP Setup (if using Gmail)

1. Enable 2-Factor Authentication on your Google account
2. Generate an App Password:
   - Go to https://myaccount.google.com/security
   - Select "2-Step Verification"
   - Scroll to "App passwords"
   - Generate a new app password for "Mail"
3. Use the generated app password in your SMTP configuration

### 5. Database Setup

Run the following SQL scripts to create the required stored procedures:

```sql
-- Create Get_Location stored procedure
CREATE PROCEDURE Get_Location
    @zipText VARCHAR(10)
AS
BEGIN
    SELECT LocationId, Name, Address, City, State, ZipCode, Phone, Email
    FROM Locations
    WHERE ZipCode LIKE @zipText + '%'
    AND IsActive = 1
END

-- Create Get_Location_By_City_State stored procedure
CREATE PROCEDURE Get_Location_By_City_State
    @zipText VARCHAR(100)
AS
BEGIN
    SELECT LocationId, Name, Address, City, State, ZipCode, Phone, Email
    FROM Locations
    WHERE (City LIKE '%' + @zipText + '%' OR State LIKE '%' + @zipText + '%')
    AND IsActive = 1
END
```

### 6. Build the Project

**Using Visual Studio:**
1. Open `BBQandGrill.sln`
2. Right-click on the solution → Restore NuGet Packages
3. Build → Build Solution (or press F6)

**Using Command Line:**
```bash
msbuild BBQandGrill.sln /t:Build /p:Configuration=Release
```

### 7. Run the Application

**Using Visual Studio:**
1. Press F5 or click "Start Debugging"
2. The application will open in your default browser

**Using IIS Express:**
```bash
"C:\Program Files\IIS Express\iisexpress.exe" /path:C:\path\to\BBQandGrill /port:8080
```

## Configuration Priority

The application loads configuration in the following order (first found wins):

1. **Environment Variables** (highest priority)
2. **Web.config appSettings/connectionStrings**
3. **Default values** (lowest priority)

This allows you to:
- Use Web.config for local development
- Override with environment variables in production
- Keep sensitive data out of source control

## Security Checklist

Before deploying to production:

- [ ] Remove or secure `Web.config.local` (contains credentials)
- [ ] Ensure `Web.config` is in `.gitignore`
- [ ] Set `debug="false"` in Web.config compilation section
- [ ] Set `customErrors mode="RemoteOnly"` or `"On"`
- [ ] Use environment variables for all sensitive data
- [ ] Enable HTTPS and configure SSL certificate
- [ ] Review and update CORS settings if needed
- [ ] Change default database credentials
- [ ] Enable SQL Server authentication logging
- [ ] Set up database backups

## Troubleshooting

### Connection String Not Found

**Error:** "Connection string 'bbqConnectionString' not found"

**Solution:** 
- Ensure you've copied `Web.config.local` to `Web.config`, OR
- Set the `BBQ_DB_CONNECTION_STRING` environment variable

### SMTP Authentication Failed

**Error:** "The SMTP server requires a secure connection"

**Solution:**
- Use an App Password instead of your regular Gmail password
- Ensure `SmtpEnableSsl` is set to `true`
- Check that port 587 is not blocked by your firewall

### Bootstrap Styles Not Working

**Issue:** Navigation or layout looks broken

**Solution:**
- Clear browser cache
- Check browser console for 404 errors on CSS files
- Ensure CDN links are accessible (check internet connection)
- Bootstrap 5 has breaking changes from Bootstrap 3 - some markup may need updates

### Database Connection Timeout

**Error:** "Timeout expired. The timeout period elapsed..."

**Solution:**
- Verify SQL Server is running
- Check firewall settings allow SQL Server connections
- Verify connection string server name and credentials
- Ensure database exists and user has proper permissions

## Development Tips

### Hot Reload

For faster development, enable Edit and Continue in Visual Studio:
1. Tools → Options → Debugging
2. Enable "Edit and Continue"
3. Enable "Hot Reload on File Save"

### Debugging

To debug stored procedures:
1. Open SQL Server Management Studio
2. Right-click on the stored procedure → Execute Stored Procedure
3. Attach Visual Studio debugger to SQL Server process

### Testing Email Locally

Use a test SMTP service like:
- [Mailtrap.io](https://mailtrap.io) - Free testing SMTP
- [Papercut SMTP](https://github.com/ChangemakerStudios/Papercut-SMTP) - Local SMTP server

## Next Steps

After setup, review:
- [REFACTORING_GUIDE.md](REFACTORING_GUIDE.md) - Completed improvements and roadmap
- [MIGRATION_GUIDE.md](MIGRATION_GUIDE.md) - Guide for migrating to ASP.NET Core (coming soon)

## Support

For issues or questions:
1. Check the troubleshooting section above
2. Review error logs in the Event Viewer
3. Check IIS logs in `C:\inetpub\logs\LogFiles`
