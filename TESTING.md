# AksharaShift - Testing and Verification Guide

## Pre-Testing Checklist

- [ ] Windows 10 or Windows 11 system
- [ ] Malayalam Phonetic keyboard layout installed
- [ ] .NET 8.0 Runtime installed
- [ ] ML and FML fonts installed (optional, for proper display)
- [ ] Application built successfully (AksharaShift.exe exists)

## Test Environment Setup

### 1. Install Malayalam Phonetic Keyboard

1. Open Settings → Time & Language → Language & region
2. Click "Add a language"
3. Search for and select "Malayalam"
4. Select the Malayalam input method to be added
5. Once added, click on Malayalam → Options → Add input method
6. Select "Malayalam Phonetic" input method
7. Close Settings

### 2. Verify Keyboard Layout

1. Open Notepad or any text editor
2. Observe the keyboard indicator in the system tray (bottom right)
3. Click the language indicator and select "Malayalam Phonetic"
4. Try typing: `anugraham` (in English letters)
   - Should display as: `അനുഗ്രഹം` (Unicode Malayalam)

### 3. Install ML and FML Fonts (Optional)

Download and install fonts to see converted text properly:
- ML-TTCore, ML-Standard fonts for ML format
- FMLBareily, FMLGothic fonts for FML format

## Functional Testing

### Test 1: Basic Conversion (ML Format)

**Objective**: Verify Win+Shift+M converts Unicode Malayalam to ML format

**Steps**:
1. Open Notepad
2. Switch to Malayalam Phonetic keyboard
3. Type: `namaskar` → Should display as `നമസ്കാര്`
4. Select the text (Ctrl+A or mouse drag)
5. Press **Win + Shift + M**
6. Observe the selected text

**Expected Result**:
- Selected text is replaced with ML legacy format
- Original clipboard content is restored
- Text appears as legacy font characters (may look like symbols if fonts not installed)

**Pass/Fail**: ___________

### Test 2: Basic Conversion (FML Format)

**Objective**: Verify Win+Shift+F converts Unicode Malayalam to FML format

**Steps**:
1. Open Notepad
2. Type: `namaskaram` → Should display as `നമസ്കാരം`
3. Select the text
4. Press **Win + Shift + F**
5. Observe the selected text

**Expected Result**:
- Selected text is replaced with FML legacy format
- Text appears different from ML format (different character mapping)

**Pass/Fail**: ___________

### Test 3: Keyboard Layout Detection

**Objective**: Verify conversion only works with Malayalam Phonetic layout

**Steps**:
1. Open Notepad with some Malayalam text selected
2. Switch to English (US) keyboard layout
3. Press **Win + Shift + M**
4. Observe nothing happens

**Expected Result**:
- Shortcut has no effect when Malayalam Phonetic is not active
- Text remains unchanged

**Pass/Fail**: ___________

### Test 4: Multiple Application Testing

**Objective**: Verify shortcuts work across different applications

**Applications to test**:
- [ ] Notepad
- [ ] Microsoft Word
- [ ] Google Chrome (in text field)
- [ ] Email client

**Steps for each**:
1. Type or paste Unicode Malayalam text
2. Select the text
3. Press **Win + Shift + M**
4. Verify conversion occurs

**Pass/Fail**: ___________

### Test 5: Clipboard Restoration

**Objective**: Verify original clipboard content is restored after conversion

**Steps**:
1. Copy some text to clipboard (e.g., "Hello World")
2. Open Notepad with Malayalam text
3. Select and convert using **Win + Shift + M**
4. Open another text editor
5. Paste (Ctrl+V)

**Expected Result**:
- Original text "Hello World" is pasted (not the Malayalam text)
- Clipboard is properly restored

**Pass/Fail**: ___________

### Test 6: Empty Selection Handling

**Objective**: Verify application handles empty selection gracefully

**Steps**:
1. Click in Notepad without selecting any text
2. Press **Win + Shift + M**

**Expected Result**:
- No error message
- No changes to document
- Application remains responsive

**Pass/Fail**: ___________

