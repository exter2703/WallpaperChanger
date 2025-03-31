namespace WallpaperChanger;

public class LanguageManager
{
    public readonly SettingsManager _settingsManager;
    public readonly Form1 _form;
    private Language _currentLanguage = Language.PL;

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
                "Apply" => "\ud83c\udfaf Zastosuj",
                "Upload" => "\ud83d\uddbc\ufe0f Dodaj",
                "Delete" => "\ud83d\uddd1\ufe0f Usuń",
                _ => key
            },
            Language.ENG => key switch
            {
                "Apply" => "\ud83c\udfaf Apply",
                "Upload" => "\ud83d\uddbc\ufe0f Upload",
                "Delete" => "\ud83d\uddd1\ufe0f Delete",
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