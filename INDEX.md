# 📑 AksharaShift - Complete Project Index

## 🚀 Quick Navigation

### ⭐ **START HERE**
- **[QUICK_REFERENCE.md](QUICK_REFERENCE.md)** - 2-minute quick start guide
- **[AksharaShift.exe](AksharaShift/bin/Release/net8.0-windows/AksharaShift.exe)** - The executable to run

### 📖 **USER GUIDES**
- **[README.md](README.md)** - Complete user & developer guide
- **[TESTING.md](TESTING.md)** - How to test the application

### ⚙️ **INSTALLATION & DEPLOYMENT**
- **[BUILD_REPORT.md](BUILD_REPORT.md)** - Build details and deployment instructions
- **[Deploy-AksharaShift.ps1](Deploy-AksharaShift.ps1)** - Automated deployment script
- **[POWERSHELL_COMMANDS.md](POWERSHELL_COMMANDS.md)** - Admin commands & scripts

### 📚 **REFERENCE**
- **[CHARACTER_MAPPING_REFERENCE.md](CHARACTER_MAPPING_REFERENCE.md)** - Complete character tables
- **[IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md)** - Technical implementation details
- **[PROJECT_SUMMARY.md](PROJECT_SUMMARY.md)** - Complete project overview

---

## 📂 FILE STRUCTURE

```
d:\Projects\AksharaShift\
│
├── 📁 AksharaShift\                    (Main Application Folder)
│   ├── 📁 bin\Release\net8.0-windows\
│   │   ├── ⭐ AksharaShift.exe        (MAIN EXECUTABLE)
│   │   ├── AksharaShift.dll
│   │   ├── AksharaShift.pdb
│   │   └── [Runtime dependencies]
│   │
│   ├── 📁 obj\                        (Build artifacts)
│   │
│   ├── 📄 KeyboardHook.cs             (Global keyboard hook)
│   ├── 📄 KeyboardLayoutDetector.cs   (Malayalam detection)
│   ├── 📄 MalayalamTextConverter.cs   (100+ mappings)
│   ├── 📄 TextProcessor.cs            (Clipboard operations)
│   ├── 📄 AutoStartManager.cs         (Windows auto-start)
│   ├── 📄 MainWindow.xaml             (UI definition)
│   ├── 📄 MainWindow.xaml.cs          (Code-behind)
│   ├── 📄 App.xaml                    (Application def)
│   ├── 📄 App.xaml.cs                 (Startup code)
│   ├── 📄 AssemblyInfo.cs             (Assembly metadata)
│   └── 📄 AksharaShift.csproj         (Project config)
│
├── 📄 README.md                       (12.7 KB) - Full guide
├── 📄 BUILD_REPORT.md                 (9.6 KB) - Build details
├── 📄 TESTING.md                      (11.9 KB) - Test guide
├── 📄 CHARACTER_MAPPING_REFERENCE.md  (10.5 KB) - Character tables
├── 📄 POWERSHELL_COMMANDS.md          (16.2 KB) - Admin commands
├── 📄 IMPLEMENTATION_SUMMARY.md       (13.5 KB) - Tech details
├── 📄 QUICK_REFERENCE.md              (10.4 KB) - Quick start
├── 📄 PROJECT_SUMMARY.md              (Complete overview)
├── 📄 INDEX.md                        (This file)
└── 📄 Deploy-AksharaShift.ps1         (Deployment script)
```

---

## 🎯 COMMON TASKS

