namespace WallpaperChanger;

using System.IO;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;
public partial class Form1 : Form
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(
        int uAction, int uParam, string lpvParam, int fuWinIni);

    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDCHANGE = 0x02;
    
    private readonly string settingsPath = Path.Combine(Application.StartupPath, "settings.txt");
    private string wallpaperPath;
    public Form1()
    {
        InitializeComponent();
        WallpaperDisplay();
        CenterToScreen();
        if (File.Exists(settingsPath))
        {
            string theme = File.ReadAllText(settingsPath).Trim();

            if (theme == "Dark")
            {
                isDarkMode = false;
                ChangeDarkMode(null, null);
            }
        }
    }

    private void WallpaperDisplay()
    {
        if (wallpapersListBox == null) return;
        
        wallpapersDisplay.Controls.Clear();
        wallpapersListBox.BeginUpdate();
        wallpapersListBox.Items.Clear();

        string wallpapersPath = Path.Combine(Application.StartupPath, "Wallpapers");

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
        if (wallpapersListBox.SelectedItem is string fileName)
        {
            string fullPath = Path.Combine(Application.StartupPath, "Wallpapers", fileName);

            if (File.Exists(fullPath))
            {
                using (var img = Image.FromFile(fullPath))
                {
                    wallpapersDisplay.Image?.Dispose();
                    wallpapersDisplay.Image = new Bitmap(img);
                }
            }
        }

    }
    private void ApplyButtonClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show("Please select a wallpaper.");
            return;
        }
         
        string fullPath = Path.Combine(Application.StartupPath, "Wallpapers", selectedWallpaper);

        if (!File.Exists(fullPath))
        {
            MessageBox.Show("File does not exist.");
            return;
        }

        bool result = SystemParametersInfo(
            SPI_SETDESKWALLPAPER,
            0,
            fullPath,
            SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

        if (result)
        {
            MessageBox.Show("Wallpaper changed.");
        }
        else
        {
            MessageBox.Show("Wallpaper could not be updated.");
        }
    }

    private void AddWallPaperButtonClick(object sender, EventArgs e)
    {
        using (OpenFileDialog fileExplorer = new OpenFileDialog())
        {
            fileExplorer.Title = "Select Wallpaper(s) to upload";
            fileExplorer.Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            fileExplorer.Multiselect = true;

            if (fileExplorer.ShowDialog() == DialogResult.OK)
            {
                string wallpaperPath = Path.Combine(Application.StartupPath, "Wallpapers");

                if (!Directory.Exists(wallpaperPath))
                {
                    Directory.CreateDirectory(wallpaperPath);
                }

                foreach (string selectedWallpaper in fileExplorer.FileNames)
                {
                    string fileName = Path.GetFileName(selectedWallpaper);
                    string destinationPath = Path.Combine(wallpaperPath, fileName);

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
    }

    private void DeleteWallPaperButtonClick(object sender, EventArgs e)
    {
        if (wallpapersListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show("Please select a Wallpaper!");
            return;
        }
        string fullPath = Path.Combine(Application.StartupPath, "Wallpapers", selectedWallpaper);

        if (File.Exists(fullPath))
        {
            var confirm = MessageBox.Show($"Czy na pewno chcesz usunąć {selectedWallpaper}'?", "Potwierdź akcję",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    File.Delete(fullPath);
                    MessageBox.Show("Wallpaper deleted!");

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

    private bool isDarkMode = false;
    public void ChangeDarkMode(object? sender, EventArgs? e)
    {
        isDarkMode = !isDarkMode;
        if (isDarkMode)
        {
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            foreach (Control control in Controls)
            {
                control.BackColor = Color.FromArgb(100, 100, 100);
                control.ForeColor = Color.White;

                if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                }
                darkModeButton.Text = "\u2600\ufe0f";
            }
        }
        else
        {
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;

            foreach (Control control in Controls)
            {
                control.BackColor = SystemColors.Control;
                control.ForeColor = SystemColors.ControlText;

                if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                }
            }
            darkModeButton.Text = "\ud83c\udf19";
        }
        darkModeButton.Text = isDarkMode ? "\u2600\ufe0f9" : "\ud83c\udf19";
        File.WriteAllText(settingsPath, isDarkMode ? "Dark" : "Light");
    }
}