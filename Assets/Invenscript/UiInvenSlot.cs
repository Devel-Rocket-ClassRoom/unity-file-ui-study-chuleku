using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInvenSlot : MonoBehaviour
{

    public Image imageIcon;
    public TextMeshProUGUI textName;
    public SaveItemData saveItemData { get; private set; }
    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        saveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        saveItemData = data;
        imageIcon.sprite = saveItemData.itemData.IconSprite;
        textName.text = saveItemData.itemData.StringName;
    }

}