### "I want to run the application NOW"
1. Navigate to: `AksharaShift\bin\Release\net8.0-windows\`
2. Double-click: `AksharaShift.exe`
3. Look for tray icon
4. Test with Malayalam text + Win+Shift+M

👉 See: [QUICK_REFERENCE.md](QUICK_REFERENCE.md)

### "I want to install it permanently"
```powershell
# Run as Administrator
.\Deploy-AksharaShift.ps1 -Action Deploy -InstallPath "C:\Program Files\AksharaShift"
```

👉 See: [Deploy-AksharaShift.ps1](Deploy-AksharaShift.ps1)

### "I want to enable auto-start"
```powershell
# Run as Administrator
.\Deploy-AksharaShift.ps1 -Action EnableAutoStart
```

👉 See: [POWERSHELL_COMMANDS.md](POWERSHELL_COMMANDS.md)

### "I want to understand how it works"
👉 See: [IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md)

### "I want to see the character mappings"
👉 See: [CHARACTER_MAPPING_REFERENCE.md](CHARACTER_MAPPING_REFERENCE.md)

### "I want to test the application"
👉 See: [TESTING.md](TESTING.md)

### "I want to build it from scratch"
👉 See: [README.md](README.md) - "Building from Source" section

### "Something is not working"
👉 See: [BUILD_REPORT.md](BUILD_REPORT.md) - "Troubleshooting" section

---

## 📊 PROJECT STATISTICS

| Aspect | Details |
|--------|---------|
| **Language** | C# 12 |
| **Framework** | .NET 8.0 |
| **Platform** | Windows 10/11 |
| **Executable Size** | 142 KB |
| **Source Code** | 36.57 KB (8 files) |
| **Documentation** | 84 KB (8 files) |
| **Character Mappings** | 100+ |
| **Build Time** | ~4 seconds |
| **Status** | ✅ Production Ready |

---

## ✨ KEY FEATURES

- ✅ Global keyboard shortcuts (Win+Shift+M, Win+Shift+F)
- ✅ Malayalam Phonetic detection
- ✅ Unicode to ML/FML conversion
- ✅ System tray integration
- ✅ Auto-start support
- ✅ Clipboard restoration
- ✅ Production-quality code
- ✅ Comprehensive documentation

---

## 🔧 SYSTEM REQUIREMENTS

- **OS:** Windows 10 or 11
- **.NET:** 8.0 Runtime (included)
- **RAM:** 50 MB
- **Disk:** 200 MB
- **Keyboard:** Malayalam Phonetic layout

---

## 📞 DOCUMENTATION MAP

```
User Level                  Developer Level
───────────────────────────────────────────────────
Getting Started:            Architecture:
 ├─ QUICK_REFERENCE.md      ├─ IMPLEMENTATION_SUMMARY.md
 └─ README.md               └─ Source code files

Testing:                    Deployment:
 ├─ TESTING.md              ├─ BUILD_REPORT.md
 └─ CHARACTER_MAPPING_REFERENCE.md └─ Deploy-AksharaShift.ps1

Reference:                  Administration:
 ├─ QUICK_REFERENCE.md      ├─ POWERSHELL_COMMANDS.md
 └─ CHARACTER_MAPPING_REFERENCE.md └─ Deploy-AksharaShift.ps1
```

---

## 🎓 LEARNING PATH

1. **Start:** Read [QUICK_REFERENCE.md](QUICK_REFERENCE.md) (5 min)
2. **Run:** Execute [AksharaShift.exe](AksharaShift/bin/Release/net8.0-windows/AksharaShift.exe) (1 min)
3. **Test:** Follow [TESTING.md](TESTING.md) (10 min)
4. **Deploy:** Use [Deploy-AksharaShift.ps1](Deploy-AksharaShift.ps1) (5 min)
5. **Reference:** Check [CHARACTER_MAPPING_REFERENCE.md](CHARACTER_MAPPING_REFERENCE.md) as needed
6. **Deep Dive:** Study [IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md) (15 min)

**Total Time:** ~36 minutes to full understanding

---

## 🔐 SECURITY & PRIVACY

✅ No internet connectivity  
✅ No data collection  
✅ No telemetry  
✅ Registry only in HKCU (current user)  
✅ Clipboard temporary + restored  
✅ No persistent file modifications  

---

## 📞 SUPPORT INFORMATION

| Question | File | Section |
|----------|------|---------|
| How to use? | README.md | Usage Guide |
| How to install? | BUILD_REPORT.md | Deployment Instructions |
| How to test? | TESTING.md | Test Procedures |
| What chars convert? | CHARACTER_MAPPING_REFERENCE.md | All tables |
| Admin commands? | POWERSHELL_COMMANDS.md | All commands |
| How does it work? | IMPLEMENTATION_SUMMARY.md | Technical Details |
| Quick start? | QUICK_REFERENCE.md | Quick Guide |

---

## 🎉 SUMMARY

**AksharaShift** is a complete, production-ready Windows application for converting Unicode Malayalam text to legacy ASCII fonts. It includes:

- ✅ Fully compiled executable (142 KB)
- ✅ Complete source code (8 components)
- ✅ Comprehensive documentation (8 files, 84 KB)
- ✅ Deployment automation scripts
- ✅ Character conversion tables
- ✅ Testing procedures

**Status:** Ready for immediate use and deployment

---

## 📝 VERSION INFORMATION

- **Project:** AksharaShift
- **Version:** 1.0
- **Release Date:** January 5, 2026
- **Status:** ✅ Production Ready
- **Framework:** .NET 8.0
- **Platform:** Windows 10/11

---

**Last Updated:** January 5, 2026  
**Build Status:** ✅ Complete  
**Quality Level:** Production Ready

---

### Quick Links
- [Run Application Now](AksharaShift/bin/Release/net8.0-windows/AksharaShift.exe)
- [Quick Start Guide](QUICK_REFERENCE.md)
- [Full Documentation](README.md)
- [Deployment Script](Deploy-AksharaShift.ps1)
