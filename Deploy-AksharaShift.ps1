#!/usr/bin/env powershell
<#
.SYNOPSIS
    AksharaShift Deployment Script
    
.DESCRIPTION
    Complete deployment solution for AksharaShift application
    
.PARAMETER Action
    Deploy, Start, Stop, EnableAutoStart, DisableAutoStart, Uninstall
    
.EXAMPLE
    .\Deploy-AksharaShift.ps1 -Action Deploy -InstallPath "C:\Program Files\AksharaShift"
    .\Deploy-AksharaShift.ps1 -Action Start
    .\Deploy-AksharaShift.ps1 -Action EnableAutoStart
#>

param(
    [Parameter(Mandatory=$true)]
    [ValidateSet('Deploy', 'Start', 'Stop', 'EnableAutoStart', 'DisableAutoStart', 'Uninstall', 'Check')]
    [string]$Action,
    
    [Parameter(Mandatory=$false)]
    [string]$InstallPath = "C:\Program Files\AksharaShift",
    
    [Parameter(Mandatory=$false)]
    [string]$SourcePath = "d:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\"
)

# Admin rights check
function Ensure-AdminRights {
    $isAdmin = [bool]([System.Security.Principal.WindowsIdentity]::GetCurrent().Groups -match "S-1-5-32-544")
    if (-not $isAdmin) {
        Write-Warning "This action requires administrator privileges."
        Write-Host "Please run PowerShell as Administrator."
        exit 1
    }
}

# Deploy function
function Deploy-Application {
    Write-Host "╔════════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
    Write-Host "║          Deploying AksharaShift Application                    ║" -ForegroundColor Cyan
    Write-Host "╚════════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
    
    Write-Host "`nSource:      $SourcePath"
    Write-Host "Destination: $InstallPath`n"
    
    # Check source
    if (-not (Test-Path $SourcePath)) {
        Write-Error "Source path not found: $SourcePath"
        return $false
    }
    
    # Create installation directory
    Write-Host "[1/4] Creating installation directory..." -ForegroundColor Yellow
    if (-not (Test-Path $InstallPath)) {
        New-Item -ItemType Directory -Path $InstallPath -Force | Out-Null
        Write-Host "✓ Directory created" -ForegroundColor Green
    }
    
    # Copy files
    Write-Host "[2/4] Copying application files..." -ForegroundColor Yellow
    try {
        Copy-Item "$SourcePath*" -Destination $InstallPath -Recurse -Force
        Write-Host "✓ Files copied" -ForegroundColor Green
    } catch {
        Write-Error "Failed to copy files: $_"
        return $false
    }
    
    # Create Start Menu shortcut
    Write-Host "[3/4] Creating Start Menu shortcut..." -ForegroundColor Yellow
    try {
        $StartMenuPath = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs"
        $WshShell = New-Object -ComObject WScript.Shell
        $Shortcut = $WshShell.CreateShortcut("$StartMenuPath\AksharaShift.lnk")
        $Shortcut.TargetPath = "$InstallPath\AksharaShift.exe"
        $Shortcut.WorkingDirectory = $InstallPath
        $Shortcut.Description = "Malayalam Unicode to Legacy Font Converter"
        $Shortcut.Save()
        Write-Host "✓ Shortcut created" -ForegroundColor Green
    } catch {
        Write-Warning "Failed to create shortcut: $_"
    }
    
    # Verify installation
    Write-Host "[4/4] Verifying installation..." -ForegroundColor Yellow
    $exePath = "$InstallPath\AksharaShift.exe"
    if (Test-Path $exePath) {
        $size = (Get-Item $exePath).Length / 1KB
        Write-Host "✓ Installation verified" -ForegroundColor Green
        Write-Host "`nApplication installed at: $exePath"
        Write-Host "Executable size: $([math]::Round($size, 1)) KB`n"
        return $true
    } else {
        Write-Error "Executable not found at $exePath"
        return $false
    }
}

# Start application
function Start-Application {
    Write-Host "Starting AksharaShift..." -ForegroundColor Yellow
    
    $exePath = "$InstallPath\AksharaShift.exe"
    if (-not (Test-Path $exePath)) {
        Write-Error "Application not found at $exePath`nPlease run Deploy action first."
        return $false
    }
    
    try {
        Start-Process -FilePath $exePath
        Write-Host "✓ Application started" -ForegroundColor Green
        return $true
    } catch {
        Write-Error "Failed to start application: $_"
        return $false
    }
}

# Stop application
function Stop-Application {
    Write-Host "Stopping AksharaShift..." -ForegroundColor Yellow
    
    try {
        Get-Process AksharaShift -ErrorAction Stop | Stop-Process -Force
        Write-Host "✓ Application stopped" -ForegroundColor Green
        return $true
    } catch {
        Write-Warning "Application not currently running"
        return $true
    }
}