### Test 7: System Tray Functionality

**Objective**: Verify system tray icon and context menu work correctly

**Steps**:
1. Launch AksharaShift
2. Look for icon in system tray (bottom right)
3. Right-click the icon
4. Click "Enabled" to toggle
5. Verify status changes in menu

**Expected Result**:
- Icon visible in system tray
- Context menu appears with "Enabled" and "Exit" options
- Toggling changes the checkmark status
- Conversion shortcuts disabled when "Enabled" is unchecked

**Pass/Fail**: ___________

### Test 8: Application Launch and Termination

**Objective**: Verify application starts cleanly and exits properly

**Steps**:
1. Start AksharaShift from command line:
   ```
   AksharaShift.exe
   ```
2. Verify system tray icon appears
3. Right-click system tray icon and click "Exit"
4. Verify application closes and tray icon disappears

**Expected Result**:
- Application launches silently
- No UI window appears (runs in background)
- Tray icon visible and functional
- Application exits cleanly on "Exit" command

**Pass/Fail**: ___________

## Character Mapping Verification

### Test ML Format Mappings

Create a test file with Malayalam text and verify ML conversion. Sample test text:

**Input (Unicode Malayalam)**:
```
കേരളം നിന്ദിത ഭാഷ
```

**Steps**:
1. Type or paste above text
2. Select all (Ctrl+A)
3. Press **Win + Shift + M**
4. Compare output with expected ML mappings

**Expected Output Characteristics**:
- All Malayalam characters converted to ASCII
- Output should be readable in ML font
- English text in input passes through unchanged

**Pass/Fail**: ___________

### Test FML Format Mappings

**Input (Unicode Malayalam)**:
```
ഫാ സ്മാർട്ട് ടെക്സ്ട് കൺവഷൻ
```

**Steps**:
1. Type above text
2. Select all
3. Press **Win + Shift + F**
4. Compare output with expected FML mappings

**Expected Output Characteristics**:
- Different from ML output (alternative character mapping)
- Output should be readable in FML font

**Pass/Fail**: ___________

## Performance Testing

### Test 1: Conversion Speed

**Objective**: Verify conversion completes quickly

**Setup**:
1. Create a document with 500+ Malayalam characters
2. Select all text
3. Note the time
4. Press **Win + Shift + M**
5. Note the time when replacement completes

**Expected Result**:
- Conversion completes within 500ms
- No noticeable delay in application responsiveness

**Pass/Fail**: ___________

### Test 2: Memory Usage

**Objective**: Verify application doesn't leak memory

**Steps**:
1. Open Task Manager (Ctrl+Shift+Esc)
2. Search for AksharaShift process
3. Note memory usage
4. Perform 10 conversions
5. Note memory usage again

**Expected Result**:
- Memory usage remains stable
- No significant increase after multiple conversions
- Should use < 100MB of RAM

**Pass/Fail**: ___________

## Auto-Start Testing

### Test 1: Registry Entry Creation

**Objective**: Verify auto-start registry entry is created correctly

**Steps**:
1. Run PowerShell:
   ```powershell
   .\Setup-AutoStart.ps1 -Action Enable
   ```
2. Open Registry Editor (regedit)
3. Navigate to: `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`
4. Look for `AksharaShift` entry

**Expected Result**:
- Registry entry `AksharaShift` exists
- Value contains full path to executable with `--minimized` flag

**Pass/Fail**: ___________

### Test 2: Auto-Start on Reboot

**Objective**: Verify application launches on Windows startup

**Steps**:
1. Enable auto-start using Setup-AutoStart.ps1
2. Restart the computer
3. Log in to user account
4. Wait 10 seconds
5. Check system tray for AksharaShift icon

**Expected Result**:
- Application appears in system tray after login
- No manual launch required
- Shortcuts are functional immediately

**Pass/Fail**: ___________

### Test 3: Auto-Start Disable

**Objective**: Verify auto-start can be disabled

**Steps**:
1. Run PowerShell:
   ```powershell
   .\Setup-AutoStart.ps1 -Action Disable
   ```
