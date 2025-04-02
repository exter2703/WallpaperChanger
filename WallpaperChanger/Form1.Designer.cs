namespace WallpaperChanger;

partial class Form1
{
  
    private System.ComponentModel.IContainer components = null;
    internal System.Windows.Forms.Button applyButton;
    internal System.Windows.Forms.Button addWallPaperButton;
    internal System.Windows.Forms.Button deleteWallPaperButton;
    private System.Windows.Forms.PictureBox wallpapersDisplay;
    internal System.Windows.Forms.ListBox wallpapersListBox;
    internal System.Windows.Forms.Button darkModeButton;
    internal System.Windows.Forms.Button changeWallpaperFolderButton;
    internal System.Windows.Forms.Button resetSettingsButton;
    internal System.Windows.Forms.ComboBox languageComboBox;
    internal System.Windows.Forms.ToolTip toolTip;
    internal System.Windows.Forms.Button wallpapersFolderLocation;
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
    
    private void ConfigureButtons(Button button, string name, string text, Point location, Size size, int textSize, EventHandler eventHandler)
    {
            button.Text = text;
            button.Location = location;
            button.Size = size;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI Emoji", textSize);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Click += eventHandler;
            button.UseVisualStyleBackColor = true;
            button.Name = name;
    }

    private void AddControls(params Control[] controls)
    {
        foreach (var control in controls)
        {
            if (control != null)
            {
                this.Controls.Add(control);
            }
        }
    }

    #region WallpaperManager
    private void InitializeComponent()
    {
        this.SuspendLayout();

        #region CreatingControls
        this.components = new System.ComponentModel.Container();
        this.applyButton = new System.Windows.Forms.Button();
        this.addWallPaperButton = new System.Windows.Forms.Button();
        this.deleteWallPaperButton = new System.Windows.Forms.Button();
        this.wallpapersDisplay = new System.Windows.Forms.PictureBox();
        this.wallpapersListBox = new System.Windows.Forms.ListBox();
        this.darkModeButton = new System.Windows.Forms.Button();
        this.changeWallpaperFolderButton = new System.Windows.Forms.Button();
        this.resetSettingsButton = new System.Windows.Forms.Button();
        this.languageComboBox = new System.Windows.Forms.ComboBox();
        this.toolTip = new ToolTip(this.components);
        this.wallpapersFolderLocation = new System.Windows.Forms.Button();
        #endregion

        #region Buttons
        //applyButton
        ConfigureButtons(applyButton, "applyButton", "\ud83c\udfaf Apply", new Point(55, 100), new Size(210, 40), 15, ApplyWallpaperButtonClick);
        
        //addWallpaperButton
        ConfigureButtons(addWallPaperButton, "addWallpaperButton", "\ud83d\uddbc\ufe0f Upload", new Point(55, 150), new Size(100, 40), 12, AddWallpaperClick);
        
        //deleteWallPaperButton
        ConfigureButtons(deleteWallPaperButton, "deleteWallpaperButton", "\ud83d\uddd1\ufe0f Delete",
            new Point(165, 150), new Size(100, 40), 12, DeleteWallpaperClick);
        
        //darkModeButton
        ConfigureButtons(darkModeButton, "darkModeButton", "\ud83c\udf19", new Point(10, 10), new Size(40, 40), 15, (s, e) => _themeManager.ToggleTheme());
        
        //changeWallpaperFoldeButton
        ConfigureButtons(changeWallpaperFolderButton, "changeWallpaperButton", "\ud83d\udcc2", new Point(55, 10), new Size(40, 40), 15, ChangeWallpaperFolderClick);
        
        //loadDefaultSettingsButton
        ConfigureButtons(resetSettingsButton, "resetSettingsButton", "\ud83d\udd04", 
            new Point(100, 10), new Size(40, 40), 15, (s, e) => LoadDefaultSettings());
        
        ConfigureButtons(wallpapersFolderLocation, "wallpapersFolderLocation", "Open wallpapers folder", new Point(55, 490), new Size (210, 27), 9, OpenWallpaperFolderClick);
        #endregion
        
        #region ButtonToolTips
        
        #endregion
        
        #region Others
        //wallpaperDisplay
        this.wallpapersDisplay.Location = new System.Drawing.Point(300, 100);
        this.wallpapersDisplay.Size = new System.Drawing.Size(1920/3, 1080/3);
        this.wallpapersDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.wallpapersDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.wallpapersDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
        this.wallpapersDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        //wallpaperListBox
        this.wallpapersListBox.Location = new System.Drawing.Point(55, 200);
        this.wallpapersListBox.Size = new System.Drawing.Size(210, 300);
        this.wallpapersListBox.SelectedIndexChanged += 
            new System.EventHandler(this.WallpapersListBoxSelectedIndexChanged);
        this.wallpapersListBox.FormattingEnabled = true;
        this.wallpapersListBox.AllowDrop = true;
        this.wallpapersListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        //languageComboBox
        this.languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        this.languageComboBox.FormattingEnabled = true;
        this.languageComboBox.Location = new System.Drawing.Point(150, 10);
        this.languageComboBox.Name = "languageComboBox";
        this.languageComboBox.Size = new System.Drawing.Size(47, 20);
        this.languageComboBox.Items.AddRange(new object[]{"PL", "ENG"});
        this.languageComboBox.SelectedIndex = 1;
        this.languageComboBox.SelectedIndexChanged += new EventHandler(this.ChangeLanguage);
        #endregion
        
        #region WindowProperties
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1920/2, 1080/2);
        this.MinimumSize = new System.Drawing.Size(1920/2, 1080/2);
        this.Name = "Form1";
        this.Text = "Wallpaper Manager";
        this.ResumeLayout(false);
        #endregion
        
        #region AddToLayout
        AddControls(applyButton, 
            addWallPaperButton, 
            deleteWallPaperButton, 
            resetSettingsButton,
            darkModeButton, 
            changeWallpaperFolderButton,
            languageComboBox, 
            wallpapersListBox, 
            wallpapersDisplay,
            wallpapersFolderLocation
            );
        #endregion
    }
    #endregion
}