namespace WallpaperChanger;

public class DeleteManager
{
    private string _fullPath;
    private readonly LanguageManager _languageManager;
    private readonly SettingsManager _settingsManager;
    private ListBox _wallpaperListBox;

    public DeleteManager(string fullPath, LanguageManager languageManager, SettingsManager settingsManager, ListBox wallpaperListBox)
    {
        _fullPath = fullPath;
        _languageManager = languageManager;
        _settingsManager = settingsManager;
        _wallpaperListBox = wallpaperListBox;
    }

    public void DeleteWallpaper()
    {
        if (_wallpaperListBox.SelectedItem is not string selectedWallpaper)
        {
            MessageBox.Show(_languageManager.GetText("SelectWallpaper"), _languageManager.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var fullPath = Path.Combine(_settingsManager.WallpapersPath, selectedWallpaper);
        
        if (File.Exists(fullPath))
        {
            var confirmBox = MessageBox.Show(_languageManager.GetText("AreYouSureToDelete"), 
                _languageManager.GetText("SubmitAction"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmBox == DialogResult.Yes)
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(_languageManager.GetText("Error") + ex.Message);
                }
        }
        else
        {
            MessageBox.Show(_languageManager.GetText("WallpaperNotFound"), _languageManager.GetText("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}