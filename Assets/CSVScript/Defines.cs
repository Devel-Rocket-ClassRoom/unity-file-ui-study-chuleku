using UnityEngine;

public enum Language
{
    Korean,
    English,
    Japanese
}

public static class Variables
{
    public static System.Action OnLanguageChanged;

    private static Language CurrentLanguage = Language.Korean;
    public static Language Language
    {
        get
        {
            return CurrentLanguage;
        }
        set
        {
            CurrentLanguage = value;
            DataTableManager.ChangeLanguage(CurrentLanguage);
            OnLanguageChanged?.Invoke();
        }
    }
}

public static class DataTableIds
{
    public static readonly string[] stringTableIds = new string[]
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };
    public static string String => stringTableIds[(int)Variables.Language];
}