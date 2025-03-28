namespace WallpaperChanger;

using System.IO;
using System.Linq;
using System.Drawing;
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        WallpaperDisplay();
        CenterToScreen();
    }

    private void WallpaperDisplay()
    {
        wallpapersDisplay.Controls.Clear();

        string wallpapersPath = Path.Combine(Application.StartupPath, "Wallpapers");

        if (!Directory.Exists(wallpapersPath)) return;
        
        var imageFiles = Directory.GetFiles(wallpapersPath, "*.*")
            .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"));

        foreach (var file in imageFiles)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile(file);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Width = 200;
            pictureBox.Height = 200;
            pictureBox.Margin = new Padding(5);
            pictureBox.Cursor = Cursors.Hand;
         
            pictureBox.Tag = file;

            pictureBox.Click += (s, e) =>
            {
                MessageBox.Show("Clicked wallpaper: " + file);
                WallpaperDisplay();
            };
            
            wallpapersDisplay.Controls.Add(pictureBox);
        }
    }
    private void ApplyButtonClick(object sender, EventArgs e)
    {
        MessageBox.Show("Wallpaper set!");
    }

    private void AddWallPaperButtonClick(object sender, EventArgs e)
    {
        using (OpenFileDialog fileExplorer = new OpenFileDialog())
        {
            fileExplorer.Title = "Select Wallpaper(s) to upload";
            fileExplorer.Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (fileExplorer.ShowDialog() == DialogResult.OK)
            {
                string selectedWallpaper = fileExplorer.FileName;
                string wallpaperPath = Path.Combine(Application.StartupPath, "Wallpapers");

                if (!Directory.Exists(wallpaperPath))
                {
                    Directory.CreateDirectory(wallpaperPath);
                }
                string fileName = Path.GetFileName(selectedWallpaper);
                string wallpaperDestination = Path.Combine(wallpaperPath, fileName);

                if (!File.Exists(selectedWallpaper))
                {
                    File.Copy(selectedWallpaper, wallpaperDestination);
                    MessageBox.Show("Wallpaper added!");
                }
                else
                {
                    MessageBox.Show("Wallpaper already added!");
                }
                
            }
        }
    }

    private void DeleteWallPaperButtonClick(object sender, EventArgs e)
    {
        
    }
}