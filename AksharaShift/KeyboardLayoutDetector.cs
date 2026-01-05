using System;
using System.Runtime.InteropServices;

namespace AksharaShift
{
    /// <summary>
    /// Detects the current keyboard layout and checks if Malayalam Phonetic is active.
    /// </summary>
    public class KeyboardLayoutDetector
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        private static extern int GetKeyboardLayoutName(char[] pwszKLID);

        /// <summary>
        /// Gets the name of the current keyboard layout for the active window.
        /// </summary>
        /// <returns>Keyboard layout name (e.g., "00004d09" for Malayalam Phonetic)</returns>
        public static string GetCurrentKeyboardLayout()
        {
            try
            {
                IntPtr foregroundWindow = GetForegroundWindow();
                uint threadId = GetWindowThreadProcessId(foregroundWindow, out _);
                IntPtr layoutHandle = GetKeyboardLayout(threadId);
                
                char[] layoutName = new char[9];
                GetKeyboardLayoutName(layoutName);
                
                return new string(layoutName).TrimEnd('\0');
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Checks if the current keyboard layout is Malayalam Phonetic.
        /// Malayalam Phonetic has the LCID 0x4D09 (19721 in decimal)
        /// </summary>
        /// <returns>True if Malayalam Phonetic is active, false otherwise</returns>
        public static bool IsMalayalamPhoneticActive()
        {
            string layout = GetCurrentKeyboardLayout();
            
            // Malayalam Phonetic layout ID is 0x4D09
            // This corresponds to language ID 0x4D (Malayalam - 77 in decimal) with layout ID 09
            return layout.Contains("4d09") || layout.Contains("4D09");
        }

        /// <summary>
        /// Gets the full name/description of the current keyboard layout.
        /// </summary>
        /// <returns>Layout display name</returns>
        public static string GetKeyboardLayoutName()
        {
            try
            {
                string layout = GetCurrentKeyboardLayout();
                
                // Map common layout IDs to names
                return layout.ToUpper() switch
                {
                    "00004d09" => "Malayalam Phonetic",
                    "00000409" => "English (United States)",
                    _ => layout
                };
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
