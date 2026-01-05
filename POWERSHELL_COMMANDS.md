# AksharaShift - Complete Setup and PowerShell Command Reference

## Project Overview

**Application**: AksharaShift - Malayalam Unicode to Legacy Font Converter  
**Version**: 1.0  
**Framework**: .NET 8.0 with WPF and Windows Forms  
**Platform**: Windows 10/11  
**Location**: `d:\Projects\AksharaShift`

## Quick Start Commands

### 1. Navigate to Project

```powershell
cd d:\Projects\AksharaShift\AksharaShift
```

### 2. Build the Application

```powershell
# Build in Release mode (optimized for production)
dotnet build -c Release

# Build in Debug mode (with debugging symbols)
dotnet build -c Debug
```

### 3. Run the Application

```powershell
# Run directly from project
dotnet run -c Release

# Or run the compiled executable
.\bin\Release\net8.0-windows\AksharaShift.exe

# Run minimized (for auto-start)
.\bin\Release\net8.0-windows\AksharaShift.exe --minimized
```

### 4. Auto-Start Configuration

```powershell
# Navigate to project root
cd d:\Projects\AksharaShift

# Check auto-start status
.\Setup-AutoStart.ps1 -Action Status

# Enable auto-start on boot
.\Setup-AutoStart.ps1 -Action Enable

# Disable auto-start
.\Setup-AutoStart.ps1 -Action Disable

# Full install (build + enable auto-start)
.\Setup-AutoStart.ps1 -Action Install
```

## Complete Build Workflow

### Step 1: Clean Previous Builds

```powershell
# Navigate to project
cd d:\Projects\AksharaShift\AksharaShift

# Clean build artifacts
dotnet clean

# Remove bin and obj directories
Remove-Item bin -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item obj -Recurse -Force -ErrorAction SilentlyContinue
```

### Step 2: Restore Dependencies

```powershell
# Restore NuGet packages
dotnet restore
```

### Step 3: Build Release Version

```powershell
# Full build with verbose output
dotnet build -c Release -v detailed

# Or just build
dotnet build -c Release
```

### Step 4: Verify Build

```powershell
# Check if executable exists and get info
$exePath = '.\bin\Release\net8.0-windows\AksharaShift.exe'
if (Test-Path $exePath) {
    Get-Item $exePath | Format-List Name, Length, LastWriteTime
    Write-Host "Build successful!" -ForegroundColor Green
} else {
    Write-Host "Build failed - executable not found" -ForegroundColor Red
}
```

## Project Structure

```
d:\Projects\AksharaShift\
├── AksharaShift\                          # Main project folder
│   ├── bin\
│   │   └── Release\net8.0-windows\        # Compiled binaries
│   │       └── AksharaShift.exe          # Main executable
│   ├── App.xaml                           # WPF Application definition
│   ├── App.xaml.cs                        # App startup logic
│   ├── MainWindow.xaml                    # Hidden background window
│   ├── MainWindow.xaml.cs                 # Window and hook management
│   ├── KeyboardHook.cs                    # Global keyboard interception
│   ├── KeyboardLayoutDetector.cs          # Keyboard layout detection
│   ├── MalayalamTextConverter.cs          # Conversion engine
│   ├── TextProcessor.cs                   # Clipboard and text operations
│   ├── AutoStartManager.cs                # Registry-based auto-start
│   └── AksharaShift.csproj               # Project file
├── README.md                              # Main documentation
├── TESTING.md                             # Testing guide
├── CHARACTER_MAPPING_REFERENCE.md         # Character mapping tables
├── POWERSHELL_COMMANDS.md                 # This file
├── Setup-AutoStart.ps1                    # Auto-start configuration script
└── Start-AksharaShift.bat                # Batch file launcher
```

## File Descriptions

### Source Code Files

