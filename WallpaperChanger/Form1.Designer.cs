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
    private System.Windows.Forms.ComboBox languageComboBox;
    
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

    private void AddControls(Button? button, Button button2, Button button3, Button button4, ComboBox? comboBox, ListBox? listBox, PictureBox? pictureBox)
    {
        this.Controls.Add(button);
        this.Controls.Add(button2);
        this.Controls.Add(button3);
        this.Controls.Add(button4);
        this.Controls.Add(comboBox);
        this.Controls.Add(listBox);
        this.Controls.Add(pictureBox);
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
        this.languageComboBox = new System.Windows.Forms.ComboBox();
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
        
        //applyButton
        ConfigureButtons(applyButton, "applyButton", "\ud83c\udfaf Apply", new Point(55, 100), new Size(210, 40), 15, ApplyButtonClick);
        
        //addWallpaperButton
        ConfigureButtons(addWallPaperButton, "addWallpaperButton", "\ud83d\uddbc\ufe0f Upload", new Point(55, 150), new Size(100, 40), 12, AddWallPaperButtonClick);
        
        //deleteWallPaperButton
        ConfigureButtons(deleteWallPaperButton, "deleteWallpaperButton", "\ud83d\uddd1\ufe0f Delete",
            new Point(165, 150), new Size(100, 40), 12, DeleteWallPaperButtonClick);
        
        //darkModeButton
        ConfigureButtons(darkModeButton, "darkModeButton", "\ud83c\udf19", new Point(10, 10), new Size(40, 40), 15, (s, e) => _themeManager.ToggleTheme());
        
        //changeWallpaperFoldeButton
        ConfigureButtons(changeWallpaperFolderButton, "changeWallpaperButton", "\ud83d\udcc2", new Point(55, 10), new Size(40, 40), 15, ChangeWallpaperFolderClick);
        
        //loadDefaultSettingsButton
        ConfigureButtons(loadDefaultSettingsButton, "loadDefaultSettingsButton", "\ud83d\udd04", new Point(100, 10), new Size(40, 40), 15, (s, e) => _settingsManager.LoadSettings());
        
        //languageComboBox
        this.languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1920/2, 1080/2);
        this.MinimumSize = new System.Drawing.Size(1920/2, 1080/2);
        this.Name = "Form1";
        this.Text = "Wallpaper Manager";
        this.ResumeLayout(false);
        
        //Add to layout
        AddControls(applyButton, addWallPaperButton, deleteWallPaperButton, loadDefaultSettingsButton, languageComboBox, wallpapersListBox, wallpapersDisplay);
        AddControls(darkModeButton, changeWallpaperFolderButton, null, null, null, null, null);
    }
    
    #endregion
}