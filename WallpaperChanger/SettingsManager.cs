namespace WallpaperChanger;

public class SettingsManager
{
    private readonly string _settingsPath;

    public string Theme { get; set; } = "Light";
    public string WallpapersPath { get; set; }
    public string Language { get; set; } = "PL";

    public SettingsManager(string defaultWallpaperPath)
    {
        string appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WallpaperManager");
        Directory.CreateDirectory(appData);
        _settingsPath = Path.Combine(appData, "settings.txt");
        WallpapersPath = defaultWallpaperPath;

        LoadSettings();
    }

    public void LoadSettings()
    {
        if (!File.Exists(_settingsPath)) return;
        
        var lines = File.ReadAllLines(_settingsPath);

        foreach (var line in lines)
        {
            if (line.StartsWith("Theme="))
            {
                string theme = line.Split("=")[1].Trim();
                Theme = theme;
            }

            if (line.StartsWith("Language="))
            {
                string lang = line.Split("=")[1].Trim();
                Language = lang;
            }

            if (line.StartsWith("WallpapersPath="))
            {
                string path = line.Split("=")[1].Trim();
                if (Directory.Exists(path))
                {
                    WallpapersPath = path;
                }
            }
        }
    }

    public void SaveSettings()
    {
        string settings = $"Theme={Theme}\nWallpapersPath={WallpapersPath}\nLanguage={Language}";
        File.WriteAllText(_settingsPath, settings);
    }
}