| File | Purpose |
|------|---------|
| `KeyboardHook.cs` | Global keyboard hook implementation using Windows API |
| `KeyboardLayoutDetector.cs` | Detects current keyboard layout (Malayalam Phonetic) |
| `MalayalamTextConverter.cs` | Core conversion engine with ML/FML mappings |
| `TextProcessor.cs` | Clipboard operations and text replacement |
| `AutoStartManager.cs` | Manages Windows Registry for auto-start |
| `MainWindow.xaml/cs` | Application window and event routing |
| `App.xaml/cs` | WPF application initialization |

### Configuration Files

| File | Purpose |
|------|---------|
| `AksharaShift.csproj` | Project configuration and dependencies |

### Documentation

| File | Purpose |
|------|---------|
| `README.md` | Complete user and developer documentation |
| `TESTING.md` | Comprehensive testing guide with test cases |
| `CHARACTER_MAPPING_REFERENCE.md` | Complete Unicode to Legacy character mappings |
| `POWERSHELL_COMMANDS.md` | PowerShell command reference (this file) |

### Helper Scripts

| File | Purpose |
|------|---------|
| `Setup-AutoStart.ps1` | Configure auto-start via Registry |
| `Start-AksharaShift.bat` | Batch file to launch application |

## Development Environment Setup

### Install .NET 8 SDK

```powershell
# Check if .NET 8 is installed
dotnet --version

# Download and install from: https://dotnet.microsoft.com/download
# Or using Chocolatey:
choco install dotnet-sdk-8.0

# Or using Windows Package Manager:
winget install Microsoft.DotNet.SDK.8
```

### Install VS Code

```powershell
# Using Chocolatey
choco install vscode

# Using Windows Package Manager
winget install Microsoft.VisualStudioCode

# Or download from: https://code.visualstudio.com
```

### Recommended VS Code Extensions

```powershell
# C# extension
code --install-extension ms-dotnettools.csharp

# C# Dev Kit
code --install-extension ms-dotnettools.csdevkit

# PowerShell extension
code --install-extension ms-vscode.PowerShell

# Git extensions
code --install-extension eamodio.gitlens
code --install-extension GitHub.remotehub
```

## Running and Testing Commands

### Run Application (Multiple Methods)

```powershell
# Method 1: Direct executable
d:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe

# Method 2: Using batch file
d:\Projects\AksharaShift\Start-AksharaShift.bat

# Method 3: From PowerShell with parameters
& 'd:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe' --minimized
```

### Test Conversion (Standalone)

```powershell
# Create a test script to verify conversion
@'
using System;

namespace AksharaShift.Test {
    class Program {
        static void Main() {
            // Test ML conversion
            string inputText = "അഥര";  # Unicode Malayalam
            string mlOutput = MalayalamTextConverter.ConvertToML(inputText);
            string fmlOutput = MalayalamTextConverter.ConvertToFML(inputText);
            
            Console.WriteLine($"Input:  {inputText}");
            Console.WriteLine($"ML:     {mlOutput}");
            Console.WriteLine($"FML:    {fmlOutput}");
        }
    }
}
'@ | Out-File test.cs

# Compile and run test
csc.exe test.cs
.\test.exe
```

## Registry Operations

### Check Auto-Start Status

```powershell
# View all startup programs
Get-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"

# Check specific application
Get-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run" -Name "AksharaShift" -ErrorAction SilentlyContinue
```

### Manual Registry Management

```powershell
# Enable auto-start manually
$regPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
$appPath = 'd:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe'
New-ItemProperty -Path $regPath -Name "AksharaShift" -Value "`"$appPath`" --minimized" -PropertyType String -Force

# Disable auto-start manually
Remove-ItemProperty -Path $regPath -Name "AksharaShift" -Force

# Export registry for backup
Export-Registry -Path $regPath -OutFile backup.reg

# Clear all startup entries for fresh start
Get-ItemProperty -Path $regPath | foreach { Remove-ItemProperty -Path $regPath -Name $_.PSChildName -ErrorAction SilentlyContinue }
```

## Debugging and Troubleshooting Commands

### Enable Debug Output

```powershell
# Set environment variable for verbose output
$env:DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

# Set debug output
$DebugPreference = "Continue"
```

### Check Application Processes

```powershell
# Find running AksharaShift instances
Get-Process | Where-Object { $_.ProcessName -like "*AksharaShift*" }

