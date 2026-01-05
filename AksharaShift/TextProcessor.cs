using System;
using System.Threading;
using System.Windows;

namespace AksharaShift
{
    /// <summary>
    /// Handles clipboard operations including text capture, conversion, and replacement.
    /// Manages the flow: copy selected text → convert → paste back → restore clipboard.
    /// </summary>
    public class TextProcessor
    {
        private string previousClipboardContent = string.Empty;
        private bool hasStoredClipboard = false;

        /// <summary>
        /// Saves the current clipboard content before modification.
        /// </summary>
        private void SaveClipboardContent()
        {
            try
            {
                previousClipboardContent = System.Windows.Forms.Clipboard.GetText() ?? string.Empty;
                hasStoredClipboard = true;
            }
            catch
            {
                hasStoredClipboard = false;
            }
        }

        /// <summary>
        /// Restores the previously saved clipboard content.
        /// </summary>
        private void RestoreClipboardContent()
        {
            if (!hasStoredClipboard || string.IsNullOrEmpty(previousClipboardContent))
                return;

            try
            {
                System.Windows.Forms.Clipboard.SetText(previousClipboardContent);
                hasStoredClipboard = false;
            }
            catch
            {
                // Silently fail if restoration is not possible
            }
        }

        /// <summary>
        /// Copies currently selected text to clipboard.
        /// Works by simulating Ctrl+C keyboard shortcut.
        /// </summary>
        private void CopySelectedText()
        {
            try
            {
                KeyboardSimulator.SimulateKeyPress(System.Windows.Input.Key.C, 
                    System.Windows.Input.ModifierKeys.Control);
                
                // Give the system time to copy to clipboard
                Thread.Sleep(100);
            }
            catch
            {
                // Silently handle copy failures
            }
        }

        /// <summary>
        /// Pastes text from clipboard.
        /// Works by simulating Ctrl+V keyboard shortcut.
        /// </summary>
        private void PasteText()
        {
            try
            {
                KeyboardSimulator.SimulateKeyPress(System.Windows.Input.Key.V, 
                    System.Windows.Input.ModifierKeys.Control);
                
                // Give the system time to paste
                Thread.Sleep(100);
            }
            catch
            {
                // Silently handle paste failures
            }
        }

        /// <summary>
        /// Gets the currently selected text using clipboard simulation.
        /// </summary>
        /// <returns>Selected text, or empty string if operation fails</returns>
        public string GetSelectedText()
        {
            try
            {
                SaveClipboardContent();
                
                // Clear clipboard
                System.Windows.Forms.Clipboard.Clear();
                Thread.Sleep(50);

                // Copy selected text using Ctrl+C
                CopySelectedText();

                // Get the copied text
                string selectedText = System.Windows.Forms.Clipboard.GetText() ?? string.Empty;
                
                return selectedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Performs complete conversion and replacement workflow for selected text.
        /// 1. Captures selected text
        /// 2. Converts using specified format
        /// 3. Replaces selected text with converted text
        /// 4. Restores clipboard
        /// </summary>
        /// <param name="conversionType">Type of conversion (ML or FML)</param>
        /// <returns>True if operation successful, false otherwise</returns>
        public bool ConvertAndReplaceSelectedText(ConversionType conversionType)
        {
            try
            {
                // Save current clipboard
                SaveClipboardContent();

                // Get selected text
                string selectedText = GetSelectedText();
                
                if (string.IsNullOrEmpty(selectedText))
                {
                    RestoreClipboardContent();
                    return false;
                }

                // Convert text based on type
                string convertedText = conversionType == ConversionType.ML
                    ? MalayalamTextConverter.ConvertToML(selectedText)
                    : MalayalamTextConverter.ConvertToFML(selectedText);

                // Put converted text in clipboard
                System.Windows.Forms.Clipboard.SetText(convertedText);
                Thread.Sleep(50);

                // Paste the converted text
                PasteText();

                // Restore original clipboard content
                RestoreClipboardContent();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ConvertAndReplaceSelectedText: {ex.Message}");
                
                // Try to restore clipboard on failure
                try
                {
                    RestoreClipboardContent();
                }
                catch { }

                return false;
            }
        }

        /// <summary>
        /// Directly converts text without clipboard operations.
        /// Useful for testing and API access.
        /// </summary>
        public string ConvertText(string text, ConversionType conversionType)
        {
            return conversionType == ConversionType.ML
                ? MalayalamTextConverter.ConvertToML(text)
                : MalayalamTextConverter.ConvertToFML(text);
        }
    }

    /// <summary>
    /// Helper class to simulate keyboard input using Windows API.
    /// </summary>
    public static class KeyboardSimulator
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const uint KEYEVENTF_KEYUP = 2;

        /// <summary>
        /// Simulates pressing a key with optional modifiers.
        /// </summary>
        public static void SimulateKeyPress(System.Windows.Input.Key key, System.Windows.Input.ModifierKeys modifiers)
        {
            // Handle modifier keys
            if ((modifiers & System.Windows.Input.ModifierKeys.Control) > 0)
            {
                keybd_event(0x11, 0, 0, UIntPtr.Zero); // VK_CONTROL down
            }

            if ((modifiers & System.Windows.Input.ModifierKeys.Shift) > 0)
            {
                keybd_event(0x10, 0, 0, UIntPtr.Zero); // VK_SHIFT down
            }

            if ((modifiers & System.Windows.Input.ModifierKeys.Alt) > 0)
            {
                keybd_event(0x12, 0, 0, UIntPtr.Zero); // VK_MENU down
            }

            // Get virtual key code for the key
            byte vk = (byte)System.Windows.Forms.Keys.C; // Default to C
            switch (key)
            {
                case System.Windows.Input.Key.C:
                    vk = (byte)System.Windows.Forms.Keys.C;
                    break;
                case System.Windows.Input.Key.V:
                    vk = (byte)System.Windows.Forms.Keys.V;
                    break;
                case System.Windows.Input.Key.X:
                    vk = (byte)System.Windows.Forms.Keys.X;
                    break;
                case System.Windows.Input.Key.A:
                    vk = (byte)System.Windows.Forms.Keys.A;
                    break;
            }

            // Press the key
            keybd_event(vk, 0, 0, UIntPtr.Zero);
            Thread.Sleep(50);

            // Release the key
            keybd_event(vk, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);

            // Release modifier keys
            if ((modifiers & System.Windows.Input.ModifierKeys.Control) > 0)
            {
                keybd_event(0x11, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }

            if ((modifiers & System.Windows.Input.ModifierKeys.Shift) > 0)
            {
                keybd_event(0x10, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }

            if ((modifiers & System.Windows.Input.ModifierKeys.Alt) > 0)
            {
                keybd_event(0x12, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
        }
    }
}
