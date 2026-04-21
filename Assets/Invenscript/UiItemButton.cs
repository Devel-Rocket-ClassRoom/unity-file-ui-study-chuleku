using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[ExecuteInEditMode]
public class UiItemButton : MonoBehaviour
{
    public string id;
    public Image imageicon;
    public TextMeshProUGUI Name;
    public UiItemInfo iteminfo;
    public UnityEvent<SaveItemData> onSelectSlot;
    private void Awake()
    {
        ItemData data = DataTableManager.ItemTable.Get(id);
        Name.text = data.StringName;
        imageicon.sprite = data.IconSprite;
    }

    public void OnChangeItemId()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.itemData = DataTableManager.ItemTable.Get(id);
        iteminfo.SetSaveItemData(newItem);
    }
}