# Get detailed process information
Get-Process | Where-Object { $_.ProcessName -like "*AksharaShift*" } | Select-Object *

# Kill application if hung
Stop-Process -Name "AksharaShift" -Force
```

### Monitor Log Output

```powershell
# Get Windows Event Viewer logs for application
Get-EventLog -LogName Application -Source "AksharaShift" -Newest 10

# View system logs
Get-EventLog -LogName System -Source "Service Control Manager" -Newest 10
```

### Performance Monitoring

```powershell
# Monitor real-time process resources
Get-Process | Where-Object { $_.ProcessName -like "*AksharaShift*" } | 
    foreach { 
        Write-Host "Process: $($_.ProcessName)"
        Write-Host "Memory: $($_.WorkingSet / 1MB) MB"
        Write-Host "CPU: $($_.CPU) s"
        Write-Host "Threads: $($_.Threads.Count)"
        Write-Host "---"
    }

# Get memory usage over time
$process = Get-Process | Where-Object { $_.ProcessName -like "*AksharaShift*" }
for ($i = 0; $i -lt 5; $i++) {
    "{0}: {1} MB" -f (Get-Date -Format HH:mm:ss), ($process.WorkingSet / 1MB)
    Start-Sleep -Seconds 1
}
```

## Diagnostic Commands

### System Information

```powershell
# Get Windows version
[System.Environment]::OSVersion

# Get .NET version
dotnet --info

# Get keyboard layouts
Get-WinUserLanguageList

# Check Malayalam Phonetic keyboard
Get-WinUserLanguageList | Where-Object { $_.LanguageTag -eq "ml-IN" }
```

### Port and Network

```powershell
# Check if any ports are bound (application doesn't use network, but for testing)
netstat -ano | findstr LISTENING

# Check DNS resolution (if needed)
Resolve-DnsName -Name "unicode.org"
```

## Package Management Commands

### NuGet Package Information

```powershell
# List project dependencies
dotnet list package

# Check for outdated packages
dotnet list package --outdated

# Update specific package
dotnet add package PackageName --version VersionNumber

# Remove package
dotnet remove package PackageName
```

## Deployment Commands

### Create Release Package

```powershell
# Publish application (self-contained)
dotnet publish -c Release -r win-x64 --self-contained

# Publish as framework-dependent
dotnet publish -c Release

# Output structure
# bin/Release/net8.0-windows/publish/ contains all required files
```

### Create Installer (Optional)

```powershell
# For more complex installations, create a batch installer
$setupScript = @'
@echo off
echo Installing AksharaShift...
xcopy /Y /E "%~dp0bin\Release\net8.0-windows\*" "%ProgramFiles%\AksharaShift\"
reg add "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run" /v AksharaShift /t REG_SZ /d "%ProgramFiles%\AksharaShift\AksharaShift.exe" /f
echo Installation complete!
pause
'@

$setupScript | Out-File -FilePath "install.bat"
```

## Uninstall Commands

### Remove Application

```powershell
# Remove from auto-start
Remove-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run" -Name "AksharaShift" -Force -ErrorAction SilentlyContinue

# Remove installation directory
Remove-Item -Path "d:\Projects\AksharaShift" -Recurse -Force -ErrorAction SilentlyContinue

# Verify removal
if (Test-Path "d:\Projects\AksharaShift") {
    Write-Host "Removal incomplete" -ForegroundColor Red
} else {
    Write-Host "Application removed successfully" -ForegroundColor Green
}
```

## Useful Aliases and Functions

### Create Custom Functions

```powershell
# Function to quickly build and run
function Build-AndRun {
    param(
        [ValidateSet("Debug", "Release")]
        [string]$Configuration = "Release"
    )
    
    cd d:\Projects\AksharaShift\AksharaShift
    Write-Host "Building in $Configuration mode..." -ForegroundColor Cyan
    dotnet build -c $Configuration
    
    $exePath = ".\bin\$Configuration\net8.0-windows\AksharaShift.exe"
    if (Test-Path $exePath) {
        Write-Host "Running application..." -ForegroundColor Green
        & $exePath
    } else {
        Write-Host "Build failed" -ForegroundColor Red
    }
}

