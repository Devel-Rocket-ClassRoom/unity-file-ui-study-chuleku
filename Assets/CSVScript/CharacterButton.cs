using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{

    public string id;
    public LocalizationText nameLocalizationText;

    public Image iconImage;
    public CheckCharacher checkCharacher;
    public void OnChangeItemId()
    {
         CharacterData itemData = DataTableManager.CharacterTable.Get(id);
        if (itemData != null)
        {
            iconImage.sprite = itemData.IconSprite;
            nameLocalizationText.id = id;
            nameLocalizationText.OnChangeId();
        }
    }

    public void OnClick()
    {
        checkCharacher.SetCharacterData(id);
    }

}
