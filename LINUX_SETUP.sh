#!/bin/bash

# ASP.NET Web Forms on Linux Setup Script
# This script attempts to set up Mono to run the Web Forms application

echo "=========================================="
echo "ASP.NET Web Forms Linux Setup"
echo "=========================================="
echo ""

# Check if running on Linux
if [[ "$OSTYPE" != "linux-gnu"* ]]; then
    echo "❌ This script is for Linux only"
    exit 1
fi

echo "⚠️  WARNING: ASP.NET Web Forms has limited support on Linux"
echo "   Consider migrating to ASP.NET Core for better Linux support"
echo ""
read -p "Continue with Mono installation? (y/n) " -n 1 -r
echo ""

if [[ ! $REPLY =~ ^[Yy]$ ]]; then
    echo "Installation cancelled"
    exit 0
fi

# Update package list
echo "📦 Updating package list..."
sudo apt update

# Install Mono
echo "📦 Installing Mono..."
sudo apt install -y mono-complete mono-xsp4

# Verify installation
echo ""
echo "✅ Checking installation..."
mono --version
echo ""
xsp4 --version

echo ""
echo "=========================================="
echo "Installation Complete!"
echo "=========================================="
echo ""
echo "To run the application:"
echo "  cd \"$PWD\""
echo "  xsp4 --port 8080"
echo ""
echo "Then open: http://localhost:8080"
echo ""
echo "⚠️  Known Issues:"
echo "  - Some ASP.NET features may not work"
echo "  - SQL Server connectivity may be problematic"
echo "  - Performance may be slower than Windows/IIS"
echo ""
echo "💡 For production use, consider:"
echo "  - Migrating to ASP.NET Core (see MIGRATION_TO_CORE.md)"
echo "  - Running on Windows with IIS"
echo "  - Using a Windows VM"
echo ""
