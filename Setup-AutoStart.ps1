#!/usr/bin/env powershell
<#
.SYNOPSIS
AksharaShift Auto-Start Configuration Script

.DESCRIPTION
Enables or disables automatic startup of AksharaShift application on Windows boot.
Provides additional configuration and status checking capabilities.

.PARAMETER Action
The action to perform: Enable, Disable, or Status

.EXAMPLE
.\Setup-AutoStart.ps1 -Action Enable
.\Setup-AutoStart.ps1 -Action Status

.NOTES
Requires local user permissions (no admin elevation needed for user registry)
#>

param(
    [Parameter(Mandatory=$false)]
    [ValidateSet('Enable', 'Disable', 'Status', 'Install')]
    [string]$Action = 'Status'
)

# Configuration
$AppName = 'AksharaShift'
$RegistryPath = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Run'
$ExecutablePath = 'd:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe'

Write-Host "=====================================`n" -ForegroundColor Cyan
Write-Host "AksharaShift Auto-Start Configuration" -ForegroundColor Yellow
Write-Host "=====================================`n" -ForegroundColor Cyan

# Function to enable auto-start
function Enable-AutoStart {
    try {
        if (-not (Test-Path -Path $ExecutablePath)) {
            Write-Host "ERROR: Executable not found at: $ExecutablePath" -ForegroundColor Red
            Write-Host "Please build the project first using: dotnet build -c Release" -ForegroundColor Yellow
            return $false
        }

        Write-Host "Enabling auto-start for $AppName..." -ForegroundColor Green
        
        # Add registry entry
        New-ItemProperty -Path $RegistryPath -Name $AppName -Value """$ExecutablePath"" --minimized" -PropertyType String -Force | Out-Null
        
        Write-Host "✓ Auto-start enabled successfully!" -ForegroundColor Green
        Write-Host "  Application will launch on next Windows restart" -ForegroundColor Green
        Write-Host "  Registry Key: $RegistryPath\$AppName" -ForegroundColor Cyan
        return $true
    }
    catch {
        Write-Host "ERROR: Failed to enable auto-start" -ForegroundColor Red
        Write-Host "Message: $_" -ForegroundColor Red
        return $false
    }
}

# Function to disable auto-start
function Disable-AutoStart {
    try {
        Write-Host "Disabling auto-start for $AppName..." -ForegroundColor Green
        
        # Remove registry entry
        Remove-ItemProperty -Path $RegistryPath -Name $AppName -Force -ErrorAction SilentlyContinue
        
        Write-Host "✓ Auto-start disabled successfully!" -ForegroundColor Green
        Write-Host "  Application will no longer launch automatically" -ForegroundColor Green
        return $true
    }
    catch {
        Write-Host "ERROR: Failed to disable auto-start" -ForegroundColor Red
        Write-Host "Message: $_" -ForegroundColor Red
        return $false
    }
}

# Function to check auto-start status
function Get-AutoStartStatus {
    try {
        Write-Host "Checking auto-start status..." -ForegroundColor Green
        Write-Host ""
        
        # Get registry value
        $regValue = Get-ItemProperty -Path $RegistryPath -Name $AppName -ErrorAction SilentlyContinue
        
        if ($regValue) {
            Write-Host "Status: ENABLED" -ForegroundColor Green
            Write-Host "Registry Value: $($regValue.$AppName)" -ForegroundColor Cyan
        } else {
            Write-Host "Status: DISABLED" -ForegroundColor Yellow
        }
        
        Write-Host ""
        Write-Host "Executable Path: $ExecutablePath" -ForegroundColor Cyan
        if (Test-Path -Path $ExecutablePath) {
            Write-Host "Executable: FOUND ✓" -ForegroundColor Green
            $fileInfo = Get-Item -Path $ExecutablePath
            Write-Host "File Size: $($fileInfo.Length / 1MB -as [int]) MB" -ForegroundColor Cyan
            Write-Host "Last Modified: $($fileInfo.LastWriteTime)" -ForegroundColor Cyan
        } else {
            Write-Host "Executable: NOT FOUND ✗" -ForegroundColor Red
        }
        
        Write-Host ""
        return $true
    }
    catch {
        Write-Host "ERROR: Failed to check status" -ForegroundColor Red
        Write-Host "Message: $_" -ForegroundColor Red
        return $false
    }
}

# Function to install and enable auto-start
function Install-AutoStart {
    Write-Host "Installation Mode" -ForegroundColor Yellow
    Write-Host ""
    
    # Check if executable exists and build if needed
    if (-not (Test-Path -Path $ExecutablePath)) {
        Write-Host "Executable not found. Building the project..." -ForegroundColor Yellow
        
        $projectPath = 'd:\Projects\AksharaShift\AksharaShift'
        if (Test-Path -Path $projectPath) {
            Push-Location $projectPath
            Write-Host "Running: dotnet build -c Release" -ForegroundColor Cyan
            dotnet build -c Release
            Pop-Location
            
            if (-not (Test-Path -Path $ExecutablePath)) {
                Write-Host "Build failed. Cannot find executable." -ForegroundColor Red
                return $false
            }
        } else {
            Write-Host "Project path not found: $projectPath" -ForegroundColor Red
            return $false
        }
    }
    
    # Enable auto-start
    return Enable-AutoStart
}

# Main logic
Write-Host "Action: $Action`n" -ForegroundColor Cyan

switch ($Action.ToLower()) {
    'enable' {
        Enable-AutoStart
    }
    'disable' {
        Disable-AutoStart
    }
    'status' {
        Get-AutoStartStatus
    }
    'install' {
        Install-AutoStart
    }
    default {
        Get-AutoStartStatus
    }
}

Write-Host "`n=====================================`n" -ForegroundColor Cyan
