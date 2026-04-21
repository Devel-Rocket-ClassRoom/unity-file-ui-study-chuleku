using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UiItemInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}:{1}";
    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;
    }

    public void SetSaveItemData(SaveItemData data)
    {
        ItemData itemData = data.itemData;

        imageIcon.sprite = itemData.IconSprite;
        textName.text = string.Format(FormatCommon,DataTableManager.StringTable.Get("Name"),itemData.StringName);
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Desc"), itemData.StringDesc);
        textType.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Type"), itemData.Type.ToString());
        textValue.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Value"), itemData.Value.ToString());
        textCost.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Cost"), itemData.Cost.ToString());

    }

}