2. Open Registry Editor
3. Check for `AksharaShift` entry in Run key

**Expected Result**:
- Registry entry is removed
- Application no longer appears on startup

**Pass/Fail**: ___________

## Edge Cases and Error Handling

### Test 1: Non-Malayalam Unicode Input

**Input**: Emoji and special Unicode characters

**Steps**:
1. Type: `Hello 👋 World 你好`
2. Select all
3. Press **Win + Shift + M**

**Expected Result**:
- Non-Malayalam characters pass through unchanged
- No error message

**Pass/Fail**: ___________

### Test 2: Mixed Malayalam and English

**Input**: `namaskaram and enna kaarya`

**Steps**:
1. Type mixed text
2. Select all
3. Press **Win + Shift + M**

**Expected Result**:
- Malayalam part converted
- English part unchanged
- Output is properly mixed

**Pass/Fail**: ___________

### Test 3: Rapid Successive Conversions

**Steps**:
1. Type Malayalam text
2. Select text
3. Press **Win + Shift + M**
4. Immediately press **Win + Shift + M** again (while result displays)

**Expected Result**:
- Second conversion works on the converted text
- No crashes or errors

**Pass/Fail**: ___________

### Test 4: Very Long Text Conversion

**Input**: 10000+ Malayalam characters

**Steps**:
1. Paste large Malayalam text
2. Select all
3. Press **Win + Shift + M**
4. Verify completion

**Expected Result**:
- Conversion completes successfully
- No truncation of text
- Application remains responsive

**Pass/Fail**: ___________

## Regression Testing

### Test 1: Multiple Application Types

Test with different application categories:
- [ ] Text Editors (Notepad, VS Code, Sublime)
- [ ] Office Suite (Word, Excel, PowerPoint)
- [ ] Email Clients (Outlook, Gmail in browser)
- [ ] Chat Applications (Teams, Discord)
- [ ] Web Browsers (Chrome, Firefox, Edge)

### Test 2: Accessibility Tools

Verify compatibility with:
- [ ] Narrator (Windows screen reader)
- [ ] Windows Magnifier
- [ ] High contrast mode

## Documentation Tests

### Test 1: README Accuracy

- [ ] Build instructions work as documented
- [ ] Installation steps are correct
- [ ] Shortcuts are properly documented
- [ ] Configuration steps work

### Test 2: Code Comments

- [ ] All critical sections have comments
- [ ] Comments are clear and accurate
- [ ] Namespace documentation is present

## Test Summary Report

| Test # | Description | Expected | Actual | Status | Notes |
|--------|-------------|----------|--------|--------|-------|
| 1 | Basic ML Conversion | Text converts to ML | | | |
| 2 | Basic FML Conversion | Text converts to FML | | | |
| 3 | Layout Detection | Only works with Malayalam | | | |
| 4 | Multi-app Testing | Works across apps | | | |
| 5 | Clipboard Restoration | Original content restored | | | |
| 6 | Empty Selection | Handles gracefully | | | |
| 7 | System Tray | Icon and menu work | | | |
| 8 | Launch/Termination | Starts and exits cleanly | | | |
| 9 | ML Mappings | Correct character mapping | | | |
| 10 | FML Mappings | Correct character mapping | | | |

## Known Limitations

1. **Clipboard-based conversion**: Some applications restrict clipboard access
2. **Conjunct support**: Not all Malayalam conjunct combinations may have mappings
3. **Font dependency**: Output display depends on legacy fonts being installed
4. **Keyboard layout**: Only works with Malayalam Phonetic keyboard layout

## Troubleshooting Failed Tests

If a test fails:
1. Check the error message in Debug output (if available)
2. Verify Malayalam Phonetic keyboard is properly installed
3. Ensure the application was built correctly
4. Check that fonts are installed if testing display
5. Verify no conflicting keyboard shortcuts exist
6. Review the Troubleshooting section in main README.md

---

**Test Version**: 1.0  
**Last Updated**: January 5, 2026  
**Application**: AksharaShift v1.0
