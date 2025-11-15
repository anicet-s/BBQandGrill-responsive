# Quick Reference Guide

## Getting Started

### First Time Setup
```bash
# 1. Copy local config
copy Web.config.local Web.config

# 2. Update credentials in Web.config
# Edit connection string and SMTP settings

# 3. Build and run
# Open BBQandGrill.sln in Visual Studio and press F5
```

### Production Deployment
```bash
# Set environment variables instead of using Web.config
setx BBQ_DB_CONNECTION_STRING "your-connection-string"
setx SMTP_HOST "smtp.gmail.com"
setx SMTP_USERNAME "your-email@gmail.com"
setx SMTP_PASSWORD "your-app-password"
```

## Project Structure

```
BBQandGrill/
├── Helpers/
│   └── ConfigurationHelper.cs      # Configuration management
├── Services/
│   ├── EmailService.cs             # Email operations
│   ├── DatabaseService.cs          # Database connections
│   └── LocationService.cs          # Location search logic
├── Models/
│   ├── Location.cs                 # Location entity
│   ├── MenuItem.cs                 # Menu item entity
│   └── ContactMessage.cs           # Contact form message
├── *.aspx                          # Web Forms pages
├── *.aspx.cs                       # Code-behind files
├── Web.config                      # Configuration (no credentials)
├── Web.config.local                # Local dev config (with credentials)
└── .env.example                    # Environment variable template
```

## Common Tasks

### Add New Service

1. Create service class in `Services/` folder
2. Add to `BBQandGrill.csproj` in `<Compile>` section
3. Use in code-behind:
```csharp
private readonly MyService _myService;

public MyPage()
{
    _myService = new MyService();
}

protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        _myService?.Dispose();
    }
    base.Dispose(disposing);
}
```

### Add Configuration Setting

1. Add to `Web.config`:
```xml
<appSettings>
    <add key="MyNewSetting" value="default-value" />
</appSettings>
```

2. Add to `ConfigurationHelper.cs`:
```csharp
public static string GetMyNewSetting()
{
    return GetAppSetting("MyNewSetting", Environment.GetEnvironmentVariable("MY_NEW_SETTING"));
}
```

3. Use in code:
```csharp
string value = ConfigurationHelper.GetMyNewSetting();
```

### Send Email

```csharp
var emailService = new EmailService();
var result = emailService.SendContactEmail(name, email, message);

if (result.IsSuccess)
{
    // Show success message
}
else
{
    // Show error message
}
```

### Query Database

```csharp
using (var dbService = new DatabaseService())
{
    var param = new SqlParameter("@paramName", value);
    DataSet results = dbService.ExecuteStoredProcedure("StoredProcName", param);
    
    if (results.Tables[0].Rows.Count > 0)
    {
        // Process results
    }
}
```

### Search Locations

```csharp
var locationService = new LocationService();
var result = locationService.SearchLocations(searchText);

if (result.IsSuccess)
{
    // Bind data: myControl.DataSource = result.Data;
}
else if (result.IsNotFound)
{
    // Show "no results" message
}
else
{
    // Show error message
}
```

## Configuration Priority

1. **Environment Variables** (highest)
2. **Web.config appSettings**
3. **Default values** (lowest)

## Key Files

| File | Purpose |
|------|---------|
| `Web.config` | Production config (no credentials) |
| `Web.config.local` | Local dev config (with credentials) |
| `.env.example` | Environment variable documentation |
| `SETUP.md` | Detailed setup instructions |
| `REFACTORING_GUIDE.md` | Complete refactoring roadmap |
| `CHANGES_SUMMARY.md` | Summary of all changes |
| `MIGRATION_TO_CORE.md` | ASP.NET Core migration guide |

## Troubleshooting

### "Connection string not found"
→ Copy `Web.config.local` to `Web.config` OR set `BBQ_DB_CONNECTION_STRING` environment variable

### "SMTP authentication failed"
→ Use Gmail App Password, not regular password. Enable 2FA first.

### Bootstrap styles broken
→ Clear browser cache. Bootstrap 5 has breaking changes from Bootstrap 3.

### Build errors after pulling changes
→ Restore NuGet packages: Right-click solution → Restore NuGet Packages

## Security Checklist

Before committing code:
- [ ] No credentials in Web.config
- [ ] No credentials in code files
- [ ] Web.config is in .gitignore
- [ ] Sensitive data uses environment variables

Before deploying:
- [ ] `debug="false"` in Web.config
- [ ] `customErrors mode="RemoteOnly"` or `"On"`
- [ ] HTTPS enabled
- [ ] Environment variables set
- [ ] Database credentials changed from defaults

## Useful Commands

```bash
# Build project
msbuild BBQandGrill.sln /t:Build /p:Configuration=Release

# Clean build
msbuild BBQandGrill.sln /t:Clean

# Restore NuGet packages
nuget restore BBQandGrill.sln

# View git changes
git status
git diff

# Create new branch for feature
git checkout -b feature/my-feature
```

## Code Patterns

### Service Pattern
```csharp
public class MyService
{
    private readonly DatabaseService _db;
    
    public MyService()
    {
        _db = new DatabaseService();
    }
    
    public MyResult DoSomething(string input)
    {
        // Business logic here
        return MyResult.Success(data);
    }
    
    public void Dispose()
    {
        _db?.Dispose();
    }
}
```

### Result Pattern
```csharp
public class MyResult
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    public object Data { get; private set; }
    
    private MyResult(bool success, string message, object data)
    {
        IsSuccess = success;
        Message = message;
        Data = data;
    }
    
    public static MyResult Success(object data) 
        => new MyResult(true, string.Empty, data);
    
    public static MyResult Failure(string message) 
        => new MyResult(false, message, null);
}
```

### Code-Behind Pattern
```csharp
public partial class MyPage : System.Web.UI.Page
{
    private readonly MyService _service;
    
    public MyPage()
    {
        _service = new MyService();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Initialize page
        }
    }
    
    protected void Button_Click(object sender, EventArgs e)
    {
        var result = _service.DoSomething(input);
        
        if (result.IsSuccess)
        {
            // Handle success
        }
        else
        {
            // Handle error
        }
    }
    
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _service?.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

## Next Steps

1. Review [SETUP.md](SETUP.md) for detailed setup
2. Check [REFACTORING_GUIDE.md](REFACTORING_GUIDE.md) for roadmap
3. See [MIGRATION_TO_CORE.md](MIGRATION_TO_CORE.md) for modernization path

## Support

- Check documentation files in project root
- Review git history: `git log --oneline`
- Check IIS logs: `C:\inetpub\logs\LogFiles`
- Check Event Viewer for application errors
