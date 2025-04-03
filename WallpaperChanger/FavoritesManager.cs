using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace WallpaperChanger;

public class FavoritesManager
{
    private readonly string _favoritesFilePath;
    private HashSet<string> _favorites = new();
    

    public FavoritesManager()
    {
        string appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WallpaperManager");
        _favoritesFilePath = Path.Combine(appData, "favoriteWallpapers.json");
        Console.WriteLine($"Ulubione tapety: {_favoritesFilePath}");
        Load();
    }

    public bool IsFavorite(string fileName) => _favorites.Contains(fileName);

    public void Add(string fileName)
    {
        if (_favorites.Add(fileName)) Save();
    }

    public void Remove(string fileName)
    {
        if (_favorites.Remove(fileName)) Save();
    }
    
    public IEnumerable<string> GetAll() => _favorites;

    public IEnumerable<string> FilterFavorites(IEnumerable<string> files)
    {
        return files.Where(file => _favorites.Contains(Path.GetFileName(file)));
    }

    private void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_favoritesFilePath)!);
        File.WriteAllText(_favoritesFilePath, JsonSerializer.Serialize(_favorites));
    }

    public void Load()
    {
        if (File.Exists(_favoritesFilePath))
        {
            var json = File.ReadAllText(_favoritesFilePath);
            _favorites = JsonSerializer.Deserialize<HashSet<string>>(json) ?? new();
        }
    }
}