# Enable auto-start
function Enable-AutoStart {
    Ensure-AdminRights
    
    Write-Host "Enabling auto-start..." -ForegroundColor Yellow
    
    $regPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
    $exePath = "$InstallPath\AksharaShift.exe"
    
    if (-not (Test-Path $exePath)) {
        Write-Error "Application not found at $exePath"
        return $false
    }
    
    try {
        New-ItemProperty -Path $regPath -Name "AksharaShift" -Value $exePath -Force | Out-Null
        Write-Host "✓ Auto-start enabled" -ForegroundColor Green
        Write-Host "  Registry path: $regPath"
        Write-Host "  Application will start on next boot`n"
        return $true
    } catch {
        Write-Error "Failed to enable auto-start: $_"
        return $false
    }
}

# Disable auto-start
function Disable-AutoStart {
    Ensure-AdminRights
    
    Write-Host "Disabling auto-start..." -ForegroundColor Yellow
    
    $regPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
    
    try {
        Remove-ItemProperty -Path $regPath -Name "AksharaShift" -Force -ErrorAction SilentlyContinue
        Write-Host "✓ Auto-start disabled" -ForegroundColor Green
        Write-Host "  Application will not start on boot`n"
        return $true
    } catch {
        Write-Error "Failed to disable auto-start: $_"
        return $false
    }
}

# Uninstall
function Uninstall-Application {
    Ensure-AdminRights
    
    Write-Host "╔════════════════════════════════════════════════════════════════╗" -ForegroundColor Red
    Write-Host "║          Uninstalling AksharaShift Application                 ║" -ForegroundColor Red
    Write-Host "╚════════════════════════════════════════════════════════════════╝" -ForegroundColor Red
    
    # Stop application
    Stop-Application | Out-Null
    
    # Disable auto-start
    Write-Host "`nDisabling auto-start..." -ForegroundColor Yellow
    $regPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
    Remove-ItemProperty -Path $regPath -Name "AksharaShift" -Force -ErrorAction SilentlyContinue
    
    # Remove Start Menu shortcut
    Write-Host "Removing Start Menu shortcut..." -ForegroundColor Yellow
    $shortcutPath = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\AksharaShift.lnk"
    if (Test-Path $shortcutPath) {
        Remove-Item $shortcutPath -Force
    }
    
    # Remove installation directory
    Write-Host "Removing application files..." -ForegroundColor Yellow
    if (Test-Path $InstallPath) {
        Remove-Item $InstallPath -Recurse -Force
        Write-Host "✓ Application uninstalled" -ForegroundColor Green
    }
    
    Write-Host "`nAksharaShift has been completely removed from your system.`n"
    return $true
}

# Check installation
function Check-Installation {
    Write-Host "╔════════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
    Write-Host "║          Checking AksharaShift Installation                    ║" -ForegroundColor Cyan
    Write-Host "╚════════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
    
    $exePath = "$InstallPath\AksharaShift.exe"
    $regPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
    
    Write-Host "`nInstall Location: $InstallPath"
    
    # Check exe
    if (Test-Path $exePath) {
        $size = (Get-Item $exePath).Length / 1KB
        $version = (Get-Item $exePath).VersionInfo.FileVersion
        Write-Host "✓ Executable found ($('{0:N1}' -f $size) KB)" -ForegroundColor Green
    } else {
        Write-Host "✗ Executable not found" -ForegroundColor Red
    }
    
    # Check auto-start
    $autoStartValue = Get-ItemProperty -Path $regPath -Name "AksharaShift" -ErrorAction SilentlyContinue
    if ($autoStartValue) {
        Write-Host "✓ Auto-start enabled" -ForegroundColor Green
    } else {
        Write-Host "✗ Auto-start disabled" -ForegroundColor Yellow
    }
    
    # Check process
    $process = Get-Process AksharaShift -ErrorAction SilentlyContinue
    if ($process) {
        Write-Host "✓ Application is running" -ForegroundColor Green
        Write-Host "  PID: $($process.Id)" -ForegroundColor Gray
    } else {
        Write-Host "✗ Application is not running" -ForegroundColor Yellow
    }
    
    Write-Host ""
    return $true
}

# Main execution
switch ($Action) {
    'Deploy' {
        Ensure-AdminRights
        Deploy-Application
    }
    'Start' {
        Start-Application
    }
    'Stop' {
        Stop-Application
    }
    'EnableAutoStart' {
        Enable-AutoStart
    }
    'DisableAutoStart' {
        Disable-AutoStart
    }
    'Uninstall' {
        Uninstall-Application
    }
    'Check' {
        Check-Installation
    }
}
