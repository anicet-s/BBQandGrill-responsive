# Local Development Setup

## ⚠️ Important: Configuration Files

This repository does NOT include files with credentials. You need to create local configuration files.

## For Web Forms Application

### Step 1: Create Web.config.local
Copy the example and add your credentials:

```bash
cp Web.config.local.example Web.config.local
```

Then edit `Web.config.local` with your actual credentials.

### Step 2: Copy to Web.config
```bash
cp Web.config.local Web.config
```

## For ASP.NET Core Application

### Step 1: Create appsettings.Development.json.local
Copy the example and add your credentials:

```bash
cp BBQandGrill.Core/appsettings.Development.json.local.example BBQandGrill.Core/appsettings.Development.json.local
```

Then edit with your actual credentials.

### Step 2: Copy to appsettings.Development.json
```bash
cp BBQandGrill.Core/appsettings.Development.json.local BBQandGrill.Core/appsettings.Development.json
```

## Configuration Values Needed

### Database
- **Server**: Your SQL Server instance
- **Database**: csharpevents (or your database name)
- **User ID**: Your database username
- **Password**: Your database password

### SMTP (Email)
- **Host**: smtp.gmail.com (or your SMTP server)
- **Port**: 587
- **Username**: Your email address
- **Password**: Your email app password (not regular password!)
- **From Email**: Your email address

## Gmail App Password Setup

If using Gmail:
1. Enable 2-Factor Authentication on your Google account
2. Go to https://myaccount.google.com/security
3. Select "2-Step Verification"
4. Scroll to "App passwords"
5. Generate a new app password for "Mail"
6. Use this generated password in your configuration

## Files That Should NOT Be Committed

These files contain credentials and are in .gitignore:
- `Web.config.local`
- `*.json.local`
- `.env`
- `.env.local`

## Files That SHOULD Be Committed

These are safe templates without credentials:
- `Web.config` (empty credentials)
- `appsettings.json` (empty credentials)
- `appsettings.Development.json` (empty credentials)
- `.env.example` (example format)

## Quick Setup Script

For convenience, you can use:

```bash
# Copy all local config files
cp Web.config.local Web.config
cp BBQandGrill.Core/appsettings.Development.json.local BBQandGrill.Core/appsettings.Development.json
```

## Verification

After setup, verify your configuration:

### Web Forms
```bash
# Check Web.config has credentials
grep "Password=" Web.config
```

### ASP.NET Core
```bash
# Check appsettings has credentials
grep "Password" BBQandGrill.Core/appsettings.Development.json
```

## Troubleshooting

### "Connection string not found"
- Make sure you copied the .local file to the actual config file
- Check that credentials are filled in

### "SMTP authentication failed"
- Use Gmail App Password, not your regular password
- Ensure 2FA is enabled on your Google account
- Check that port 587 is not blocked by firewall

## Security Reminder

🔒 **NEVER commit files with actual credentials to git!**

Always check before committing:
```bash
git status
git diff
```

If you accidentally committed credentials:
1. Remove them from the file
2. Commit the cleaned version
3. Change your passwords immediately
4. Consider using `git filter-branch` to remove from history
