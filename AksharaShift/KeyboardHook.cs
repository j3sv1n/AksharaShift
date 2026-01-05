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
        private const int VK_LWIN = 0x5B;
        private const int VK_RWIN = 0x5C;
        private const int VK_SHIFT = 0x10;
        private const int VK_M = 0x4D;
        private const int VK_F = 0x46;

        // Keyboard state
        private bool isWinPressed = false;
        private bool isShiftPressed = false;

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
        public event EventHandler<KeyEventArgs>? OnWinShiftM;
        public event EventHandler<KeyEventArgs>? OnWinShiftF;

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
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                // Get the virtual key code
                int vkCode = Marshal.ReadInt32(lParam);

                // Track Win key state
                if (vkCode == VK_LWIN || vkCode == VK_RWIN)
                {
                    isWinPressed = true;
                }

                // Track Shift key state
                if (vkCode == VK_SHIFT)
                {
                    isShiftPressed = true;
                }

                // Detect Win+Shift+M
                if (vkCode == VK_M && isWinPressed && isShiftPressed)
                {
                    OnWinShiftM?.Invoke(this, new KeyEventArgs());
                }

                // Detect Win+Shift+F
                if (vkCode == VK_F && isWinPressed && isShiftPressed)
                {
                    OnWinShiftF?.Invoke(this, new KeyEventArgs());
                }
            }

            // Update key states on key up
            if (wParam == (IntPtr)0x0101) // WM_KEYUP
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (vkCode == VK_LWIN || vkCode == VK_RWIN)
                {
                    // Re-check if Win is still pressed
                    isWinPressed = (GetAsyncKeyState(VK_LWIN) & 0x8000) != 0 || (GetAsyncKeyState(VK_RWIN) & 0x8000) != 0;
                }

                if (vkCode == VK_SHIFT)
                {
                    isShiftPressed = (GetAsyncKeyState(VK_SHIFT) & 0x8000) != 0;
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
