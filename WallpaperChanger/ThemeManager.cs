namespace WallpaperChanger;

public class ThemeManager
{
    private readonly SettingsManager _settingsManager;
    private readonly Form _form;

    public ThemeManager(SettingsManager settingsManager, Form form)
    {
        this._settingsManager = settingsManager;
        this._form = form;
    }

    public void ApplyTheme()
    {
        bool isDark = _settingsManager.Theme == "Dark";

        _form.BackColor = isDark ? Color.FromArgb(40, 40, 40) : SystemColors.Control;
        _form.ForeColor = isDark ? Color.White : SystemColors.ControlText;

        foreach (Control control in _form.Controls)
        {
            control.BackColor = isDark ? Color.FromArgb(100, 100, 100) : SystemColors.Control;
            control.ForeColor = isDark ? Color.White : SystemColors.ControlText;

            if (control is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
            }
        }

        if (_form.Controls["darkModeButton"] is Button themeButton)
        {
            themeButton.Text = isDark ? "\u2600\ufe0f" : "\ud83c\udf19";
        }
    }

    public void ToggleTheme()
    {
        _settingsManager.Theme = _settingsManager.Theme == "Light" ? "Dark" : "Light";
        _settingsManager.SaveSettings();
        ApplyTheme();
    }
}