namespace WallpaperChanger;

public class UploadManager
{
    private readonly string _targetFolder;
    private readonly ListBox _wallpapersListBox;
    private readonly ThemeManager _themeManager;
    private readonly SettingsManager _settingsManager;

    public UploadManager(string targetFolder, ListBox wallpapersListBox, SettingsManager settingsManager, ThemeManager themeManager)
    {
        this._targetFolder = targetFolder;
        this._wallpapersListBox = wallpapersListBox;
        _settingsManager = settingsManager;
        _themeManager = themeManager;
    }

    private bool IsImage(string path)
    {
        string ext = Path.GetExtension(path).ToLower();
        return ext is ".jpg" or ".jpeg" or ".png" or ".bmp";
    }
    
    public void UploadFromFile(string sourcePath)
    {

        if (!IsImage(sourcePath)) return;
        
        string fileName = Path.GetFileName(sourcePath);
        string destinationPath = Path.Combine(_targetFolder, fileName);
        if (!File.Exists(destinationPath))
        {
            File.Copy(sourcePath, destinationPath);
        }
        
        if (!_wallpapersListBox.Items.Contains(fileName))
        {
           _wallpapersListBox.Items.Add(fileName); 
        }
    }
    
    public void UploadMultiple(string[] sourcePaths)
    {
        foreach (string sourcePath in sourcePaths)
        {
            UploadFromFile(sourcePath);
        }
    }

    public void DragEnterWindow(object? sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Copy;
        _wallpapersListBox.BackColor = Color.LightBlue;
        _wallpapersListBox.BorderStyle = BorderStyle.FixedSingle;
        _wallpapersListBox.ForeColor = Color.Black;
    }
    
    public void DragAndDropWindow(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetData(DataFormats.FileDrop) is string[] files)
        {
            UploadMultiple(files);
        }
        _wallpapersListBox.BackColor = _settingsManager.Theme == "Dark" ? Color.FromArgb(100, 100, 100) : SystemColors.Control;
        _wallpapersListBox.ForeColor = _settingsManager.Theme == "Dark" ? Color.White : SystemColors.ControlText;
    }

    public void DragLeaveWindow(object? sender, EventArgs e)
    {
        _wallpapersListBox.BackColor = _settingsManager.Theme == "Dark" ? Color.FromArgb(100, 100, 100) : SystemColors.Control;
        _wallpapersListBox.ForeColor = _settingsManager.Theme == "Dark" ? Color.White : SystemColors.ControlText;
    }
}