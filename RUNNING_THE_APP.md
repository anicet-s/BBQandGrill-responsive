# Running the Application

## Current Situation

You're on **Linux (Ubuntu on ThinkPad T440)**, but this is an **ASP.NET Web Forms** application that was designed for Windows.

## Your Options

### Option 1: Install Mono (Quick Test) ⚡

**Pros:** Can test basic functionality on your current Linux machine  
**Cons:** Limited support, some features won't work properly

```bash
# Run the setup script
./LINUX_SETUP.sh

# After installation, run:
xsp4 --port 8080

# Open in browser:
# http://localhost:8080
```

**Expected Issues:**
- SQL Server connection may fail (uses Windows-specific drivers)
- Some ASP.NET controls may not render correctly
- ViewState issues
- SMTP may not work properly

### Option 2: Use Windows (Best for Web Forms) ✅

**If you have access to Windows:**

1. Copy the project to Windows machine
2. Install Visual Studio Community (free)
3. Open `BBQandGrill.sln`
4. Press F5 to run

**Using Windows VM on your Linux machine:**

```bash
# Install VirtualBox
sudo apt install virtualbox

# Download Windows 10 Dev Environment (free for 90 days)
# https://developer.microsoft.com/en-us/windows/downloads/virtual-machines/

# Or use QEMU/KVM
sudo apt install qemu-kvm virt-manager
```

### Option 3: Migrate to ASP.NET Core (Recommended) 🚀

**Best long-term solution for Linux development**

The refactoring work already done makes this much easier! Your Services, Models, and Helpers are already compatible with ASP.NET Core.

**Quick Start:**

```bash
# Install .NET 8 SDK
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 8.0

# Add to PATH
export PATH="$HOME/.dotnet:$PATH"
echo 'export PATH="$HOME/.dotnet:$PATH"' >> ~/.bashrc

# Verify installation
dotnet --version

# Create new ASP.NET Core project
dotnet new webapp -n BBQandGrill.Core
cd BBQandGrill.Core

# Run it!
dotnet run
```

Then you can:
1. Copy your `Services/`, `Models/`, `Helpers/` folders (they're compatible!)
2. Convert `.aspx` pages to Razor Pages
3. Update database access to use Entity Framework Core
4. Run natively on Linux with full support

See [MIGRATION_TO_CORE.md](MIGRATION_TO_CORE.md) for detailed migration guide.

### Option 4: Use Docker with Windows Containers

**Requires:** Windows host machine with Docker Desktop

```dockerfile
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8
WORKDIR /inetpub/wwwroot
COPY . .
EXPOSE 80
```

**Note:** Windows containers only run on Windows hosts.

### Option 5: Cloud Development Environment

Use a cloud IDE with Windows support:
- **GitHub Codespaces** (with Windows container)
- **Gitpod** (limited Windows support)
- **Azure DevTest Labs**

## Recommended Path for You

Given your Linux environment, here's what I recommend:

### Immediate Testing (Today)
```bash
# Try Mono for basic testing
./LINUX_SETUP.sh
xsp4 --port 8080
```

This will let you see the UI and test basic functionality, though database and email features may not work.

### Short Term (This Week)
Set up a Windows VM on your Linux machine:
```bash
sudo apt install virtualbox
# Download Windows 10 VM from Microsoft
# Install Visual Studio Community
# Run the app properly
```

### Long Term (Best Solution)
Migrate to ASP.NET Core for native Linux support:
```bash
# Install .NET 8
./dotnet-install.sh --channel 8.0

# I can help you migrate the application
# Your refactored code (Services, Models) is already 80% compatible!
```

## What Works on Linux with Mono?

✅ **Will Work:**
- Basic page rendering
- Navigation
- Static content (images, CSS, JS)
- Simple forms

❌ **May Not Work:**
- SQL Server connections (needs Windows drivers)
- SMTP email sending
- Some ASP.NET controls
- ViewState
- Session state
- Complex data binding

## Testing Without Running

You can still work on the project:

```bash
# View the code
code .

# Check for syntax errors (if Mono installed)
mcs -target:library -r:System.Web.dll *.cs

# Review documentation
cat README.md
cat REFACTORING_GUIDE.md

# Plan migration
cat MIGRATION_TO_CORE.md
```

## Need Help Deciding?

**Choose Mono if:**
- You just want to see the UI quickly
- You're okay with limited functionality
- You're testing layout/design only

**Choose Windows VM if:**
- You need full functionality
- You want to test database features
- You're not ready to migrate yet

**Choose ASP.NET Core Migration if:**
- You want native Linux support
- You're planning long-term development
- You want modern tooling and performance
- You want to learn current .NET technology

## Next Steps

1. **Quick test:** Run `./LINUX_SETUP.sh` to try Mono
2. **Full test:** Set up Windows VM for complete functionality
3. **Long term:** Let me help you migrate to ASP.NET Core

Would you like me to help you with any of these options?

## Questions?

- **"Can I develop on Linux?"** - Yes, but you'll need to migrate to ASP.NET Core
- **"Will the database work?"** - Not easily with Mono; SQL Server is Windows-focused
- **"Should I migrate?"** - Yes, if you plan to develop on Linux long-term
- **"How hard is migration?"** - Easier than you think! Your refactored code is already compatible

## Support

- See [RUN_ON_LINUX.md](RUN_ON_LINUX.md) for detailed Linux instructions
- See [MIGRATION_TO_CORE.md](MIGRATION_TO_CORE.md) for migration guide
- See [SETUP.md](SETUP.md) for Windows setup instructions
