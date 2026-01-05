using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace AksharaShift;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private KeyboardHook keyboardHook;
    private TextProcessor textProcessor;
    private NotifyIcon? notifyIcon;
    private bool isEnabled = true;

    public MainWindow()
    {
        InitializeComponent();
        
        // Hide the window
        this.ShowInTaskbar = false;
        this.WindowStyle = WindowStyle.None;
        this.Height = 0;
        this.Width = 0;
        
        // Initialize components
        keyboardHook = new KeyboardHook();
        textProcessor = new TextProcessor();
        
        // Setup event handlers
        keyboardHook.OnCtrlAlt1 += KeyboardHook_OnCtrlAlt1;
        keyboardHook.OnCtrlAlt2 += KeyboardHook_OnCtrlAlt2;

        // Setup system tray
        SetupSystemTray();
        
        // Install keyboard hook
        try
        {
            keyboardHook.Install();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to install keyboard hook: {ex.Message}");
        }

        this.Loaded += MainWindow_Loaded;
        this.Closing += MainWindow_Closing;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Hide the window from appearing in taskbar
        this.Hide();
        this.WindowState = WindowState.Minimized;
    }

    /// <summary>
    /// Handles Ctrl+Alt+1 (ML Format)
    /// </summary>
    private void KeyboardHook_OnCtrlAlt1(object? sender, KeyEventArgs e)
    {
        if (!isEnabled)
            return;

        try
        {
            textProcessor.ConvertAndReplaceSelectedText(ConversionType.ML);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error during ML conversion: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles Ctrl+Alt+2 (FML Format)
    /// </summary>
    private void KeyboardHook_OnCtrlAlt2(object? sender, KeyEventArgs e)
    {
        if (!isEnabled)
            return;

        try
        {
            textProcessor.ConvertAndReplaceSelectedText(ConversionType.FML);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error during FML conversion: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets up the system tray icon with context menu.
    /// </summary>
    private void SetupSystemTray()
    {
        notifyIcon = new NotifyIcon();
        notifyIcon.Icon = SystemIcons.Application;
        notifyIcon.Text = "AksharaShift - Malayalam Text Converter";
        notifyIcon.Visible = true;

        ContextMenuStrip contextMenu = new ContextMenuStrip();

        // Toggle enable/disable
        ToolStripMenuItem toggleItem = new ToolStripMenuItem();
        toggleItem.Text = "Enabled";
        toggleItem.Checked = isEnabled;
        toggleItem.Click += (s, e) =>
        {
            isEnabled = !isEnabled;
            toggleItem.Checked = isEnabled;
            toggleItem.Text = isEnabled ? "Enabled" : "Disabled";
        };
        contextMenu.Items.Add(toggleItem);

        // Separator
        contextMenu.Items.Add(new ToolStripSeparator());

        // Exit item
        ToolStripMenuItem exitItem = new ToolStripMenuItem();
        exitItem.Text = "Exit";
        exitItem.Click += (s, e) =>
        {
            this.Close();
        };
        contextMenu.Items.Add(exitItem);

        notifyIcon.ContextMenuStrip = contextMenu;
    }

    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        keyboardHook?.Uninstall();
        notifyIcon?.Dispose();
    }
}