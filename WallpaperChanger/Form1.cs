using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace WallpaperChanger;

using System.IO;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;
public partial class Form1 : Form
{
    private readonly SettingsManager _settingsManager;
    private readonly ThemeManager _themeManager;
    private readonly LanguageManager _languageManager;
    private readonly UploadManager _uploadManager;
    private readonly DeleteManager _deleteManager;
    
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(
        int uAction, int uParam, string lpvParam, int fuWinIni);

    private const int SpiSetdeskwallpaper = 20;
    private const int SpifUpdateinifile = 0x01;
    private const int SpifSendchange = 0x02;
    
    public Form1()
    {
        InitializeComponent();
        CenterToScreen();
        
        _settingsManager = new SettingsManager(Path.Combine(Application.StartupPath, "Wallpapers"));
        _themeManager = new ThemeManager(_settingsManager, this);
        _languageManager = new LanguageManager(_settingsManager, this);
        _deleteManager = new DeleteManager(_settingsManager.WallpapersPath, _languageManager, _settingsManager, wallpapersListBox);
        _uploadManager = new UploadManager(_settingsManager.WallpapersPath, _settingsManager, wallpapersListBox);
        
        wallpapersListBox.AllowDrop = true;
        wallpapersListBox.DragEnter += _uploadManager.DragEnterWindow;
        wallpapersListBox.DragDrop += _uploadManager.DragAndDropWindow;
        wallpapersListBox.DragLeave += _uploadManager.DragLeaveWindow;
        
        _themeManager.ApplyTheme();
        _languageManager.ApplyLanguage();
        WallpaperDisplay();
    }
    
    private void WallpaperDisplay()
    {
        if (wallpapersListBox == null) return;
        wallpapersDisplay.Controls.Clear();
        wallpapersListBox.BeginUpdate();
        wallpapersListBox.Items.Clear();

        var wallpapersPath = Path.Combine(_settingsManager.WallpapersPath);

        if (!Directory.Exists(wallpapersPath)){
            wallpapersListBox.EndUpdate();
            return;
        }
        
        var imageFiles = Directory.GetFiles(wallpapersPath, "*.*")
            .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"));

        foreach (var file in imageFiles)
        {
            wallpapersListBox.Items.Add(Path.GetFileName(file));
        }
        wallpapersListBox.EndUpdate();
    }

    private void WallpapersListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is string selectedWallpaper)
        {
            var fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);

            if (File.Exists(fullPath))
            {
                using var img = Image.FromFile(fullPath);
                wallpapersDisplay.Image?.Dispose();
                wallpapersDisplay.Image = new Bitmap(img);
            }
        }
    }

    private void ChangeLanguage(object sender, EventArgs e)
    {
        if (languageComboBox.SelectedItem?.ToString() == "ENG")
            _languageManager.SetLanguage(LanguageManager.Language.ENG);
        else
            _languageManager.SetLanguage(LanguageManager.Language.PL);
        
        _languageManager.ApplyLanguage();
    }

    private void LoadDefaultSettings()
    {
        _settingsManager.Theme = "Light";
        _settingsManager.WallpapersPath = Path.Combine(Application.StartupPath, "Wallpapers");
        _languageManager.SetLanguage(LanguageManager.Language.ENG);
        _themeManager.ApplyTheme();
        _languageManager.ApplyLanguage();
        _settingsManager.SaveSettings();
        WallpaperDisplay();
    }

    public void OpenWallpaperFolderClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is string selectedWallpaper)
        {
            string fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);
    
            if (File.Exists(fullPath))
                Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
            else
                MessageBox.Show(_languageManager.GetText("FileNotExists"), _languageManager.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            Process.Start("explorer.exe", _settingsManager.WallpapersPath);
        }
    }
    
    private void ApplyWallpaperButtonClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show(_languageManager.GetText("SelectWallpaper"), _languageManager.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
         
        var fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);
    
        if (!File.Exists(fullPath))
        {
            MessageBox.Show("FileNotExists");
            return;
        }
    
        var result = SystemParametersInfo(
            SpiSetdeskwallpaper,
            0,
            fullPath,
            SpifUpdateinifile | SpifSendchange);
    
        MessageBox.Show(result ? _languageManager.GetText("AppliedToast") : _languageManager.GetText("WallpaperNotUpdated"));
    }

    private void AddWallpaperClick(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Obrazy (*.jpg; *.png; *.bmp) | *.jpg; *.png; *.bmp";
        openFileDialog.Multiselect = true;
        
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            _uploadManager.UploadMultiple(openFileDialog.FileNames);
        }
    }

    public void DeleteWallpaperClick(object sender, EventArgs e)
    {
        _deleteManager.DeleteWallpaper();
        wallpapersDisplay.Image?.Dispose();
        wallpapersDisplay.Image = null;
        WallpaperDisplay();
    }

    public void ChangeWallpaperFolderClick(object? sender, EventArgs? e)
    {
        using (var folderDialog = new FolderBrowserDialog())
        {
            folderDialog.Description = "Select Wallpaper(s) folder";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                _settingsManager.WallpapersPath = folderDialog.SelectedPath;
                WallpaperDisplay();
                MessageBox.Show("Wallpaper folder selected!");
            }
        }
        _settingsManager.SaveSettings();
    }
}