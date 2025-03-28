namespace WallpaperChanger;

partial class Form1
{
  
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button applyButton;
    private System.Windows.Forms.Button addWallPaperButton;
    private System.Windows.Forms.Button deleteWallPaperButton;
    private System.Windows.Forms.FlowLayoutPanel wallpapersDisplay;
    
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
        this.wallpapersDisplay = new System.Windows.Forms.FlowLayoutPanel();
        this.SuspendLayout();
        
        //wallpaperDisplay
        this.wallpapersDisplay.AutoScroll = true;
        this.wallpapersDisplay.Location = new System.Drawing.Point(300, 50);
        this.wallpapersDisplay.Name = "wallpapersDisplay";
        this.wallpapersDisplay.Size = new System.Drawing.Size(450, 350);
        this.wallpapersDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

        //applyButton
        this.applyButton.Location = new System.Drawing.Point(100, 100);
        this.applyButton.Size = new System.Drawing.Size(100, 40);
        this.applyButton.Name = "applyButton";
        this.applyButton.Text = "Apply";
        this.applyButton.UseVisualStyleBackColor = true;
        this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
        
        //addWallpaperButton
        this.addWallPaperButton.Location = new System.Drawing.Point(100, 160);
        this.addWallPaperButton.Size = new System.Drawing.Size(100, 40);
        this.addWallPaperButton.Name = "addWallPaperButton";
        this.addWallPaperButton.Text = "Upload Wallpaper";
        this.addWallPaperButton.UseVisualStyleBackColor = true;
        this.addWallPaperButton.Click += new System.EventHandler(this.AddWallPaperButtonClick);
        
        //deleteWallPaperButton
        this.deleteWallPaperButton.Location = new System.Drawing.Point(100, 200);
        this.deleteWallPaperButton.Size = new System.Drawing.Size(100, 40);
        this.deleteWallPaperButton.Name = "deleteWallPaperButton";
        this.deleteWallPaperButton.Text = "Delete Wallpaper";
        this.deleteWallPaperButton.UseVisualStyleBackColor = true;
        this.deleteWallPaperButton.Click += new System.EventHandler(this.DeleteWallPaperButtonClick);

        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Name = "Form1";
        this.Text = "Wallpaper Changer";
        this.ResumeLayout(false);
        
        //Add to layout
        this.Controls.Add(this.applyButton);
        this.Controls.Add(this.addWallPaperButton);
        this.Controls.Add(this.deleteWallPaperButton);
        this.Controls.Add(this.wallpapersDisplay);
    }
    
    #endregion
}