# Function to check status
function Get-AppStatus {
    Write-Host "=== AksharaShift Status ===" -ForegroundColor Cyan
    
    # Check process
    $process = Get-Process | Where-Object { $_.ProcessName -like "*AksharaShift*" }
    if ($process) {
        Write-Host "✓ Application running" -ForegroundColor Green
        Write-Host "  Memory: $($process.WorkingSet / 1MB) MB"
    } else {
        Write-Host "✗ Application not running" -ForegroundColor Yellow
    }
    
    # Check auto-start
    $autoStart = Get-ItemProperty -Path "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run" -Name "AksharaShift" -ErrorAction SilentlyContinue
    if ($autoStart) {
        Write-Host "✓ Auto-start enabled" -ForegroundColor Green
    } else {
        Write-Host "✗ Auto-start disabled" -ForegroundColor Yellow
    }
    
    # Check keyboard
    $malKb = Get-WinUserLanguageList | Where-Object { $_.LanguageTag -eq "ml-IN" }
    if ($malKb) {
        Write-Host "✓ Malayalam keyboard available" -ForegroundColor Green
    } else {
        Write-Host "✗ Malayalam keyboard not installed" -ForegroundColor Yellow
    }
}

# Add to PowerShell profile for persistence
# Add above functions to your profile: $PROFILE
```

## Useful One-Liners

```powershell
# Quick build
cd d:\Projects\AksharaShift\AksharaShift; dotnet build -c Release

# Quick run
& 'd:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe'

# Kill hanging process
Stop-Process -Name "AksharaShift" -Force

# Check if running
Get-Process -Name "AksharaShift" -ErrorAction SilentlyContinue | Measure-Object | Select-Object -ExpandProperty Count

# Get executable size
(Get-Item 'd:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe').Length / 1MB

# Create zip for distribution
Compress-Archive -Path 'd:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows' -DestinationPath 'AksharaShift_Release.zip' -Force
```

## Environment Variables

### Useful Environment Variables

```powershell
# Set for production
$env:ASPNETCORE_ENVIRONMENT = "Production"

# Disable telemetry
$env:DOTNET_CLI_TELEMETRY_OPTOUT = "1"

# Set application path
$env:AKSHARASHIFT_PATH = "d:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows"

# Verify
Write-Host $env:AKSHARASHIFT_PATH
```

## Batch File Commands

### Create Launcher Batch

```batch
@echo off
cd /d d:\Projects\AksharaShift\AksharaShift
dotnet build -c Release
if errorlevel 1 (
    echo Build failed!
    pause
    exit /b 1
)
echo Build successful. Starting application...
start "" bin\Release\net8.0-windows\AksharaShift.exe
```

## Version and Release Management

### Version Update Workflow

```powershell
# Update version in project file
$projFile = 'd:\Projects\AksharaShift\AksharaShift\AksharaShift.csproj'
[xml]$xml = Get-Content $projFile
$xml.Project.PropertyGroup.Version = "1.1.0"
$xml.Save($projFile)

# Build new version
cd d:\Projects\AksharaShift\AksharaShift
dotnet build -c Release

# Create release tag
git tag -a "v1.1.0" -m "Release version 1.1.0"
```

## Summary of Key Commands

| Task | Command |
|------|---------|
| Build | `dotnet build -c Release` |
| Run | `.\bin\Release\net8.0-windows\AksharaShift.exe` |
| Enable Auto-Start | `.\Setup-AutoStart.ps1 -Action Enable` |
| Check Status | `.\Setup-AutoStart.ps1 -Action Status` |
| Clean | `dotnet clean` |
| Publish | `dotnet publish -c Release` |
| Restore | `dotnet restore` |

---

**Document Version**: 1.0  
**Last Updated**: January 5, 2026  
**Application**: AksharaShift v1.0  
**Commands Reference**: Complete PowerShell and command-line reference
