@echo off
REM AksharaShift - Start Application
REM This batch file launches the AksharaShift application

setlocal enabledelayedexpansion

set "APP_DIR=d:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows"
set "APP_EXE=!APP_DIR!\AksharaShift.exe"

echo.
echo ==========================================
echo AksharaShift - Malayalam Text Converter
echo ==========================================
echo.

if exist "!APP_EXE!" (
    echo [OK] Found application at: !APP_EXE!
    echo.
    echo Starting AksharaShift...
    echo.
    
    REM Start the application
    start "" "!APP_EXE!"
    
    echo [OK] Application launched in background
    echo.
    echo Tip: Look for the AksharaShift icon in the system tray
    echo       Right-click for options: Enable/Disable and Exit
    echo.
) else (
    echo [ERROR] Application not found at: !APP_EXE!
    echo.
    echo Please build the application first using:
    echo   cd d:\Projects\AksharaShift\AksharaShift
    echo   dotnet build -c Release
    echo.
)

echo ==========================================
echo.

endlocal
pause
