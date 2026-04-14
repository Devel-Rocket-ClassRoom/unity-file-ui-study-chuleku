using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDescription;


    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDescription.id = string.Empty;
        textName.OnChangeId();
        textDescription.OnChangeId();
    }
    public void SetItemData(string itemId)
    {
        ItemData item = DataTableManager.ItemTable.Get(itemId);
        if (item != null)
        {
            SetItemData(item);
        }
    }

    public void SetItemData(ItemData item)
    {
        icon.sprite = item.IconSprite;
        textName.id = item.Name;
        textDescription.id = item.Desc;

        textName.OnChangeId();
        textDescription.OnChangeId();
    }

}
