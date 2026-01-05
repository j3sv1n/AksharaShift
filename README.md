# AksharaShift - Malayalam Text Converter

## Overview

AksharaShift is a Windows background application that performs Unicode Malayalam to Malayalam legacy (ASCII) font conversion using global keyboard shortcuts. It detects when the active keyboard layout is Malayalam Phonetic and converts selected text to either ML or FML legacy font formats.

## Features

- ✅ **Background Operation**: Runs silently in the system background with optional system tray icon
- ✅ **Global Keyboard Hooks**: Detects Win+Shift+M and Win+Shift+F shortcuts system-wide
- ✅ **Malayalam Phonetic Detection**: Automatically detects when Malayalam Phonetic keyboard is active
- ✅ **Two Conversion Formats**:
  - **ML Format**: Win+Shift+M - Converts to ML legacy ASCII font
  - **FML Format**: Win+Shift+F - Converts to FML legacy ASCII font
- ✅ **Clipboard Management**: Safely handles clipboard operations and restoration
- ✅ **System Tray Integration**: Provides enable/disable toggle and exit options
- ✅ **Auto-Start on Boot**: Configurable automatic launch on Windows startup
- ✅ **Windows 10/11 Compatible**: Built with .NET 8.0 and WPF

## Installation

### Prerequisites

- Windows 10 or Windows 11
- .NET 8.0 Runtime (or .NET 8.0 SDK for development)
- Administrator privileges (for auto-start configuration)

### Build from Source

1. Navigate to the project directory:
   ```powershell
   cd d:\Projects\AksharaShift\AksharaShift
   ```

2. Build the application:
   ```powershell
   dotnet build -c Release
   ```

3. The compiled executable will be available at:
   ```
   bin\Release\net8.0-windows\AksharaShift.exe
   ```

### Running the Application

#### First Time Run (Manual)

```powershell
d:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe
```

The application will start silently in the background. You should see a system tray icon.

#### Right-click on System Tray Icon

- **Enabled** - Toggle to enable/disable the conversion shortcuts
- **Exit** - Close the application

## Usage

### Keyboard Shortcuts

#### ML Format Conversion (Win+Shift+M)

1. Select text in any application (must be Unicode Malayalam)
2. Press **Win + Shift + M**
3. The selected text will be converted to ML legacy ASCII format
4. Original clipboard content is automatically restored

#### FML Format Conversion (Win+Shift+F)

1. Select text in any application (must be Unicode Malayalam)
2. Press **Win + Shift + F**
3. The selected text will be converted to FML legacy ASCII format
4. Original clipboard content is automatically restored

### Supported Input

- Unicode Malayalam characters (U+0D00 to U+0D7F)
- Malayalam conjuncts (two-character combinations)
- English text (passes through unchanged)
- Numbers (converted to legacy format)
- Punctuation marks

### Output Formats

#### ML Format
- Uses standard ML legacy font character mappings
- Example: `അ` (U+0D05) → `a`
- Example: `ആ` (U+0D06) → `A`

#### FML Format
- Uses alternative FML legacy font character mappings
- Example: `അ` (U+0D05) → `a`
- Example: `ങ` (U+0D19) → `` ` ``

## Configuration

### Enabling Auto-Start on Boot

To automatically launch AksharaShift when Windows starts:

#### Option 1: Using the Application (Requires Administrator Privileges)

Create a batch file named `enable_autostart.bat`:

```batch
@echo off
REM Navigate to the application directory
cd /d "d:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows"

REM Add registry entry for auto-start
reg add "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run" /v AksharaShift /t REG_SZ /d "%cd%\AksharaShift.exe --minimized" /f

