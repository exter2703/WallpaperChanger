namespace WallpaperChanger;

partial class Form1
{
  
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button applyButton;
    private System.Windows.Forms.Button addWallPaperButton;
    private System.Windows.Forms.Button deleteWallPaperButton;
    private System.Windows.Forms.PictureBox wallpapersDisplay;
    private System.Windows.Forms.ListBox wallpapersListBox;
    private System.Windows.Forms.Button darkModeButton;
    private System.Windows.Forms.Button changeWallpaperFolderButton;
    private System.Windows.Forms.Button loadDefaultSettingsButton;
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.applyButton = new System.Windows.Forms.Button();
        this.addWallPaperButton = new System.Windows.Forms.Button();
        this.deleteWallPaperButton = new System.Windows.Forms.Button();
        this.wallpapersDisplay = new System.Windows.Forms.PictureBox();
        this.wallpapersListBox = new System.Windows.Forms.ListBox();
        this.darkModeButton = new System.Windows.Forms.Button();
        this.changeWallpaperFolderButton = new System.Windows.Forms.Button();
        this.loadDefaultSettingsButton = new System.Windows.Forms.Button();
        this.SuspendLayout();
        
        //wallpaperDisplay
        this.wallpapersDisplay.Location = new System.Drawing.Point(300, 100);
        this.wallpapersDisplay.Size = new System.Drawing.Size(1920/3, 1080/3);
        this.wallpapersDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.wallpapersDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.wallpapersDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
        this.wallpapersDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Controls.Add(this.wallpapersDisplay);

        //wallpaperListBox
        this.wallpapersListBox.Location = new System.Drawing.Point(55, 200);
        this.wallpapersListBox.Size = new System.Drawing.Size(210, 300);
        this.wallpapersListBox.SelectedIndexChanged += 
            new System.EventHandler(this.WallpapersListBoxSelectedIndexChanged);
        this.wallpapersListBox.FormattingEnabled = true;
        this.wallpapersListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        //this.wallpapersListBox.Dock = System.Windows.Forms.DockStyle.Right;
        
        //applyButton
        this.applyButton.Location = new System.Drawing.Point(55, 100);
        this.applyButton.Size = new System.Drawing.Size(210, 40);
        this.applyButton.Name = "applyButton";
        this.applyButton.Text = "\ud83c\udfaf Apply";
        this.applyButton.Font = new Font("Segoe UI Emoji", 15);
        this.applyButton.UseVisualStyleBackColor = true;
        this.applyButton.TextAlign = ContentAlignment.MiddleCenter;
        this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
        //this.applyButton.Dock = DockStyle.Top;
        
        //addWallpaperButton
        this.addWallPaperButton.Location = new System.Drawing.Point(55, 150);
        this.addWallPaperButton.Size = new System.Drawing.Size(100, 40);
        this.addWallPaperButton.Name = "addWallPaperButton";
        this.addWallPaperButton.Text = "\ud83d\uddbc\ufe0f Upload";
        this.addWallPaperButton.Font = new Font("Segoe UI Emoji", 12);
        this.addWallPaperButton.UseVisualStyleBackColor = true;
        this.addWallPaperButton.TextAlign = ContentAlignment.MiddleCenter;
        this.addWallPaperButton.Click += new System.EventHandler(this.AddWallPaperButtonClick);
        //this.addWallPaperButton.Dock = DockStyle.Left;
        
        //deleteWallPaperButton
        this.deleteWallPaperButton.Location = new System.Drawing.Point(165, 150);
        this.deleteWallPaperButton.Size = new System.Drawing.Size(100, 40);
        this.deleteWallPaperButton.Name = "deleteWallPaperButton";
        this.deleteWallPaperButton.Text = "\ud83d\uddd1\ufe0f Delete";
        this.deleteWallPaperButton.Font = new Font("Segoe UI Emoji", 12);
        this.deleteWallPaperButton.TextAlign = ContentAlignment.MiddleCenter;
        this.deleteWallPaperButton.UseVisualStyleBackColor = true;
        this.deleteWallPaperButton.Click += new System.EventHandler(this.DeleteWallPaperButtonClick);
        //this.deleteWallPaperButton.Dock = DockStyle.Left;
        
        //darkModeButton
        this.darkModeButton.Location = new System.Drawing.Point(10, 10);
        this.darkModeButton.Size = new System.Drawing.Size(40, 40);
        this.darkModeButton.Name = "darkModeButton";
        this.darkModeButton.Text = "\ud83c\udf19";
        this.darkModeButton.Font = new Font("Segoe UI Emoji", 15);
        darkModeButton.ImageAlign = ContentAlignment.MiddleCenter;
        this.darkModeButton.UseVisualStyleBackColor = true;
        this.darkModeButton.Click += (s, e) => _themeManager.ToggleTheme();
        
        //changeWallpaperFoldeButton
        this.changeWallpaperFolderButton.Location = new System.Drawing.Point(55, 10);
        this.changeWallpaperFolderButton.Size = new System.Drawing.Size(40, 40);
        this.changeWallpaperFolderButton.Name = "changeWallpaperFolderButton";
        this.changeWallpaperFolderButton.Text = "\ud83d\udcc2";
        this.changeWallpaperFolderButton.Font = new Font("Segoe UI Emoji", 15, FontStyle.Bold);
        this.changeWallpaperFolderButton.UseVisualStyleBackColor = true;
        this.changeWallpaperFolderButton.Click += new System.EventHandler(this.ChangeWallpaperFolderClick);
        
        //loadDefaultSettingsButton
        this.loadDefaultSettingsButton.Location = new System.Drawing.Point(100, 10);
        this.loadDefaultSettingsButton.Size = new System.Drawing.Size(40, 40);
        this.loadDefaultSettingsButton.Name = "loadDefaultSettingsButton";
        this.loadDefaultSettingsButton.Text = "\ud83d\udd04";
        this.loadDefaultSettingsButton.Font = new Font("Segoe UI Emoji", 15);
        this.loadDefaultSettingsButton.UseVisualStyleBackColor = true;
        this.loadDefaultSettingsButton.Click += (sender, args) => _settingsManager.LoadSettings(); 

        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1920/2, 1080/2);
        this.MinimumSize = new System.Drawing.Size(1920/2, 1080/2);
        this.Name = "Form1";
        this.Text = "Wallpaper Manager";
        this.ResumeLayout(false);
        
        //Add to layout
        this.Controls.Add(this.applyButton);
        this.Controls.Add(this.deleteWallPaperButton);
        this.Controls.Add(this.addWallPaperButton);
        this.Controls.Add(this.wallpapersListBox);
        this.Controls.Add(this.wallpapersDisplay);
        this.Controls.Add(this.darkModeButton);
        this.Controls.Add(changeWallpaperFolderButton);
        this.Controls.Add(this.loadDefaultSettingsButton);
    }
    
    #endregion
}