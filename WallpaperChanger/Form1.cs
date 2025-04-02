using System.Windows.Forms.VisualStyles;
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
    private UploadManager _uploadManager;
    
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
        _uploadManager = new UploadManager(_settingsManager.WallpapersPath, wallpapersListBox, _settingsManager, _themeManager);
        
        wallpapersListBox.AllowDrop = true;
        wallpapersListBox.DragEnter += new DragEventHandler(_uploadManager.DragEnterWindow);
        wallpapersListBox.DragDrop += new DragEventHandler(_uploadManager.DragAndDropWindow);
        wallpapersListBox.DragLeave += new EventHandler(_uploadManager.DragLeaveWindow);
        
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
    
    private void ApplyWallpaperButtonClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show(_languageManager.GetText("SelectWallpaper"));
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

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            _uploadManager.UploadFromFile(openFileDialog.FileName);
        }
    }

    public void DragAndDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
        {
            _uploadManager.UploadMultiple(files);
        }
    }

    
    // private void AddWallpaperButtonClick(object sender, EventArgs e)
    // {
    //     using var fileExplorer = new OpenFileDialog();
    //     fileExplorer.Title = _languageManager.GetText("SelectWallpaperToAdd");
    //     fileExplorer.Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
    //     fileExplorer.Multiselect = true;
    //     
    //     if (fileExplorer.ShowDialog() == DialogResult.OK)
    //     {
    //         var fullPath = Path.Combine(_settingsManager.WallpapersPath);
    //
    //         if (!Directory.Exists(fullPath))
    //         {
    //             Directory.CreateDirectory(fullPath);
    //         }
    //
    //         foreach (var selectedWallpaper in fileExplorer.FileNames)
    //         {
    //             var fileName = Path.GetFileName(selectedWallpaper);
    //             var destinationPath = Path.Combine(fullPath, fileName);
    //
    //             if (!File.Exists(destinationPath))
    //             {
    //                 File.Copy(selectedWallpaper, destinationPath);
    //                 WallpaperDisplay();
    //                 MessageBox.Show(_languageManager.GetText("AddedToast"));
    //             }
    //             else
    //             {
    //                 MessageBox.Show(_languageManager.GetText("WallpaperAlreadyAdded"));
    //                 WallpaperDisplay();
    //             }
    //         }
    //     }
    // }

    
    private void DeleteWallpaperButtonClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show(_languageManager.GetText("SelectWallpaper"));
            return;
        }
        var fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);

        if (File.Exists(fullPath))
        {
            var confirm = MessageBox.Show(_languageManager.GetText("AreYouSureToDelete"), _languageManager.GetText("SubmitAction"),
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
                    MessageBox.Show(_languageManager.GetText("Error") + ex.Message);
                }
            }
        }
        else
        {
            MessageBox.Show(_languageManager.GetText("WallpaperNotFound"));
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