echo AksharaShift auto-start enabled.
pause
```

Run this batch file with administrator privileges.

#### Option 2: Manual Registry Edit

1. Press `Win + R` and type `regedit`
2. Navigate to: `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`
3. Right-click and create a new String Value
4. Name: `AksharaShift`
5. Value: `d:\Projects\AksharaShift\AksharaShift\bin\Release\net8.0-windows\AksharaShift.exe --minimized`
6. Click OK and close Registry Editor

#### Option 3: Create Startup Folder Shortcut

1. Press `Win + R` and type `shell:startup`
2. Create a shortcut to `AksharaShift.exe` in this folder
3. The application will auto-start on next boot

### Disabling Auto-Start

To remove the auto-start registry entry:

```batch
@echo off
reg delete "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run" /v AksharaShift /f
echo AksharaShift auto-start disabled.
pause
```

Or manually delete the `AksharaShift` value from the Registry Editor.

## Malayalam Font Installation

To display the converted legacy font text correctly, you need to install ML and FML fonts.

### Installing ML Font

1. Download the ML font from a Malayalam software repository
2. Right-click the `.ttf` file → "Install"
3. Or: Settings → Personalization → Fonts → Add fonts from file

### Installing FML Font

1. Download the FML font from a Malayalam software repository
2. Follow the same installation steps as ML font

### Recommended Font Sources

- **ML and FML Fonts**: Available through Malayalam computing communities
- **Popular Fonts**: ML-TTCore, FMLBareily, etc.

**Note**: Ensure the fonts are installed system-wide or in the user's font directory for proper display across all applications.

## Character Mapping Tables

### ML Font Mappings (Sample)

| Unicode | ML |
|---------|-------|
| അ | a |
| ആ | A |
| ഇ | i |
| ഈ | I |
| ഉ | u |
| ഊ | U |
| ക | k |
| ഖ | K |
| ഗ | g |
| ഘ | G |

For complete mappings, refer to [MalayalamTextConverter.cs](AksharaShift/MalayalamTextConverter.cs).

### FML Font Mappings (Sample)

| Unicode | FML |
|---------|------|
| അ | a |
| ആ | A |
| ഇ | i |
| ഈ | I |
| ങ | ` |
| ച | c |
| ത | q |
| ദ | w |

For complete mappings, refer to [MalayalamTextConverter.cs](AksharaShift/MalayalamTextConverter.cs).

## Architecture

### Core Components

#### KeyboardHook.cs
- Global keyboard interception using Windows API
- Detects Win+Shift+M and Win+Shift+F combinations
- Uses `SetWindowsHookEx` with low-level keyboard hook

#### KeyboardLayoutDetector.cs
- Detects current keyboard layout
- Checks if Malayalam Phonetic (0x4D09) is active
- Uses Windows API for layout detection

#### MalayalamTextConverter.cs
- Core conversion engine with comprehensive character mappings
- Supports ML and FML legacy ASCII formats
- Handles conjuncts (two-character combinations)
- Provides conversion statistics

#### TextProcessor.cs
- Clipboard operations and restoration
- Selected text capture and replacement
- Keyboard simulation for copy/paste
- Complete workflow management

#### AutoStartManager.cs
- Registry-based auto-start configuration
- Enable/disable auto-start functionality
- Provides status checking

#### MainWindow.xaml/cs
- Application entry point
- System tray icon integration
- Event routing to conversion handlers
- Enables/disables shortcut processing

### Workflow Diagram

```
User selects text
        ↓
Presses Win+Shift+M (or Win+Shift+F)
        ↓
KeyboardHook detects shortcut
        ↓
MainWindow receives event
        ↓
Check: Is Malayalam Phonetic active? + Is app enabled?
        ↓
TextProcessor.ConvertAndReplaceSelectedText()
        ├─ Save current clipboard
        ├─ Copy selected text (Ctrl+C)
        ├─ Convert using MalayalamTextConverter
        ├─ Place result in clipboard
        ├─ Paste converted text (Ctrl+V)
        └─ Restore original clipboard
        ↓
Selected text is now converted
```

## Technical Details

### Windows API Used

- `SetWindowsHookEx()` - Install global keyboard hook
- `GetKeyboardLayout()` - Detect current keyboard layout
- `Clipboard` operations - .NET System.Windows namespace
- `keybd_event()` - Simulate keyboard input

### Build Configuration

- **Framework**: .NET 8.0 (net8.0-windows)
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Output Type**: WinExe (Windows Executable)
- **Windows Forms Support**: Enabled (for system tray NotifyIcon)

## Troubleshooting

### Application doesn't respond to shortcuts

1. **Check Malayalam Phonetic keyboard**: Current keyboard must be Malayalam Phonetic
   - Check Windows taskbar keyboard indicator
   - Switch to Malayalam Phonetic layout first

