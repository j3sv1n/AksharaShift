using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AksharaShift
{
    /// <summary>
    /// Implements a global keyboard hook using Windows API to intercept keyboard events.
    /// Detects Win+Shift+M and Win+Shift+F key combinations for Malayalam text conversion.
    /// </summary>
    public class KeyboardHook
    {
        // Windows API constants
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        // Virtual key codes
        private const int VK_LCTRL = 0xA2;
        private const int VK_RCTRL = 0xA3;
        private const int VK_LMENU = 0xA4;  // Alt key
        private const int VK_RMENU = 0xA5;  // Alt key
        private const int VK_1 = 0x31;
        private const int VK_2 = 0x32;

        // P/Invoke declarations for Windows API
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        // Keyboard hook delegate
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr hookHandle = IntPtr.Zero;
        private LowLevelKeyboardProc? hookProc = null;

        // Events
        public event EventHandler<KeyEventArgs>? OnCtrlAlt1;
        public event EventHandler<KeyEventArgs>? OnCtrlAlt2;

        /// <summary>
        /// Installs the global keyboard hook.
        /// </summary>
        public void Install()
        {
            if (hookHandle != IntPtr.Zero)
                return;

            hookProc = HookCallback;
            IntPtr moduleHandle = GetModuleHandle(Process.GetCurrentProcess().MainModule?.ModuleName ?? "");
            hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, moduleHandle, 0);

            if (hookHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Failed to install keyboard hook.");
            }
        }

        /// <summary>
        /// Uninstalls the global keyboard hook.
        /// </summary>
        public void Uninstall()
        {
            if (hookHandle == IntPtr.Zero)
                return;

            UnhookWindowsHookEx(hookHandle);
            hookHandle = IntPtr.Zero;
            hookProc = null;
        }

        /// <summary>
        /// Keyboard hook callback - processes keyboard events.
        /// </summary>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                
                // Always check current key states using GetAsyncKeyState for accuracy
                bool ctrlPressed = (GetAsyncKeyState(VK_LCTRL) & 0x8000) != 0 || (GetAsyncKeyState(VK_RCTRL) & 0x8000) != 0;
                bool altPressed = (GetAsyncKeyState(VK_LMENU) & 0x8000) != 0 || (GetAsyncKeyState(VK_RMENU) & 0x8000) != 0;

                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    // Detect Ctrl+Alt+1
                    if (vkCode == VK_1 && ctrlPressed && altPressed)
                    {
                        OnCtrlAlt1?.Invoke(this, new KeyEventArgs());
                    }

                    // Detect Ctrl+Alt+2
                    if (vkCode == VK_2 && ctrlPressed && altPressed)
                    {
                        OnCtrlAlt2?.Invoke(this, new KeyEventArgs());
                    }
                }
            }

            return CallNextHookEx(hookHandle, nCode, wParam, lParam);
        }
    }

    /// <summary>
    /// Custom EventArgs for keyboard hook events.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
    }
}
