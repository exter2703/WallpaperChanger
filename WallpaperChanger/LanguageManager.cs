namespace WallpaperChanger;

public class LanguageManager
{
    private readonly SettingsManager _settingsManager;
    private readonly Form1 _form;
    private Language _currentLanguage;

    public enum Language
    {
        PL,
        ENG
    }

    public LanguageManager(SettingsManager settingsManager, Form1 form)
    {
        this._settingsManager = settingsManager;
        this._form = form;

        if (Enum.TryParse(_settingsManager.Language, out Language loadedLang))
        {
            _currentLanguage = loadedLang;
        }
        else
        {
            _currentLanguage = Language.PL;
        }
    }

    public void SetLanguage(Language language)
    {
        _currentLanguage = language;
        _settingsManager.Language = language.ToString();
        _settingsManager.SaveSettings();
    }

    public string GetText(string key)
    {
        return _currentLanguage switch
        {
            Language.PL => key switch
            {
                //Button
                "Apply" => "\ud83c\udfaf Zastosuj",
                "Upload" => "\ud83d\uddbc\ufe0f Dodaj",
                "Delete" => "\ud83d\uddd1\ufe0f Usuń",
                
                //Messeges
                "AppliedToast" => "Tapeta ustawiona!",
                "AddedToast" => "Tapety dodane!",
                "DeletedToast" => "Tapeta usunięta.",
                "ErrorToast" => "Wystąpił błąd.",
                "SelectWallpaper" => "Proszę wybrać tapetę.",
                "SelectWallpaperToAdd" => "Wybierz tapetę do dodania",
                "WallpaperAlreadyAdded" => "Tapeta jest już dodana.",
                "WallpaperNotUpdated" => "Tapeta nie może zostać zmieniona.",
                "AreYouSure" => "Czy jesteś pewien?",
                "WallpaperNotFound" => "Tapeta nie znaleziona!",
                "FolderSelected" => "Folder na tapety wybrany!",
                "FileNotExists" => "Plik nie istnieje!",
                "AreYouSureToDelete" => $"Czy jesteś pewien, że chcesz usunąć {_form.wallpapersListBox.SelectedItem}?",
                "SubmitAction" => "Potwierdź akcję",
                _ => key
            },
            Language.ENG => key switch
            {
                //Buttons
                "Apply" => "\ud83c\udfaf Apply",
                "Upload" => "\ud83d\uddbc\ufe0f Upload",
                "Delete" => "\ud83d\uddd1\ufe0f Delete",
                
                //Messeges
                "AppliedToast" => "Wallpaper changed!",
                "AddedToast" => "Wallpaper(s) added!",
                "DeletedToast" => "Wallpaper(s) deleted!",
                "ErrorToast" => "Error.",
                "SelectWallpaper" => "Select wallpaper.",
                "SelectWallpaperToAdd" => "Please select wallpaper to add",
                "WallpaperAlreadyAdded" => "Wallpaper already added!",
                "WallpaperNotUpdated" => "Wallpaper cannot be updated.",
                "AreYouSure" => "Are you sure?",
                "WallpaperNotFound" => "Wallpaper not found!",
                "FolderSelected" => "Wallpaper folder selected!",
                "FileNotExists" => "File not exists!",
                "AreYouSureToDelete" => $"Are you sure to delete {_form.wallpapersListBox.SelectedItem}?",
                "SubmitAction" => "Submit action",
                _ => key
            },
            _ => key
        };
    }

    public void ApplyLanguage()
    {
        _form.applyButton.Text = GetText("Apply");
        _form.addWallPaperButton.Text = GetText("Upload");
        _form.deleteWallPaperButton.Text = GetText("Delete");
        _form.languageComboBox.Text = _currentLanguage.ToString();
    }
    public Language GetCurrentLanguage() => _currentLanguage;
}