using Microsoft.VisualBasic.Devices;

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
    private ThemeManager _themeManager;
    private LanguageManager _languageManager;
    
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
        _themeManager.ApplyTheme();
        _languageManager = new LanguageManager(_settingsManager, this);
        _languageManager.ApplyLanguage();
        
        WallpaperDisplay();
    }
    
    private void WallpaperDisplay()
    {
        if (wallpapersListBox == null) return;
        wallpapersDisplay.Controls.Clear();
        wallpapersListBox.BeginUpdate();
        wallpapersListBox.Items.Clear();

        string wallpapersPath = Path.Combine(_settingsManager.WallpapersPath);

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
            string fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);

            if (File.Exists(fullPath))
            {
                using var img = Image.FromFile(fullPath);
                wallpapersDisplay.Image?.Dispose();
                wallpapersDisplay.Image = new Bitmap(img);
            }
        }
    }

    public void ChangeLanguage(object sender, EventArgs e)
    {
        if (languageComboBox.SelectedItem?.ToString() == "ENG")
            _languageManager.SetLanguage(LanguageManager.Language.ENG);
        else
            _languageManager.SetLanguage(LanguageManager.Language.PL);

        _languageManager.ApplyLanguage();
    }
    private void ApplyButtonClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show("Please select a wallpaper.");
            return;
        }
         
        string fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);

        if (!File.Exists(fullPath))
        {
            MessageBox.Show("File does not exist.");
            return;
        }

        bool result = SystemParametersInfo(
            SpiSetdeskwallpaper,
            0,
            fullPath,
            SpifUpdateinifile | SpifSendchange);

        MessageBox.Show(result ? "Wallpaper changed." : "Wallpaper could not be updated.");
    }

    private void AddWallPaperButtonClick(object sender, EventArgs e)
    {
        using OpenFileDialog fileExplorer = new OpenFileDialog();
        fileExplorer.Title = "Select Wallpaper(s) to upload";
        fileExplorer.Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
        fileExplorer.Multiselect = true;

        if (fileExplorer.ShowDialog() == DialogResult.OK)
        {
            string fullPath = Path.Combine(_settingsManager.WallpapersPath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            foreach (string selectedWallpaper in fileExplorer.FileNames)
            {
                string fileName = Path.GetFileName(selectedWallpaper);
                string destinationPath = Path.Combine(fullPath, fileName);

                if (!File.Exists(destinationPath))
                {
                    File.Copy(selectedWallpaper, destinationPath);
                    WallpaperDisplay();
                    MessageBox.Show("Wallpaper(s) added!");
                }
                else
                {
                    MessageBox.Show("Wallpaper(s) already added!");
                    WallpaperDisplay();
                }
            }
        }
    }

    private void DeleteWallPaperButtonClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show("Please select a Wallpaper!");
            return;
        }
        string fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);

        if (File.Exists(fullPath))
        {
            var confirm = MessageBox.Show($"Czy na pewno chcesz usunąć {selectedWallpaper}'?", "Potwierdź akcję",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    File.Delete(fullPath);

                    wallpapersDisplay.Image?.Dispose();
                    wallpapersDisplay.Image = null;
                    WallpaperDisplay();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd przy usuwaniu: " + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Wallapper not found!");
        }

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