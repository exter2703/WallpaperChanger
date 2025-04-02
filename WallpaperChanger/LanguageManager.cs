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
                "OpenFileLocation" => "Otwórz lokalizację plików...",
                
                //Messeges
                "AppliedToast" => "Tapeta ustawiona!",
                "AddedToast" => "Tapety dodane!",
                "DeletedToast" => "Tapeta usunięta.",
                "ErrorToast" => "Wystąpił błąd.",
                "SelectWallpaper" => "Proszę wybrać tapete!",
                "SelectWallpaperToAdd" => "Wybierz tapetę do dodania",
                "WallpaperAlreadyAdded" => "Tapeta jest już dodana.",
                "WallpaperNotUpdated" => "Tapeta nie może zostać zmieniona.",
                "AreYouSure" => "Czy jesteś pewien?",
                "WallpaperNotFound" => "Tapeta nie znaleziona!",
                "FolderSelected" => "Folder na tapety wybrany!",
                "FileNotExists" => "Plik nie istnieje!",
                "AreYouSureToDelete" => $"Czy jesteś pewien, że chcesz usunąć {_form.wallpapersListBox.SelectedItem}?",
                "SubmitAction" => "Potwierdź akcję",
                "ChangeName" => "Zmień nazwę",
                "ProvideName" => "Podaj nową nazwę:",
                "Error" => "Błąd!",
                
                //ToolTip
                "LoadDefaultSettingsToolTip" => "Załaduj domyślne ustawienia",
                "ChangeLanguageToolTip" => "Zmień język",
                "LightModeToolTip" => "Zmień tryb na ciemny",
                "DarkModeToolTip" => "Zmień tryb na jasny",
                "ChangeTheme" => "Zmień motyw",
                "ChangeWallpaperFolder" => "Zmień folder przechowywania tapet",
                _ => key
            },
            Language.ENG => key switch
            {
                //Buttons
                "Apply" => "\ud83c\udfaf Apply",
                "Upload" => "\ud83d\uddbc\ufe0f Upload",
                "Delete" => "\ud83d\uddd1\ufe0f Delete",
                "OpenFileLocation" => "Open files location...",
                
                //Messeges
                "AppliedToast" => "Wallpaper changed!",
                "AddedToast" => "Wallpaper(s) added!",
                "DeletedToast" => "Wallpaper(s) deleted!",
                "ErrorToast" => "Error.",
                "SelectWallpaper" => "Please select a wallpaper!",
                "SelectWallpaperToAdd" => "Please select wallpaper to add",
                "WallpaperAlreadyAdded" => "Wallpaper already added!",
                "WallpaperNotUpdated" => "Wallpaper cannot be updated.",
                "AreYouSure" => "Are you sure?",
                "WallpaperNotFound" => "Wallpaper not found!",
                "FolderSelected" => "Wallpaper folder selected!",
                "FileNotExists" => "File not exists!",
                "AreYouSureToDelete" => $"Are you sure to delete {_form.wallpapersListBox.SelectedItem}?",
                "SubmitAction" => "Submit action",
                "ChangeName" => "Change name",
                "ProvideName" => "Provide new name:",
                "Error" => "Error!",
                
                //Tooltip
                "LoadDefaultSettingsToolTip" => "Load default settings",
                "ChangeLanguageToolTip" => "Change language",
                "LightModeToolTip" => "Change to Dark Mode",
                "DarkModeToolTip" => "Change to Light Mode",
                "ChangeTheme" => "Change theme",
                "ChangeWallpaperFolder" => "Change wallpaper folder",
                _ => key
            },
            _ => key
        };
    }
    
    public void SetToolTips()
    {
        _form.toolTip.SetToolTip(_form.darkModeButton, GetText("ChangeTheme"));
        _form.toolTip.SetToolTip(_form.resetSettingsButton, GetText("LoadDefaultSettingsToolTip"));
        _form.toolTip.SetToolTip(_form.languageComboBox, GetText("ChangeLanguageToolTip"));
        _form.toolTip.SetToolTip(_form.changeWallpaperFolderButton, GetText("ChangeWallpaperFolder"));
    }

    public void ApplyLanguage()
    {
        _form.applyButton.Text = GetText("Apply");
        _form.addWallPaperButton.Text = GetText("Upload");
        _form.deleteWallPaperButton.Text = GetText("Delete");
        _form.languageComboBox.Text = _currentLanguage.ToString();
        _form.wallpapersFolderLocation.Text = GetText("OpenFileLocation");
        SetToolTips();
    }
    public Language GetCurrentLanguage() => _currentLanguage;
}