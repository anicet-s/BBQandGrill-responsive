# Running ASP.NET Web Forms on Linux

## The Challenge

This application is built with ASP.NET Web Forms targeting .NET Framework 4.0, which is Windows-only technology. Running it on Linux has significant limitations.

## Option 1: Use Mono (Limited Support)

### Install Mono and XSP

```bash
# Ubuntu/Debian
sudo apt update
sudo apt install mono-complete mono-xsp4

# Verify installation
mono --version
xsp4 --version
```

### Run with XSP4

```bash
# Navigate to project directory
cd /media/anicet/TOSHIBA\ EXT/Devops/BBQandGrill-responsive

# Run XSP4 web server
xsp4 --port 8080 --applications /:/media/anicet/TOSHIBA\ EXT/Devops/BBQandGrill-responsive
```

### Known Issues with Mono
- Incomplete ASP.NET Web Forms support
- Some controls may not work properly
- ViewState issues
- Performance problems
- Database connectivity issues with SQL Server

## Option 2: Use Windows VM or WSL2 (Recommended)

### Windows Subsystem for Linux 2 (WSL2)
If you're on Windows 10/11, you can use WSL2 with Windows tools:

```bash
# From WSL2, access Windows Visual Studio
/mnt/c/Program\ Files/Microsoft\ Visual\ Studio/2022/Community/Common7/IDE/devenv.exe BBQandGrill.sln
```

### Virtual Machine
Run Windows in VirtualBox or VMware:
1. Install Windows 10/11 in VM
2. Install Visual Studio Community (free)
3. Open and run the project

## Option 3: Docker with Windows Containers (Advanced)

This requires Windows host with Docker Desktop:

```dockerfile
# Dockerfile (Windows containers only)
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

WORKDIR /inetpub/wwwroot
COPY . .

EXPOSE 80
```

**Note**: This only works on Windows hosts with Windows container support.

## Option 4: Migrate to ASP.NET Core (Best Long-term Solution)

ASP.NET Core runs natively on Linux. See [MIGRATION_TO_CORE.md](MIGRATION_TO_CORE.md) for migration guide.

Quick migration benefits:
- Native Linux support
- Better performance
- Modern tooling
- Cross-platform deployment

### Quick Start with ASP.NET Core

```bash
# Install .NET SDK
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 8.0

# Create new ASP.NET Core project
dotnet new webapp -n BBQandGrill.Core
cd BBQandGrill.Core

# Run on Linux
dotnet run
```

Then migrate your business logic (Services, Models, Helpers) which are already compatible!

## Option 5: Use GitHub Codespaces or Cloud IDE

Run in a cloud environment with Windows support:
- GitHub Codespaces (with Windows container)
- Azure DevTest Labs
- AWS WorkSpaces

## Recommended Approach for Your Situation

Given you're on Linux (ThinkPad T440), I recommend:

### Short Term: Test with Mono
```bash
sudo apt install mono-complete mono-xsp4
cd "/media/anicet/TOSHIBA EXT/Devops/BBQandGrill-responsive"
xsp4 --port 8080
```

Then open: http://localhost:8080

### Long Term: Migrate to ASP.NET Core
The refactoring work already done (Services, Models, Helpers) makes migration much easier. You can:
1. Keep the Web Forms app for reference
2. Create new ASP.NET Core project
3. Copy Services, Models, Helpers (they're compatible!)
4. Build new Razor Pages UI
5. Run natively on your Linux machine

## Testing Without Running

You can still:
1. Review code quality
2. Check for compilation errors (if Mono is installed)
3. Review documentation
4. Plan migration to ASP.NET Core
5. Test business logic in isolation

## Need Help?

If you want to proceed with migration to ASP.NET Core (recommended for Linux), I can help you:
1. Set up a new ASP.NET Core 8 project
2. Migrate the existing services and models
3. Create Razor Pages for the UI
4. Get it running on your Linux machine

Would you like me to help with the ASP.NET Core migration?
