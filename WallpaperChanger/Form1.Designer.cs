namespace WallpaperChanger;

partial class Form1
{
  
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button applyButton;
    private System.Windows.Forms.Button addWallPaperButton;
    private System.Windows.Forms.Button deleteWallPaperButton;
    private System.Windows.Forms.PictureBox wallpapersDisplay;
    private System.Windows.Forms.ListBox wallpapersListBox;
    
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
        this.SuspendLayout();
        
        //wallpaperDisplay
        this.wallpapersDisplay.Location = new System.Drawing.Point(300, 100);
        this.wallpapersDisplay.Size = new System.Drawing.Size(1920/3, 1080/3);
        this.wallpapersDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.wallpapersDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        //this.wallpapersDisplay.Dock = System.Windows.Forms.DockStyle.Right;
        this.wallpapersDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Controls.Add(this.wallpapersDisplay);

        //wallpaperListBox
        this.wallpapersListBox.Location = new System.Drawing.Point(60, 200);
        this.wallpapersListBox.Size = new System.Drawing.Size(200, 225);
        this.wallpapersListBox.SelectedIndexChanged += 
            new System.EventHandler(this.WallpapersListBoxSelectedIndexChanged);
        //this.wallpapersListBox.Dock = System.Windows.Forms.DockStyle.Right;
        
        //applyButton
        this.applyButton.Location = new System.Drawing.Point(60, 100);
        this.applyButton.Size = new System.Drawing.Size(200, 40);
        this.applyButton.Name = "applyButton";
        this.applyButton.Text = "Apply";
        this.applyButton.UseVisualStyleBackColor = true;
        this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
        //this.applyButton.Dock = DockStyle.Top;
        
        //addWallpaperButton
        this.addWallPaperButton.Location = new System.Drawing.Point(60, 150);
        this.addWallPaperButton.Size = new System.Drawing.Size(100, 40);
        this.addWallPaperButton.Name = "addWallPaperButton";
        this.addWallPaperButton.Text = "Upload Wallpaper";
        this.addWallPaperButton.UseVisualStyleBackColor = true;
        this.addWallPaperButton.Click += new System.EventHandler(this.AddWallPaperButtonClick);
        //this.addWallPaperButton.Dock = DockStyle.Left;
        
        //deleteWallPaperButton
        this.deleteWallPaperButton.Location = new System.Drawing.Point(160, 150);
        this.deleteWallPaperButton.Size = new System.Drawing.Size(100, 40);
        this.deleteWallPaperButton.Name = "deleteWallPaperButton";
        this.deleteWallPaperButton.Text = "Delete Wallpaper";
        this.deleteWallPaperButton.UseVisualStyleBackColor = true;
        this.deleteWallPaperButton.Click += new System.EventHandler(this.DeleteWallPaperButtonClick);
        //this.deleteWallPaperButton.Dock = DockStyle.Left;

        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1920/2, 1080/2);
        this.MinimumSize = new System.Drawing.Size(1920/2, 1080/2);
        this.Name = "Form1";
        this.Text = "Wallpaper Changer";
        this.ResumeLayout(false);
        
        //Add to layout
        this.Controls.Add(this.applyButton);
        this.Controls.Add(this.deleteWallPaperButton);
        this.Controls.Add(this.addWallPaperButton);
        this.Controls.Add(this.wallpapersListBox);
        this.Controls.Add(this.wallpapersDisplay);
    }
    
    #endregion
}