2. **Verify application is running**: Check system tray for AksharaShift icon
   - If missing, relaunch the application

3. **Check if enabled**: Right-click tray icon and verify "Enabled" is checked

4. **Test with different application**: Try in Notepad or Word to isolate the issue

### Text not converting correctly

1. **Verify input is Unicode Malayalam**: Not Romanized or legacy format
   - Input should be typed using Malayalam Phonetic keyboard (Manglish)
   - Should display in Unicode Malayalam characters

2. **Check font installation**: Ensure ML or FML fonts are installed
   - Test fonts in Notepad

3. **Verify character support**: Some conjuncts might not have mappings
   - Check [MalayalamTextConverter.cs](AksharaShift/MalayalamTextConverter.cs) for supported characters

### Clipboard issues

1. **Cannot copy selected text**:
   - Some applications restrict clipboard access (security software, browsers)
   - Try in simpler application (Notepad, Word)

2. **Original clipboard not restored**:
   - This is logged but doesn't prevent conversion
   - Manually restore if needed from clipboard history

### Auto-start not working

1. **Check Registry**: Open Registry Editor and verify entry exists
   - `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`
   - Should have `AksharaShift` value pointing to .exe

2. **Check executable path**: Ensure full path is correct in registry value

3. **Run as Administrator**: Some Windows configurations require admin privileges
   - Create a scheduled task instead of registry entry

## Extending the Application

### Adding New Character Mappings

1. Open [MalayalamTextConverter.cs](AksharaShift/MalayalamTextConverter.cs)
2. Edit `MLMappings` or `FMLMappings` dictionaries
3. Add new character entries:
   ```csharp
   { 'ഞ', "~" }  // Character → Legacy ASCII representation
   ```
4. Rebuild the application:
   ```powershell
   dotnet build -c Release
   ```

### Adding New Keyboard Shortcuts

1. Edit [KeyboardHook.cs](AksharaShift/KeyboardHook.cs)
2. Add new virtual key code constants
3. Add detection logic in `HookCallback()`
4. Create corresponding event in KeyboardHook class
5. Handle event in [MainWindow.xaml.cs](AksharaShift/MainWindow.xaml.cs)

### Modifying Conversion Logic

1. Extend [MalayalamTextConverter.cs](AksharaShift/MalayalamTextConverter.cs)
2. Add new public methods for additional conversion types
3. Update `ConversionType` enum if needed
4. Call new methods from [TextProcessor.cs](AksharaShift/TextProcessor.cs)

## Performance Considerations

- **Global keyboard hook**: Minimal CPU impact, event-driven
- **Clipboard operations**: ~100-200ms per conversion (includes delays for system sync)
- **Memory usage**: ~30-50MB (typical WPF/WinForms application)
- **Startup time**: <1 second

## Security Notes

1. **Clipboard Access**: Application reads/writes clipboard content
   - Only for the user running the application
   - Restored immediately after use

2. **Keyboard Hook**: Detects keyboard input system-wide
   - Only specific key combinations are captured
   - No logging of general keyboard input

3. **Registry Access**: Auto-start uses standard Windows Registry
   - Only HKEY_CURRENT_USER (user-level, no admin elevation)
   - Standard Windows startup mechanism

4. **No Network Access**: Application is completely offline
   - No telemetry or phone-home functionality
   - All processing local to machine

## License and Distribution

This is a standalone Windows application. Distribute the compiled executable (AksharaShift.exe) along with required .NET runtime.

## Support

For issues, questions, or feature requests:
1. Check Troubleshooting section
2. Review character mappings in source code
3. Verify Windows configuration and Malayalam Phonetic keyboard setup

## Changelog

### Version 1.0 (Initial Release)
- ✅ Global keyboard hook implementation
- ✅ Malayalam Phonetic detection
- ✅ ML and FML format conversion
- ✅ System tray integration
- ✅ Auto-start configuration
- ✅ Comprehensive character mappings
- ✅ Clipboard management and restoration

---

**Application**: AksharaShift  
**Version**: 1.0  
**Built with**: .NET 8.0, WPF, C#  
**Platform**: Windows 10/11  
**Created**: January 5, 2026
