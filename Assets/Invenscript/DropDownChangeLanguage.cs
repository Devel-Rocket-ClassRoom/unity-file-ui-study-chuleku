using UnityEngine;
using UnityEngine.UI;

public class DropDownChangeLanguage : MonoBehaviour
{
    public void OnChangeLanguage(int index)
    {
        LocalizationText[] textobj = GameObject.FindObjectsOfType<LocalizationText>();
        foreach (LocalizationText text in textobj)
        {
            text.editlanguage = (Language)index;
            text.OnChangeLanguage(text.editlanguage);
        }
        LocalizationDropDown[] dropDownobj = GameObject.FindObjectsOfType<LocalizationDropDown>();
        foreach (LocalizationDropDown dropdown in dropDownobj)
        {
            dropdown.editorLang = (Language)index;
        }
    }

}
