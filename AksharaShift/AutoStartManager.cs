using System;
using Microsoft.Win32;

namespace AksharaShift
{
    /// <summary>
    /// Manages auto-start functionality for AksharaShift application.
    /// Configures the application to launch automatically on Windows boot.
    /// </summary>
    public class AutoStartManager
    {
        // Registry key path for startup programs
        private const string STARTUP_REGISTRY_PATH = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string APP_NAME = "AksharaShift";

        /// <summary>
        /// Enables auto-start by adding the application to Windows Registry startup.
        /// Requires administrator privileges.
        /// </summary>
        /// <returns>True if successfully enabled, false otherwise</returns>
        public static bool EnableAutoStart()
        {
            try
            {
                // Get the path to the current executable
                string? exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
                
                if (string.IsNullOrEmpty(exePath))
                    return false;

                // Open the registry key with write permissions
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(STARTUP_REGISTRY_PATH, true))
                {
                    if (key != null)
                    {
                        // Set the value to launch the app minimized
                        key.SetValue(APP_NAME, $"\"{exePath}\" --minimized");
                        return true;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                System.Diagnostics.Debug.WriteLine("Insufficient permissions to enable auto-start. Please run as administrator.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error enabling auto-start: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Disables auto-start by removing the application from Windows Registry startup.
        /// Requires administrator privileges.
        /// </summary>
        /// <returns>True if successfully disabled, false otherwise</returns>
        public static bool DisableAutoStart()
        {
            try
            {
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(STARTUP_REGISTRY_PATH, true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(APP_NAME, false);
                        return true;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                System.Diagnostics.Debug.WriteLine("Insufficient permissions to disable auto-start. Please run as administrator.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error disabling auto-start: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Checks if auto-start is currently enabled.
        /// </summary>
        /// <returns>True if auto-start is enabled, false otherwise</returns>
        public static bool IsAutoStartEnabled()
        {
            try
            {
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(STARTUP_REGISTRY_PATH, false))
                {
                    if (key != null)
                    {
                        object? value = key.GetValue(APP_NAME);
                        return value != null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking auto-start status: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Gets the auto-start registry value for the application.
        /// </summary>
        /// <returns>Registry value string, or empty string if not found</returns>
        public static string GetAutoStartValue()
        {
            try
            {
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(STARTUP_REGISTRY_PATH, false))
                {
                    if (key != null)
                    {
                        object? value = key.GetValue(APP_NAME);
                        return value?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting auto-start value: {ex.Message}");
            }

            return string.Empty;
        }
    }
}
